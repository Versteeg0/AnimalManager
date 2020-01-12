using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Repos;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Validation
{
    public class BeestValidation
    {
        /**
         * This method checks if the animals that are selected by the user are following the rules
         * It first checks if the animals are from a specific type or have a specific name
         * After this it checks several combinations to see if it violates any rules. 
         * If it does not violate any rules this method returns null
         */
        public string CheckIfSelectedBeestjesAreValid(BoekingVM boeking)
        {
            bool isFarmAnimal = false;
            bool isLionorPolar = false;
            bool isPinguin = false;
            bool isDesert = false;
            bool isSnow = false;
            foreach (Beestje b in boeking.SelectedBeestjes)
            {
                if (b.Type == "Boerderij")
                    isFarmAnimal = true;

                if (b.Name == "Leeuw" || b.Name == "Ijsbeer")
                    isLionorPolar = true;

                if (b.Name == "Pinguïn")
                    isPinguin = true;

                if (b.Type == "Woestijn")
                    isDesert = true;

                if (b.Type == "Sneeuw")
                    isSnow = true;
            }

            if (isFarmAnimal && isLionorPolar)
                return "Je mag geen leeuw of Ijsbeer bij boerderijdieren.";

            DayOfWeek day = boeking.Date.DayOfWeek;
            if (isPinguin && ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday)))
                return "Je mag helaas geen pinguïns reserveren in het weekend.";

            if (isDesert && (boeking.Date.Month > 9 || boeking.Date.Month < 3))
                return "Je mag helaas geen woestijn dieren reserveren in de maanden oktober t/m februari.";

            if (isSnow && (boeking.Date.Month > 5 && boeking.Date.Month < 9))
                return "Je mag helaas geen sneeuw dieren reserveren in de maanden juni t/m augustus.";

            return null;
        }

        /**
         * Check if the animal has a booking on the same date as the current booking.
         */
        public bool BeestjeHasNoBoeking(Beestje b, BoekingVM currentBoeking, IBoekingRepository boekingsRepository)
        {
            foreach (Boeking boeking in boekingsRepository.GetAllBoeking())
            {
                if (boeking.Beestjes != null && boeking.Beestjes.FirstOrDefault(beest => beest.Id == b.Id) != null && boeking.Date == currentBoeking.Date)
                {
                    return false;
                }
            }
            return true;
        }
    }
}