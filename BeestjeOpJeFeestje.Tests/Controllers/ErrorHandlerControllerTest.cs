using BeestjeOpJeFeestje.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class ErrorHandlerControllerTest
    {
        [TestMethod]
        public void Index()
        {
            ErrorHandlerController controller = new ErrorHandlerController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void NotFound()
        {
            ErrorHandlerController controller = new ErrorHandlerController();

            // Act
            ViewResult result = controller.NotFound() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
