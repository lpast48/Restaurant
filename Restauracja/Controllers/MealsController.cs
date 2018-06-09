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
    public class MealsController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Meals
        public ActionResult Index()
        {
            var meal = db.Meal.Where(m => m.Visibility == true);
            return View(meal.ToList());
        }

        public ActionResult ChefIndex()
        {
            var meal = db.Meal;
            return View(meal.ToList());
        }

        // GET: Meals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // GET: Meals/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Ingredients,Allergens,Price")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Meal.Add(meal);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(meal);
                }
            }
            ViewBag.Error = false;
            return View(meal);
        }

        // GET: Meals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Ingredients,Allergens,Price,Visibility")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(meal).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(meal);
                }
            }
            ViewBag.Error = false;
            return View(meal);
        }

        // GET: Meals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meal.Find(id);
            db.Meal.Remove(meal);
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
