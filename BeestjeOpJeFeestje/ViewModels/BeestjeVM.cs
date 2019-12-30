﻿using BeestjeOpJeFeestje.Models;
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

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get ; set; }

        [Required(ErrorMessage = "Type is verplicht")]
        [Display(Name ="Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Prijs is verplicht")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Display(Name = "Prijs")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vul hier het pad in van de foto")]
        [Display(Name = "Foto link")]
        public string imagePath { get; set; }

        public List<Accessoires> AccessoiresList { get; set; } = new List<Accessoires>();
        public int[] AccessoireIds { get; set; }
    }
}