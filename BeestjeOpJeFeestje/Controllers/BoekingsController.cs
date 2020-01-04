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
using BeestjeOpJeFeestje.ViewModels;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BoekingsController : Controller
    {
        private readonly IBoekingRepository boekingRepository;

        public BoekingsController(IBoekingRepository repo)
        {
            boekingRepository = repo;
        }

        // GET: Boekings
        public ActionResult Index()
        {
            List<BoekingVM> boekingList = new List<BoekingVM>();
            foreach (Boeking b in boekingRepository.GetAllBoeking())
                boekingList.Add(new BoekingVM { BoekingModel = b });
            return View(boekingList);
        }

        // GET: Boekings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boeking boeking = boekingRepository.GetBoekingById(id);
            if (boeking == null)
            {
                return HttpNotFound();
            }
            BoekingVM boekingVM = new BoekingVM();
            boekingVM.BoekingModel = boeking;
            boekingVM.FullName = boekingVM.FirstName + " " + boekingVM.Prefix + " " + boekingVM.LastName;
            
            return View(boekingVM);
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
        public ActionResult Create([Bind(Include = "Id,Date,FirstName,Prefix,LastName,Email")] BoekingVM boeking)
        {
            if (ModelState.IsValid)
            {
                boekingRepository.AddBoeking(boeking);
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
            Boeking boeking = boekingRepository.GetBoekingById(id);
            if (boeking == null)
            {
                return HttpNotFound();
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
            Boeking boeking = boekingRepository.GetBoekingById(id);
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
            Boeking boeking = boekingRepository.GetBoekingById(id);
            boekingRepository.RemoveBoeking(boeking);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                boekingRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
