using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public class BeestjesRepository : IBeestjesRepository
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

        public void AddBeestje(BeestjeVM beest)
        {
            Beestje beestje = new Beestje();
            beestje.Name = beest.Name;
            beestje.Price = beest.Price;
            beestje.ImagePath = beest.ImagePath;
            beestje.Type = beest.Type;
            if(beest.AccessoireIds != null)
            beestje.AccessoireList = beest.AccessoireIds.Select(ai => db.Accessoires.Find(ai)).ToList();
            db.Beestjes.Add(beestje);
            db.SaveChanges();
        }

        public void EditBeestje(BeestjeVM beest)
        {
            Beestje beestje = db.Beestjes.First(b => b.Id == beest.Id);
            beestje.Name = beest.Name;
            beestje.Type = beest.Type;
            beestje.Price = beest.Price;
            beestje.ImagePath = beestje.ImagePath;
            if(beest.AccessoireIds != null)
            beestje.AccessoireList = beest.AccessoireIds.Select(ai => db.Accessoires.Find(ai)).ToList();
            db.Entry(beestje).State = EntityState.Modified;
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