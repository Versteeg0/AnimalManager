using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class Accessoires
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public Beestje Beest { get; set; }

        public List<Boeking>  BoekingLijst { get; set; }

        public bool IsSelected { get; set; }
    }
}