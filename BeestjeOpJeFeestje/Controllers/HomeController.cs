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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Stap1(DateTime date)
        {
            BoekingVM boekingVM = new BoekingVM();

            var beestjes = boekingRepository.GetBeestjes();
            boekingVM.Beestjes = beestjes;

            boekingVM.Date = date;
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