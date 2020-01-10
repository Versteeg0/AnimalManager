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
            Assert.IsNotNull(controller.ViewBag);
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
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> b = new Mock<Beestje>();

            // Act
            var result = controller.BeestjeHasNoBoeking(b.Object, boekingVM.Object);

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
            
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            boekingVM.Object.Date = DateTime.Today;

            // Act
            var result = controller.BeestjeHasNoBoeking(b.Object, boekingVM.Object);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
