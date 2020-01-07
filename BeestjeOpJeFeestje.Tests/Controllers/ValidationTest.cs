using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void IjsbeerofLeeuwBoerderij()
        {
            // Arrange
            Mock<IBoekingRepository> repo = new Mock<IBoekingRepository>();
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> ijsbeer = new Mock<Beestje>();
            Mock<Beestje> eend = new Mock<Beestje>();

            ijsbeer.Object.Name = "Ijsbeer";
            eend.Object.Type = "Boerderij";

            boekingVM.Object.Date = DateTime.Now;
            boekingVM.Object.SelectedBeestjes.Add(ijsbeer.Object);
            boekingVM.Object.SelectedBeestjes.Add(eend.Object);

            HomeController controller = new HomeController(repo.Object);
    
            // Act
            var result = controller.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag geen leeuw of Ijsbeer bij boerderijdieren.", result);
        }

        [TestMethod]
        public void PinguinInWeekend()
        {
            // Arrange
            Mock<IBoekingRepository> repo = new Mock<IBoekingRepository>();
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();
            Mock<Beestje> pinguin = new Mock<Beestje>();

            boekingVM.Object.Date = new DateTime(2020, 1, 5);
            pinguin.Object.Name = "Pinguïn";
            boekingVM.Object.SelectedBeestjes.Add(pinguin.Object);

            HomeController controller = new HomeController(repo.Object);
           
            // Act
            var result = controller.CheckIfSelectedBeestjesAreValid(boekingVM.Object);

            // Assert
            Assert.AreEqual("Je mag helaas geen pinguïns reserveren in het weekend.", result);
        }
    }
}
