using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Discount;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class DiscountTest
    {
        [TestMethod]
        public void ThreeOfAKind()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            
            Mock<Beestje> animal1 = new Mock<Beestje>();
            Mock<Beestje> animal2 = new Mock<Beestje>();
            Mock<Beestje> animal3 = new Mock<Beestje>();

            // If these are not initialized correctly, automatically takes the 2% discount
            animal1.Object.Name = "b";
            animal2.Object.Name = "c";
            animal3.Object.Name = "d";

            animal1.Object.Price = 10.00m;
            animal2.Object.Price = 10.00m;
            animal3.Object.Price = 10.00m;

            animal1.Object.Type = "Boerderij";
            animal2.Object.Type = "Boerderij";
            animal3.Object.Type = "Boerderij";

            // If no date is selected, takes date of today which could be Monday/Tuesday
            boekingVM.Object.Date = new DateTime(2020, 1, 8);

            boekingVM.Object.SelectedBeestjes.Add(animal1.Object);
            boekingVM.Object.SelectedBeestjes.Add(animal2.Object);
            boekingVM.Object.SelectedBeestjes.Add(animal3.Object);

            CalculateDiscount discount = new CalculateDiscount();

            // Act
            var result = discount.CalculateTotalPrice(boekingVM.Object);

            // Assert
            Assert.AreEqual(27.00m, result);
        }

        [TestMethod]
        public void MondayOrTuesday()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            Mock<Beestje> animal1 = new Mock<Beestje>();

            // If these are not initialized correctly, automatically takes the 2% discount
            animal1.Object.Name = "b";

            animal1.Object.Price = 10.00m;

            animal1.Object.Type = "Boerderij";

            // Datetime has to be Monday/Tuesday
            boekingVM.Object.Date = new DateTime(2020, 1, 6);

            boekingVM.Object.SelectedBeestjes.Add(animal1.Object);

            CalculateDiscount discount = new CalculateDiscount();

            // Act
            var result = discount.CalculateTotalPrice(boekingVM.Object);

            // Assert
            Assert.AreEqual(8.50m, result);
        }

        [TestMethod]
        public void OneInSixDucks()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            Mock<Beestje> animal1 = new Mock<Beestje>();

            // If these are not initialized correctly, automatically takes the 2% discount
            animal1.Object.Name = "Eend";

            animal1.Object.Price = 10.00m;

            animal1.Object.Type = "Boerderij";

            // If no date is selected, takes date of today which could be Monday/Tuesday
            boekingVM.Object.Date = new DateTime(2020, 1, 8);

            boekingVM.Object.SelectedBeestjes.Add(animal1.Object);

            CalculateDiscount discount = new CalculateDiscount();

            // Act
            var result = discount.CalculateTotalPrice(boekingVM.Object);

            // Assert
            Assert.AreEqual(5.00m, result);
        }

        [TestMethod]
        public void ContainsCharStartingFromA()
        {
            // Arrange
            Mock<BoekingVM> boekingVM = new Mock<BoekingVM>();

            Mock<Beestje> animal1 = new Mock<Beestje>();

            // If these are not initialized correctly, automatically takes the 2% discount
            // In this instance we take Baviaan for the 4% discount
            animal1.Object.Name = "Baviaan";

            animal1.Object.Price = 10.00m;

            animal1.Object.Type = "Jungle";

            // If no date is selected, takes date of today which could be Monday/Tuesday
            boekingVM.Object.Date = new DateTime(2020, 1, 8);

            boekingVM.Object.SelectedBeestjes.Add(animal1.Object);

            CalculateDiscount discount = new CalculateDiscount();

            // Act
            var result = discount.CalculateTotalPrice(boekingVM.Object);

            // Assert
            Assert.AreEqual(9.60m, result);
        }

    }
}
