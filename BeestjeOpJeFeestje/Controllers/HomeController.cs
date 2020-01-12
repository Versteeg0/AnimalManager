using BeestjeOpJeFeestje.Discount;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.Validation;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private BeestValidation beestValidation;
        private readonly IBoekingRepository boekingRepository;
        private CalculateDiscount calculateDiscount;

        public HomeController(IBoekingRepository repo)
        {
            boekingRepository = repo;
            beestValidation = new BeestValidation();
        }

        /**
         * Startpage of the application, checks if tempdata is not empty to handle validation
         */
        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["nodateselected"] != null)
            {
                ViewBag.Error = TempData["nodateselected"].ToString();
            }
            return View();
        }

        /**
        * First step of the process, checks if tempdata is not empty to handle validation
        * Sets a list of all the animals in the database, if they are already in a booking on that date
        * they will become unable to select.
        */
        public ActionResult Stap1(BoekingVM boekingVM)
        {
            if(boekingVM.Date < DateTime.Now)
            {
                TempData["nodateselected"] = "Selecteer een valide datum.";
                return RedirectToAction("Index", "Home");
            }

            if(TempData["nobeestselected"] != null)
            {
                ViewBag.Error = TempData["nobeestselected"].ToString();
            }

            if (TempData["wrongcollection"] != null)
            {
                ViewBag.Error = TempData["wrongcollection"].ToString();
            }
            

            var beestjes = boekingRepository.GetBeestjes();
            List<BeestjeVM> beestlijst = new List<BeestjeVM>();
            
            foreach(var b in beestjes)
            {
                if(beestValidation.BeestjeHasNoBoeking(b, boekingVM, boekingRepository))
                boekingVM.Beestjes.Add(new BeestjeVM { Beest = b, HasBoeking = false });
                else
                 boekingVM.Beestjes.Add(new BeestjeVM { Beest = b, HasBoeking = true });
            }
            return View(boekingVM);
        }

        /**
         * Second step of the process, checks if tempdata is not empty to handle validation
         * Sets a list of all the accessoires that are from one of the selected animals
         */
        public ActionResult Stap2(BoekingVM boekingVM)
        {
            if (boekingVM.Date < DateTime.Now)
            {
                TempData["nodateselected"] = "Selecteer een valide datum.";
                return RedirectToAction("Index", "Home");
            }

            foreach (BeestjeVM b in boekingVM.Beestjes)
            {
                  if (b.IsSelected)
                  {
                      boekingVM.SelectedBeestjes.Add(boekingRepository.GetBeestjeById(b.Id));
                      boekingVM.BeestjesIds.Add(b.Id);
                  }
            }

            if (boekingVM.SelectedBeestjes.Count == 0)
            {
                TempData["nobeestselected"] = "Selecteer minimaal een beest.";
                return RedirectToAction("Stap1", "Home", new {boekingVM.Date});
            }

            string check = beestValidation.CheckIfSelectedBeestjesAreValid(boekingVM);

            if(check != null)
            {
                TempData["wrongcollection"] = check;
                return RedirectToAction("Stap1", "Home", new { boekingVM.Date});
            }

            foreach(Beestje beest in boekingVM.SelectedBeestjes)
            {
                foreach(Accessoires a in boekingRepository.GetAccessoires())
                {
                    if(a.Beest == beest)
                    boekingVM.Accessoires.Add(a);
                }
            }
            return View(boekingVM);
        }

        /**
         * Third step of the process, checks if tempdata is not empty to handle validation
         * User can input his data here for the booking
         */
        public ActionResult Stap3(BoekingVM boekingVM)
        {
            if (boekingVM.Date < DateTime.Now)
            {
                TempData["nodateselected"] = "Selecteer een valide datum.";
                return RedirectToAction("Index", "Home");
            }

            foreach (int i in boekingVM.BeestjesIds)
                    boekingVM.SelectedBeestjes.Add(boekingRepository.GetBeestjeById(i));

                foreach (Accessoires a in boekingVM.Accessoires)
                {
                    if (a.IsSelected)
                    {
                        boekingVM.SelectedAccessoires.Add(boekingRepository.GetAccessoireById(a.Id));
                        boekingVM.AccessoiresIds.Add(a.Id);
                    }
                }
         
            return View(boekingVM);
        }

        /**
        * Fourth step of the process, checks if tempdata is not empty to handle validation
        * User can see and confirm his booking
        * Also calculates the total price with discounts by using the CalculateDiscount class
        */
        public ActionResult Stap4([Bind(Include = "Date,FirstName, Prefix, LastName, Adres, Email, Number, BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
            if (boekingVM.Date < DateTime.Now)
            {
                TempData["nodateselected"] = "Selecteer een valide datum.";
                return RedirectToAction("Index", "Home");
            }

            calculateDiscount = new CalculateDiscount();
                foreach (int i in boekingVM.BeestjesIds)
                    boekingVM.SelectedBeestjes.Add(boekingRepository.GetBeestjeById(i));

                foreach (int i in boekingVM.AccessoiresIds)
                    boekingVM.SelectedAccessoires.Add(boekingRepository.GetAccessoireById(i));

            boekingVM.FullName = boekingVM.FirstName + " " + boekingVM.Prefix + " " + boekingVM.LastName;
            boekingVM.TotalPrice = calculateDiscount.CalculateTotalPrice(boekingVM);
            boekingVM.DiscountList = calculateDiscount.DiscountList;

            return View(boekingVM);
        }

        /**
        * Last step of the process, 
        * User sees a conformation message of his booking.
        * Saves the booking in the database
        */
        [HttpPost]
        public ActionResult Finish([Bind(Include = "Date,FirstName, Prefix, LastName, Adres, Email, Number, TotalPrice, BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
            boekingRepository.AddBoeking(boekingVM);
            return View();
        }
    }
}