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
    public class AccessoiresController : Controller
    {
        public AccessoiresRepository accessoiresRepository = new AccessoiresRepository();

        // GET: Accessoires
        public ActionResult Index()
        {
            return View(accessoiresRepository.GetAccessoires());
        }

        // GET: Accessoires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
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
                accessoiresRepository.CreateAccessoire(accessoires);
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
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
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
                accessoiresRepository.EditAccessoire(accessoires);
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
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
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
            Accessoires accessoires = accessoiresRepository.GetAccessoireById(id);
            accessoiresRepository.DeleteAccessoire(accessoires);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                accessoiresRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
