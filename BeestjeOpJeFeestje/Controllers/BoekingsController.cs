﻿using System;
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
    public class BoekingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boekings
        public ActionResult Index()
        {
            return View(db.Boekings.ToList());
        }

        // GET: Boekings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boeking boeking = db.Boekings.Find(id);
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        // GET: Boekings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boekings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,FirstName,Prefix,LastName,Email")] Boeking boeking)
        {
            if (ModelState.IsValid)
            {
                db.Boekings.Add(boeking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boeking);
        }

        // GET: Boekings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boeking boeking = db.Boekings.Find(id);
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        // POST: Boekings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,FirstName,Prefix,LastName,Email")] Boeking boeking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boeking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boeking);
        }

        // GET: Boekings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boeking boeking = db.Boekings.Find(id);
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        // POST: Boekings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boeking boeking = db.Boekings.Find(id);
            db.Boekings.Remove(boeking);
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