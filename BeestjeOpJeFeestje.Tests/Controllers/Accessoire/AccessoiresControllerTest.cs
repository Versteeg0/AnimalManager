using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Tests.Controllers.Accessoire
{
    [TestClass]
    public class AccessoiresControllerTest
    {
        private Mock<IAccessoiresRepository> repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Mock<IAccessoiresRepository>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoires()).Returns(new List<Accessoires>());
            AccessoiresController controller = new AccessoiresController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoireById(1)).Returns(new Accessoires());
            AccessoiresController controller = new AccessoiresController(repo.Object);

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreateAccessoire()
        {
            // Arrange
            AccessoiresController controller = new AccessoiresController(repo.Object);
            Mock<AccessoireVM> accessoireVM = new Mock<AccessoireVM>();
            // Act
            var result = (RedirectToRouteResult)controller.Create(accessoireVM.Object);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Accessoires");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Accessoires", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            AccessoiresController controller = new AccessoiresController(repo.Object);
            // Act
            var result = controller.Create();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoireById(1)).Returns(new Accessoires());
            AccessoiresController controller = new AccessoiresController(repo.Object);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoireById(1)).Returns(new Accessoires());
            AccessoiresController controller = new AccessoiresController(repo.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            // Arrange
            repo.Setup(x => x.GetAccessoireById(1)).Returns(new Accessoires());
            AccessoiresController controller = new AccessoiresController(repo.Object);

            // Act
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            result.RouteValues["action"].Equals("Index");
            result.RouteValues["controller"].Equals("Accessoires");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Accessoires", result.RouteValues["controller"]);
        }
    }
}

