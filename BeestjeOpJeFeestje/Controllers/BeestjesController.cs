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
    public class BeestjesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beestjes
        public ActionResult Index()
        {
            return View(db.Beestjes.ToList());
        }

        // GET: Beestjes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beestje beestje = db.Beestjes.Find(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            return View(beestje);
        }

        // GET: Beestjes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beestjes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Price,imagePath")] Beestje beestje)
        {
            if (ModelState.IsValid)
            {
                db.Beestjes.Add(beestje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beestje);
        }

        // GET: Beestjes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beestje beestje = db.Beestjes.Find(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            return View(beestje);
        }

        // POST: Beestjes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,Price,imagePath")] Beestje beestje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beestje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beestje);
        }

        // GET: Beestjes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beestje beestje = db.Beestjes.Find(id);
            if (beestje == null)
            {
                return HttpNotFound();
            }
            return View(beestje);
        }

        // POST: Beestjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beestje beestje = db.Beestjes.Find(id);
            db.Beestjes.Remove(beestje);
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
