using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeestjeOpJeFeestje;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Repos;
using Moq;
using BeestjeOpJeFeestje.ViewModels;
using BeestjeOpJeFeestje.Models;
using MvcContrib.TestHelper;
using BeestjeOpJeFeestje.Validation;

namespace BeestjeOpJeFeestje.Tests.Controllers.BoekingTest
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IBoekingRepository> repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Mock<IBoekingRepository>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexWrongDate()
        {
            // Arrange
            var tempData = new TempDataDictionary();
            tempData["nodateselected"] = "Selecteer een valide datum.";
            HomeController controller = new HomeController(repo.Object) { TempData = tempData };
            controller.TempData = tempData;

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(controller.ViewBag.Error, "Selecteer een valide datum.");
        }

        [TestMethod]
        public void Stap1()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjes()).Returns(new List<Beestje>());
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);

            // Act
            ViewResult result = controller.Stap1(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Stap1NoSelectedBeestje()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjes()).Returns(new List<Beestje>());

            var tempData = new TempDataDictionary();
            tempData["nobeestselected"] = "Selecteer minimaal een beest.";

            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);
           
            HomeController controller = new HomeController(repo.Object) { TempData = tempData };
            controller.TempData = tempData;

            // Act
            ViewResult result = controller.Stap1(boekingVM.Object) as ViewResult;
    
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(controller.ViewBag.Error, "Selecteer minimaal een beest.");
        }

        [TestMethod]
        public void Stap1WrongCollection()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjes()).Returns(new List<Beestje>());

            var tempData = new TempDataDictionary();
            tempData["wrongcollection"] = "Je mag geen Ijsbeer of leeuw bij een Boerderijdier";

            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);

            HomeController controller = new HomeController(repo.Object) { TempData = tempData };
            controller.TempData = tempData;

            // Act
            ViewResult result = controller.Stap1(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(controller.ViewBag.Error, "Je mag geen Ijsbeer of leeuw bij een Boerderijdier");
        }

        [TestMethod]
        public void Stap2()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoires()).Returns(new List<Accessoires>());
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> beest = new Mock<Beestje>();
            beest.Object.IsSelected = true;
            boekingVM.Object.Date = DateTime.Today.AddDays(1);
            boekingVM.Object.SelectedBeestjes.Add(beest.Object);

            // Act
            ViewResult result = controller.Stap2(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Stap2NoBeestjesSelected()
        {
            // Arrange
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);

            // Act         
            var result = (RedirectToRouteResult)controller.Stap2(boekingVM.Object);

            // Assert
            result.RouteValues["action"].Equals("Stap1");
            result.RouteValues["controller"].Equals("Home");

            // Assert
            Assert.AreEqual("Stap1", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }


        [TestMethod]
        public void Stap3()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoires()).Returns(new List<Accessoires>());
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);
            boekingVM.Object.BeestjesIds.Add(1);
            boekingVM.Object.Accessoires.Add(new Accessoires() {IsSelected = true});

            // Act
            ViewResult result = controller.Stap3(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Stap4()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjes()).Returns(new List<Beestje>());
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today.AddDays(1);

            // Act
            ViewResult result = controller.Stap4(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Finish()
        {
            // Arrange
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            // Act
            ViewResult result = controller.Finish(boekingVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BeestjeHasNoBoekingTrue()
        {
            // Arrange
            repo.Setup(x => x.GetAllBoeking()).Returns(new List<Boeking>());
            BeestValidation validation = new BeestValidation();
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> b = new Mock<Beestje>();

            // Act
            var result = validation.BeestjeHasNoBoeking(b.Object, boekingVM.Object, repo.Object);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BeestjeHasNoBoekingFalse()
        {
            // Arrange
            Mock<Beestje> b = new Mock<Beestje>();
            b.Object.Id = 1;
            Mock<Boeking> boeking = new Mock<Boeking>();
            boeking.Object.Date = DateTime.Today;
            boeking.Object.Beestjes.Add(b.Object);
            
            repo.Setup(x => x.GetAllBoeking()).Returns(new List<Boeking>(){boeking.Object});

            BeestValidation validation = new BeestValidation();
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today;

            // Act
            var result = validation.BeestjeHasNoBoeking(b.Object, boekingVM.Object, repo.Object);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
