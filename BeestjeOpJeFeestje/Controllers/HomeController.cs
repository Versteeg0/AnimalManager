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

            return View(boekingVM);
        }

        public ActionResult Stap1(BoekingVM model)
        {
            BoekingVM boekingVM = model;
            if(boekingVM.Date < DateTime.Now)
            {
                ViewBag.Error = "Selecteer een datum om een boeking aan te maken.";
                return RedirectToAction("Index");
            }
            
            var beestjes = boekingRepository.GetBeestjes();
            List<BeestjeVM> beestlijst = new List<BeestjeVM>();
            
            foreach(var b in beestjes)
            {
                beestlijst.Add(new BeestjeVM { Beest = b });
            }
            boekingVM.Beestjes = beestlijst;

            return View(boekingVM);
        }

        public ActionResult Stap2(BoekingVM model)
        {
            BoekingVM boekingVM = model;
            foreach (BeestjeVM b in boekingVM.Beestjes)
                {
                  if (b.IsSelected && BoekingHasNoBeestje(b.Beest))
                  {
                      boekingVM.SelectedBeestjes.Add(boekingRepository.GetBeestjeById(b.Id));
                  }
              }

                if (boekingVM.SelectedAccessoires.Count == 0)
                {
                    ViewBag.Error = "Selecteer minimaal een Accessoire voor je boeking.";
                    return RedirectToAction("Stap2", boekingVM);
                }

                var accessoires = boekingRepository.GetAccessoires();
                List<AccessoireVM> accessoirelijst = new List<AccessoireVM>();

            foreach (var a in accessoires)
            {
                accessoirelijst.Add(new AccessoireVM { Accessoire = a });
            }
                boekingVM.Accessoires = accessoires;

                return View(boekingVM);
        }

        public ActionResult Stap3(BoekingVM boekingVM)
        {
            

            return View(boekingVM);
        }

        public ActionResult Stap4(BoekingVM boekingVM)
        {


            return View(boekingVM);
        }

        public ActionResult Finish(BoekingVM boekingVM)
        {


            return View();
        }


        public bool BoekingHasNoBeestje(Beestje b)
        {
            foreach(Boeking boeking in boekingRepository.GetAllBoeking())
            {
                if (boeking.Beestjes.Contains(b))
                {
                    return false;
                }
            }
            return true;
        }
    }
}