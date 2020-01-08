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

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class BeestjesRepositoryTest
    {
        private Mock<IBeestjesRepository> repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Mock<IBeestjesRepository>();
        }
    }
}
