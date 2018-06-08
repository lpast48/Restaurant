using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restauracja.Models;

namespace Restauracja.Controllers
{
    public class Order_MealController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Order_Meal
        public ActionResult Index()
        {
            var order_Meal = db.Order_Meal.Include(o => o.Meal).Include(o => o.Order);
            return View(order_Meal.ToList());
        }

        // GET: Order_Meal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Meal order_Meal = db.Order_Meal.Find(id);
            if (order_Meal == null)
            {
                return HttpNotFound();
            }
            return View(order_Meal);
        }

        // GET: Order_Meal/Create
        public ActionResult Create()
        {
            ViewBag.MealId = new SelectList(db.Meal, "Id", "Name");
            ViewBag.OrderId = new SelectList(db.Order, "Id", "WaiterId");
            return View();
        }

        // POST: Order_Meal/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,MealId")] Order_Meal order_Meal)
        {
            if (ModelState.IsValid)
            {
                db.Order_Meal.Add(order_Meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MealId = new SelectList(db.Meal, "Id", "Name", order_Meal.MealId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "WaiterId", order_Meal.OrderId);
            return View(order_Meal);
        }

        // GET: Order_Meal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Meal order_Meal = db.Order_Meal.Find(id);
            if (order_Meal == null)
            {
                return HttpNotFound();
            }
            ViewBag.MealId = new SelectList(db.Meal, "Id", "Name", order_Meal.MealId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "WaiterId", order_Meal.OrderId);
            return View(order_Meal);
        }

        // POST: Order_Meal/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,MealId")] Order_Meal order_Meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MealId = new SelectList(db.Meal, "Id", "Name", order_Meal.MealId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "WaiterId", order_Meal.OrderId);
            return View(order_Meal);
        }

        // GET: Order_Meal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Meal order_Meal = db.Order_Meal.Find(id);
            if (order_Meal == null)
            {
                return HttpNotFound();
            }
            return View(order_Meal);
        }

        // POST: Order_Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Meal order_Meal = db.Order_Meal.Find(id);
            db.Order_Meal.Remove(order_Meal);
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
    }
}
