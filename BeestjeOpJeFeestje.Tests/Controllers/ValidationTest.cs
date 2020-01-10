using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.Validation;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class ValidationTest
    {
        private Mock<IBoekingRepository> repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Mock<IBoekingRepository>();
        }

        [TestMethod]
        public void Stap1HasNoDate()
        {
            // Arrange
            HomeController controller = new HomeController(repo.Object);
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            // Act
            var result = (RedirectToRouteResult)controller.Stap1(boekingVM.Object);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Home");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void PolarBearOrLionOnFarm()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> ijsbeer = new Mock<Beestje>();
            Mock<Beestje> eend = new Mock<Beestje>();

            ijsbeer.Object.Name = "Ijsbeer";
            eend.Object.Type = "Boerderij";

            boekingVM.Object.Date = DateTime.Now;
            boekingVM.Object.SelectedBeestjes.Add(ijsbeer.Object);
            boekingVM.Object.SelectedBeestjes.Add(eend.Object);

            BeestValidation validation = new BeestValidation();
    
            // Act
            var result = validation.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag geen leeuw of Ijsbeer bij boerderijdieren.", result);
        }

        [TestMethod]
        public void PinguinInWeekend()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> pinguin = new Mock<Beestje>();

            boekingVM.Object.Date = new DateTime(2020, 1, 5);
            pinguin.Object.Name = "Pinguïn";
            boekingVM.Object.SelectedBeestjes.Add(pinguin.Object);

            BeestValidation validation = new BeestValidation();
           
            // Act
            var result = validation.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag helaas geen pinguïns reserveren in het weekend.", result);
        }

        [TestMethod]
        public void DesertInWinter()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> kameel = new Mock<Beestje>();
            boekingVM.Object.Date = new DateTime(2020, 1, 5);
            kameel.Object.Type = "Woestijn";
            boekingVM.Object.SelectedBeestjes.Add(kameel.Object);

            BeestValidation validation = new BeestValidation();

            // Act
            var result = validation.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag helaas geen woestijn dieren reserveren in de maanden oktober t/m februari.", result);
        }

        [TestMethod]
        public void SnowInSummer()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> kameel = new Mock<Beestje>();
            boekingVM.Object.Date = new DateTime(2020, 7, 5);
            kameel.Object.Type = "Sneeuw";
            boekingVM.Object.SelectedBeestjes.Add(kameel.Object);

            BeestValidation validation = new BeestValidation();

            // Act
            var result = validation.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag helaas geen sneeuw dieren reserveren in de maanden juni t/m augustus.", result);
        }

    }
}
