﻿using System.Collections.Generic;
using System.Net;
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

        // Shows all the bookings in the database
        public ActionResult Index()
        {
            List<BoekingVM> boekingList = new List<BoekingVM>();
            foreach (Boeking b in boekingRepository.GetAllBoeking())
                boekingList.Add(new BoekingVM { BoekingModel = b });
            return View(boekingList);
        }

        // Shows the details of a booking
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
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

        //Get the selected booking and show the delete page
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Boeking boeking = boekingRepository.GetBoekingById(id);
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        //Delete the booking after the user confirms this
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boeking boeking = boekingRepository.GetBoekingById(id);
            boekingRepository.RemoveBoeking(boeking);
            return RedirectToAction("Index", "Boekings");
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
