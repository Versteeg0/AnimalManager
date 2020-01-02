using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.ViewModels
{
    public class BoekingVM
    {
        public Boeking BoekingModel { get; set; } = new Boeking();
        public List<Accessoires> Accessoires { get; set; } = new List<Accessoires>();
       // public List<Beestje> Beestjes { get; set; } = new List<Beestje>();
        public List<BeestjeVM> Beestjes { get; set; } = new List<BeestjeVM>();

        public List<Beestje> SelectedBeestjes { get; set; } = new List<Beestje>();
        public List<Accessoires> SelectedAccessoires { get; set; } = new List<Accessoires>();

        [Required]
        [Display(Name = "Datum")]
     //   [DataType(DataType.Date)]
        public DateTime Date { get { return BoekingModel.Date; } set { BoekingModel.Date = value; } }

        [Required(ErrorMessage = "Voornaam is verplicht")]
        [Display(Name = "Voornaam")]
        public string FirstName { get { return BoekingModel.FirstName; } set { BoekingModel.FirstName = value; } }

        [Display(Name = "Tussenvoegsel")]
        public string Prefix { get { return BoekingModel.Prefix; } set { BoekingModel.Prefix = value; } }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        [Display(Name = "Achternaam")]
        public string LastName { get { return BoekingModel.LastName; } set { BoekingModel.LastName = value; } }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Dit is geen geldig email adres")]
        public string Email { get { return BoekingModel.Email; } set { BoekingModel.Email = value; } }
    }
}