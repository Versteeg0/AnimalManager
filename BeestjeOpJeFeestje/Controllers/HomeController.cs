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
        
        public ActionResult Stap1(BoekingVM boekingVM)
        {
            if(boekingVM.Date < DateTime.Now)
            {
                return RedirectToAction("Index");
            }

            var beestjes = boekingRepository.GetBeestjes();
            boekingVM.Beestjes = beestjes;

            return View(boekingVM);
        }

        public ActionResult Stap2(BoekingVM boekingVM)
        {
            var accessoires = boekingRepository.GetAccessoires();
            boekingVM.Accessoires = accessoires;

            return View(boekingVM) ;
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


            return View(boekingVM);
        }
    }
}