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

namespace BeestjeOpJeFeestje.Tests.Controllers
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


    }
}
