using BeestjeOpJeFeestje.Discount;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoekingRepository boekingRepository;
        private CalculateDiscount calculateDiscount;

        public HomeController(IBoekingRepository repo)
        {
            boekingRepository = repo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["nodateselected"] != null)
            {
                ViewBag.Error = TempData["nodateselected"].ToString();
            }
            return View();
        }

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
                if(BeestjeHasNoBoeking(b, boekingVM))
                boekingVM.Beestjes.Add(new BeestjeVM { Beest = b, HasBoeking = false });
                else
                 boekingVM.Beestjes.Add(new BeestjeVM { Beest = b, HasBoeking = true });
            }
            return View(boekingVM);
        }

        public ActionResult Stap2(BoekingVM boekingVM)
        {
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
                return RedirectToAction("Stap1", new {boekingVM.Date});
            }

            string check = CheckIfSelectedBeestjesAreValid(boekingVM);

            if(check != null)
            {
                TempData["wrongcollection"] = check;
                return RedirectToAction("Stap1", new { boekingVM.Date});
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

        public ActionResult Stap3(BoekingVM boekingVM)
        {
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

        public ActionResult Stap4([Bind(Include = "Date,FirstName, Prefix, LastName, Adres, Email, Number, BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
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

        [HttpPost]
        public ActionResult Finish([Bind(Include = "Date,FirstName, Prefix, LastName, Adres, Email, Number, TotalPrice, BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
            boekingRepository.AddBoeking(boekingVM);
            return View();
        }

        private bool BeestjeHasNoBoeking(Beestje b, BoekingVM currentBoeking)
        {
            foreach(Boeking boeking in boekingRepository.GetAllBoeking())
            {
                if (boeking.Beestjes != null && boeking.Beestjes.FirstOrDefault(beest => beest.Id == b.Id) != null && boeking.Date == currentBoeking.Date)
                {
                    return false;
                }
            }
            return true;
        }

        public string CheckIfSelectedBeestjesAreValid(BoekingVM boeking)
        {
            bool isFarmAnimal = false;
            bool isLionorPolar = false;
            bool isPinguin = false;
            bool isDesert = false;
            bool isSnow = false;
            foreach (Beestje b in boeking.SelectedBeestjes)
            {
                if (b.Type == "Boerderij")
                    isFarmAnimal = true;

                if (b.Name == "Leeuw" || b.Name == "Ijsbeer")
                    isLionorPolar = true;

                if (b.Name == "Pinguïn")
                    isPinguin = true;

                if (b.Type == "Woestijn")
                    isDesert = true;

                if (b.Type == "Sneeuw")
                    isSnow = true;
            }

            if (isFarmAnimal && isLionorPolar)
                return "Je mag geen leeuw of Ijsbeer bij boerderijdieren.";

            DayOfWeek day = boeking.Date.DayOfWeek;
            if (isPinguin && ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday)))
                return "Je mag helaas geen pinguïns reserveren in het weekend.";

            if (isDesert && (boeking.Date.Month > 9 || boeking.Date.Month < 3))
                return "Je mag helaas geen woestijn dieren reserveren in de maanden oktober t/m februari.";   
            
            if (isSnow && (boeking.Date.Month > 5 && boeking.Date.Month < 9))
                return "Je mag helaas geen sneeuw dieren reserveren in de maanden juni t/m augustus.";

            return null;
        }
    }
}