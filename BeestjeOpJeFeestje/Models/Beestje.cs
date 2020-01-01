using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class Beestje
    {
        public Beestje()
        {
            this.AccessoireList = new List<Accessoires>();
        }

        [Key]
        public int Id { get; set; }
       
        public string Name { get; set; }
      
        public string Type { get; set; }
      
        public decimal Price { get; set; }
       
        public string ImagePath { get; set; }

        public bool IsSelected { get; set; }

        public ICollection<Accessoires> AccessoireList { get; set; }

    }
}