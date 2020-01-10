using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Tests.Controllers.BoekingTest
{
    [TestClass]
    public class BoekingControllerTest
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
            repo.Setup(x => x.GetAllBoeking()).Returns(new List<Boeking>());
            BoekingsController controller = new BoekingsController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            repo.Setup(x => x.GetBoekingById(1)).Returns(new Boeking());
            BoekingsController controller = new BoekingsController(repo.Object);

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            repo.Setup(x => x.GetBoekingById(1)).Returns(new Boeking());
            BoekingsController controller = new BoekingsController(repo.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            // Arrange
            repo.Setup(x => x.GetBoekingById(1)).Returns(new Boeking());
            BoekingsController controller = new BoekingsController(repo.Object);

            // Act
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Boekings");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Boekings", result.RouteValues["controller"]);
        }

    }
}
