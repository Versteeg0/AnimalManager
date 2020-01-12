using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.ViewModels
{
    public class BeestjeVM
    {
        public Beestje Beest { get; set; } = new Beestje();

        public int Id { get { return Beest.Id; } set { Beest.Id = value; } }

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get { return Beest.Name; } set { Beest.Name = value; } }

        [Required(ErrorMessage = "Type is verplicht")]
        [Display(Name ="Type")]
        public string Type { get { return Beest.Type; } set { Beest.Type = value; } }

        [DisplayFormat(DataFormatString = "€ {0:n}")]
        [Required(ErrorMessage = "Prijs is verplicht")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0.01, 9999999999999999.99)]
        [Display(Name = "Prijs")]
        public decimal Price { get { return Beest.Price; } set { Beest.Price = value; } }

        [Required(ErrorMessage = "Vul hier het pad in van de foto")]
        [Display(Name = "Foto link")]
        public string ImagePath { get { return Beest.ImagePath; } set { Beest.ImagePath = value; } }

        public bool IsSelected { get { return Beest.IsSelected; } set { Beest.IsSelected = value; } }
        
        public List<Accessoires> AccessoiresList { get; set; } = new List<Accessoires>();

        public bool HasBoeking { get; set; }

        public List<Boeking> AllBoekingen { get; set; } = new List<Boeking>();
    }
}