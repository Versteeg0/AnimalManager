using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public class BeestjesRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Beestje> GetBeestjes()
        {
            return db.Beestjes.ToList();
        }

        public Beestje GetBeestjeById(int? id)
        {
            return db.Beestjes.Find(id);
        }

        public void AddBeestje(Beestje beest)
        {
            db.Beestjes.Add(beest);
            db.SaveChanges();
        }

        public void EditBeestje(Beestje beest)
        {
            db.Entry(beest).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteBeestje(Beestje beest)
        {
            db.Beestjes.Remove(beest);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}