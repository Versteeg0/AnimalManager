using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BeestjesController : Controller
    {
        private BeestjesRepository beestjesRepository = new BeestjesRepository();

        // GET: Beestjes
        public ActionResult Index()
        {
            return View(beestjesRepository.GetBeestjes());
        }

        // GET: Beestjes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Price,imagePath")] Beestje beestje)
        {
            if (ModelState.IsValid)
            {
                beestjesRepository.AddBeestje(beestje);
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
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
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
                beestjesRepository.EditBeestje(beestje);
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
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
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
            Beestje beestje = beestjesRepository.GetBeestjeById(id);
            beestjesRepository.DeleteBeestje(beestje);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                beestjesRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
