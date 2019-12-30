using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.ViewModels
{
    public class AccessoireVM
    {
        public Accessoires Accessoire { get; set; } = new Accessoires();

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get { return Accessoire.Name; } set { Accessoire.Name = value; } }

        [Required(ErrorMessage = "Prijs is verplicht")]
        [Range(1, int.MaxValue, ErrorMessage = "Vul een waarde in die groter is dan 0")]
        [Display(Name = "Prijs")]
        public decimal Price { get { return Accessoire.Price; } set { Accessoire.Price = value; } }

        [Required(ErrorMessage = "Vul hier het pad in van de foto")]
        [Display(Name = "Foto link")]
        public string ImagePath { get { return Accessoire.ImagePath; } set { Accessoire.ImagePath = value; } }

        public Beestje Beest { get { return Accessoire.Beest; } set { Accessoire.Beest = value; } }
    }
}