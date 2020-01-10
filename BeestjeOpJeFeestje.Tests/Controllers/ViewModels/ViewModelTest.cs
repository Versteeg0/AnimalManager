using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Tests.Controllers.ViewModels
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void BeestjeVMTest()
        {
            BeestjeVM beest = new BeestjeVM();
            beest.Id = 1;
            beest.Type = "Boerderij";
            beest.Price = 80;
            beest.HasBoeking = true;
            beest.IsSelected = true;
            beest.ImagePath = "123";
            beest.AccessoiresList = new List<Accessoires>();

            var result = beest;

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Type, "Boerderij");
            Assert.AreEqual(result.Price, 80);
            Assert.AreEqual(result.HasBoeking, true);
            Assert.AreEqual(result.IsSelected, true);
            Assert.AreEqual(result.ImagePath, "123");
            Assert.IsNotNull(result.AccessoiresList);
        }

        [TestMethod]
        public void BoekingVMTest()
        {
            BoekingVM boeking = new BoekingVM();
            boeking.FirstName = "Unit";
            boeking.Prefix = "T";
            boeking.LastName = "Test";
            boeking.FullName = "Unit T Test";
            boeking.Number = "123";
            boeking.Email = "Test@test.com";
            boeking.TotalPrice = 69;
            boeking.SelectedAccessoires.Add(new Accessoires());
            boeking.SelectedBeestjes.Add(new Beestje());
            boeking.Beestjes.Add(new BeestjeVM());
            boeking.Accessoires.Add(new Accessoires());
            boeking.BeestjesIds.Add(1);
            boeking.AccessoiresIds.Add(1);

            var result = boeking;

            Assert.AreEqual(result.FirstName,"Unit");
            Assert.AreEqual(result.Prefix,"T");
            Assert.AreEqual(result.LastName, "Test");
            Assert.AreEqual(result.FullName, "Unit T Test");
            Assert.AreEqual(result.Number, "123");
            Assert.AreEqual(result.TotalPrice, 69);
            Assert.AreEqual(result.Email, "Test@test.com");
            Assert.AreEqual(result.SelectedAccessoires.Count, 1);
            Assert.AreEqual(result.SelectedBeestjes.Count, 1);
            Assert.AreEqual(result.Beestjes.Count, 1);
            Assert.AreEqual(result.Accessoires.Count, 1);
            Assert.AreEqual(result.BeestjesIds.Count, 1);
            Assert.AreEqual(result.AccessoiresIds.Count, 1);    
        }

        [TestMethod]
        public void AccessoireVMTest()
        {
            AccessoireVM accessoire = new AccessoireVM();
            accessoire.Id = 1;
            accessoire.Name = "Test";
            accessoire.Price = 80;
            accessoire.IsSelected = true;
            accessoire.ImagePath = "123";
            accessoire.Beest = new Beestje() {Id = 1 };

            var result = accessoire;

            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Name, "Test");
            Assert.AreEqual(result.Price, 80);
            Assert.AreEqual(result.IsSelected, true);
            Assert.AreEqual(result.ImagePath, "123");
            Assert.AreEqual(result.Beest.Id, 1);
        }
    }
}
