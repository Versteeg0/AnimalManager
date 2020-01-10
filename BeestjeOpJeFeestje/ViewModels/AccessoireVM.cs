using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.ViewModels
{
    public class AccessoireVM
    {
        public Accessoires Accessoire { get; set; } = new Accessoires();

        public int Id { get { return Accessoire.Id; } set { Accessoire.Id = value; } }

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get { return Accessoire.Name; } set { Accessoire.Name = value; } }

        [Display(Name = "Prijs")]
        [Required(ErrorMessage = "Prijs is verplicht")]
        [Range(typeof(decimal), "0.01", "100000.00", ErrorMessage = "Vul een decimale waarde in")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Vul een decimale waarde in")]
        public decimal Price { get { return Accessoire.Price; } set { Accessoire.Price = value; } }

        [Display(Name = "Foto link")]
        public string ImagePath { get { return Accessoire.ImagePath; } set { Accessoire.ImagePath = value; } }
        public bool IsSelected { get { return Accessoire.IsSelected; } set { Accessoire.IsSelected = value; } }

        [Display(Name = "Selecteer een beestje")]
        [Required(ErrorMessage = "Selecteer een beestje")]
        public int SelectedBeestjesId { get; set; }
        public IEnumerable<Beestje> BeestjesLijst { get; set; }
        public Beestje Beest { get { return Accessoire.Beest; } set { Accessoire.Beest = value; } }
    }
}