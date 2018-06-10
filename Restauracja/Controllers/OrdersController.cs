using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restauracja.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.ComponentModel.DataAnnotations;

namespace Restauracja.Controllers
{
    public class OrdersController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Orders
        public ActionResult Index()
        {
            var order = db.Order.
                Where(o => o.MealTime == null).
                OrderBy(o=>o.Table).ToList();
            return View(order);
        }

        public ActionResult TimeLaps()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TimeLaps([Bind(Include = "StartDate,EndDate")] TimeLap timeLap)
        {
            if (ModelState.IsValid)
            {
                if (timeLap.StartDate < timeLap.EndDate && timeLap.StartDate<DateTime.Now)
                {
                    ViewBag.Error = false;
                    return RedirectToAction("Statistic", timeLap);
                }
                ViewBag.Error = true;
                return View(timeLap);
            }
            return View(timeLap);
        }

        public ActionResult Statistic(TimeLap timeLap)
        {
            var test = db.Order.
                Where(o => o.OrderTime > timeLap.StartDate).
                Where(o => o.OrderTime < timeLap.EndDate).
                GroupBy(t => t.Table).
                Select(group =>new Statistics {
                    PriceSum = group.Sum(o => o.Price),
                    TableId = group.Key }).ToList();

            return View(test);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.WaiterId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Table")] Order order)
        {
            var orders = db.Order.Where(o => o.MealTime == null).ToList();
            order.WaiterId = User.Identity.GetUserId();
            order.OrderTime = DateTime.Now;
            order.MealTime = null;
            if (ModelState.IsValid)
            {
                foreach (var item in orders)
                {
                    if(order.Table == item.Table)
                    {
                        ViewBag.Error = true;
                        ViewBag.TableError = true;
                        return View(order);
                    }
                }
                try
                {
                    db.Order.Add(order);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(order);
                }
                ViewBag.Error = false;
                ViewBag.ZamowienieId = order.Id;
                return View(order);
            }
            return View(order);
        }



        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.WaiterId = new SelectList(db.Users, "Id", "Email", order.WaiterId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WaiterId,Table,OrderTime,MealTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WaiterId = new SelectList(db.Users, "Id", "Email", order.WaiterId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Order.Find(id).MealTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class Statistics
        {
            public int TableId { get; set; }
            public int PriceSum { get; set; }
        }

        public class TimeLap
        {
            [Display(Name = "Czas początku statystyk")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
            public System.DateTime StartDate { get; set; }

            [Display(Name = "Czas końcowy statystyk")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
            public System.DateTime EndDate { get; set; }
        }
    }
}
