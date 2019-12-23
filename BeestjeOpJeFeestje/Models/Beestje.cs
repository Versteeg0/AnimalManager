using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class Beestje
    {
        [Key]
        public int Id { get; set; }
       
        [Required(ErrorMessage ="Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
      
        [Required(ErrorMessage = "Type is verplicht")]
        [Display(Name = "Type")]
        public string Type { get; set; }
      
        [Required(ErrorMessage = "Prijs is verplicht")]
        [Range(1, int.MaxValue, ErrorMessage ="Vul een waarde in die groter is dan 0")]
        [Display(Name = "Prijs")]
        public double Price { get; set; }
       
        [Required(ErrorMessage = "Vul hier het pad in van de foto")]
        [Display(Name = "Foto link")]
        public string imagePath { get; set; }

        public ICollection<Accessoires> AccessoireList { get; set; }

    }
}