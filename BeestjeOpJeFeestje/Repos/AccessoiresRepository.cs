using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public class AccessoiresRepository : IAccessoiresRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Accessoires> GetAccessoires()
        {
            return db.Accessoires.ToList();
        }
        public List<Beestje> GetBeestjes()
        {
            return db.Beestjes.ToList();
        }

        public Accessoires GetAccessoireById(int? id)
        {
            return db.Accessoires.Find(id);
        }

        public void CreateAccessoire(AccessoireVM model)
        {
            Accessoires accessoire = new Accessoires();
            accessoire.Name = model.Name;
            accessoire.Price = model.Price;
            accessoire.ImagePath = model.ImagePath;
            accessoire.Beest = db.Beestjes.Find(model.SelectedBeestjesId);
            db.Accessoires.Add(accessoire);
            db.SaveChanges();
        }

        public void EditAccessoire(AccessoireVM model)
        {
            Accessoires accessoire = db.Accessoires.First(a => a.Id == model.Id);
            accessoire.Name = model.Name;
            accessoire.Price = model.Price;
            accessoire.ImagePath = model.ImagePath;
            accessoire.Beest = db.Beestjes.Find(model.SelectedBeestjesId);
            db.Entry(accessoire).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteAccessoire(Accessoires accessoire)
        {
            db.Accessoires.Remove(accessoire);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}