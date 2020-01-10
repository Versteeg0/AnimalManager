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
            ErrorController controller = new ErrorController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NotFound()
        {
            ErrorController controller = new ErrorController();

            // Act
            ViewResult result = controller.PageNotFound() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
