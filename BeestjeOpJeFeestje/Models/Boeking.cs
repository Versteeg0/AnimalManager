using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class Boeking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht")]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Display(Name = "Tussenvoegsel")]
        public string Prefix { get; set; }
       
        [Required(ErrorMessage = "Achternaam is verplicht")]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
       
        [Required(ErrorMessage ="Email is verplicht")]
        [EmailAddress(ErrorMessage ="Dit is geen geldig email adres")]
        public string Email { get; set; }

        public List<Beestje> beestjes { get; set; }
    }
}