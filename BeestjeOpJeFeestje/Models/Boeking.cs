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

        public DateTime Date { get; set; }

        public string FirstName { get; set; }

        public string Prefix { get; set; }
       
        public string LastName { get; set; }
       
        public string Email { get; set; }

        public ICollection<Beestje> Beestjes { get; set; } = new List<Beestje>();

        public ICollection<Accessoires> Accessoires { get; set; }
    }
}