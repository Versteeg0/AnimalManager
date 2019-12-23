using BeestjeOpJeFeestje.Models;
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

        public void AddBoeking(Boeking boeking)
        {
            db.Boekings.Add(boeking);
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