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

        public HomeController(IBoekingRepository repo)
        {
            boekingRepository = repo;
        }
        public ActionResult Index()
        {
            var boekingVM = new BoekingVM();
            if (Session["nodateselected"] != null)
                ViewBag.Error = Session["nodateselected"].ToString();
            return View(boekingVM);
        }

        public ActionResult Stap1(BoekingVM boekingVM)
        {
            
            if(boekingVM.Date < DateTime.Now)
            {
                Session["nodateselected"] = "Selecteer een datum na de huidige datum om een boeking aan te maken.";
                return RedirectToAction("Index");
            }
            if(Session["nobeestselected"] != null)
                 ViewBag.Error = Session["nobeestselected"].ToString();  

            var beestjes = boekingRepository.GetBeestjes();
            List<BeestjeVM> beestlijst = new List<BeestjeVM>();
            
            foreach(var b in beestjes)
            {
                if(BeestjeHasNoBoeking(b))
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
                Session["nobeestselected"] = "Selecteer minimaal een beest.";
                return RedirectToAction("Stap1", new {boekingVM.Date, NoError = false});
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
            foreach(int i in boekingVM.BeestjesIds)
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

        public ActionResult Stap4([Bind(Include = "Date,FirstName,LastName,Adres,Email,BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
            foreach (int i in boekingVM.BeestjesIds)
                boekingVM.SelectedBeestjes.Add(boekingRepository.GetBeestjeById(i));

            foreach (int i in boekingVM.AccessoiresIds)
                boekingVM.SelectedAccessoires.Add(boekingRepository.GetAccessoireById(i));

            boekingVM.FullName = boekingVM.FirstName + boekingVM.Prefix + boekingVM.LastName;
            boekingVM.TotalPrice = CalculateTotalPrice(boekingVM);

            return View(boekingVM);
        }



        [HttpPost]
        public ActionResult Finish([Bind(Include = "Date,FirstName,LastName,Adres,Email,BeestjesIds, AccessoiresIds")]BoekingVM boekingVM)
        {
            boekingRepository.AddBoeking(boekingVM);
            return View();
        }


        private bool BeestjeHasNoBoeking(Beestje b)
        {
            foreach(Boeking boeking in boekingRepository.GetAllBoeking())
            {
                if (boeking.Beestjes != null && boeking.Beestjes.FirstOrDefault(beest => beest.Id == b.Id) != null)
                {
                    return false;
                }
            }
            return true;
        }

        private decimal CalculateTotalPrice(BoekingVM boekingVM)
        {
            decimal totalprice = 0;

            foreach(Beestje b in boekingVM.SelectedBeestjes)
                totalprice += b.Price;

            foreach(Accessoires a in boekingVM.SelectedAccessoires)
                totalprice += a.Price;

            return totalprice;
        }
    }
}