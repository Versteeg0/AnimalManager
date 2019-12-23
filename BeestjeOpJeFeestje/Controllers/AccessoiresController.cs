using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AccessoiresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accessoires
        public ActionResult Index()
        {
            return View(db.Accessoires.ToList());
        }

        // GET: Accessoires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessoires accessoires = db.Accessoires.Find(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            return View(accessoires);
        }

        // GET: Accessoires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accessoires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,imagePath")] Accessoires accessoires)
        {
            if (ModelState.IsValid)
            {
                db.Accessoires.Add(accessoires);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessoires);
        }

        // GET: Accessoires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessoires accessoires = db.Accessoires.Find(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            return View(accessoires);
        }

        // POST: Accessoires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,imagePath")] Accessoires accessoires)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessoires).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessoires);
        }

        // GET: Accessoires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessoires accessoires = db.Accessoires.Find(id);
            if (accessoires == null)
            {
                return HttpNotFound();
            }
            return View(accessoires);
        }

        // POST: Accessoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accessoires accessoires = db.Accessoires.Find(id);
            db.Accessoires.Remove(accessoires);
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
