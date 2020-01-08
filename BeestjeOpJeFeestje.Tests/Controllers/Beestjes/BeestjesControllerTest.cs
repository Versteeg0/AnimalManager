using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class BeestjesControllerTest
    {
        private Mock<IBeestjesRepository> repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Mock<IBeestjesRepository>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjes()).Returns(new List<Beestje>());
            BeestjesController controller = new BeestjesController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjeById(1)).Returns(new Beestje());
            BeestjesController controller = new BeestjesController(repo.Object);

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateBeestWrongType()
        {
            // Arrange
            BeestjesController controller = new BeestjesController(repo.Object);
            Mock<BeestjeVM> beestjeVM = new Mock<BeestjeVM>();
            beestjeVM.Object.Type = "Test";
            // Act
            var result = controller.Create(beestjeVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateBeest()
        {
            // Arrange
            BeestjesController controller = new BeestjesController(repo.Object);
            Mock<BeestjeVM> beestjeVM = new Mock<BeestjeVM>();
            beestjeVM.Object.Type = "Boerderij";
            // Act
            var result = (RedirectToRouteResult)controller.Create(beestjeVM.Object);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Beestjes");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Beestjes", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjeById(1)).Returns(new Beestje());
            BeestjesController controller = new BeestjesController(repo.Object);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditBeestWrongType()
        {
            // Arrange
            BeestjesController controller = new BeestjesController(repo.Object);
            Mock<BeestjeVM> beestjeVM = new Mock<BeestjeVM>();
            beestjeVM.Object.Type = "Test";
            // Act
            var result = controller.Create(beestjeVM.Object) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjeById(1)).Returns(new Beestje());
            BeestjesController controller = new BeestjesController(repo.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            // Arrange
            repo.Setup(x => x.GetBeestjeById(1)).Returns(new Beestje());
            BeestjesController controller = new BeestjesController(repo.Object);

            // Act
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Beestjes");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Beestjes", result.RouteValues["controller"]);
        }
    }
}
