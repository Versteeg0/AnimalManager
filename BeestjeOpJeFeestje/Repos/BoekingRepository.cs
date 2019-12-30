using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public class BoekingRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Boeking> GetAllBoeking()
        {
            return db.Boekings.ToList();
        }

        public Boeking GetBoekingById(int? id)
        {
            return db.Boekings.Find(id);
        }

        public void AddBoeking(BoekingVM boekingVM)
        {
            Boeking boeking;

            boeking = new Boeking();
            db.Boekings.Add(boeking);

            boeking.FirstName = boekingVM.FirstName;
            boeking.LastName = boekingVM.LastName;
            boeking.Prefix = boekingVM.Prefix;
            boeking.Email = boekingVM.Email;
            boeking.Date = boekingVM.Date;

            boeking.Accessoires = boekingVM.AccesoiresIds.Select(ai => db.Accessoires.Find(ai)).ToList();
            boeking.Beestjes = boekingVM.BeestjesIds.Select(bi => db.Beestjes.Find(bi)).ToList();

            db.SaveChanges();
        }

        public void RemoveBoeking(Boeking boeking)
        {
            db.Boekings.Remove(boeking);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}