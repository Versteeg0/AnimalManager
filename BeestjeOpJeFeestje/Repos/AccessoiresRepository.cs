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
            db.Accessoires.Add(accessoire);
            db.SaveChanges();
        }

        public void EditAccessoire(AccessoireVM accessoireVM)
        {
            Accessoires accessoire = db.Accessoires.First(a => a.Id == accessoireVM.Id);
            accessoire.Name = accessoireVM.Name;
            accessoire.Price = accessoireVM.Price;
            accessoire.ImagePath = accessoireVM.ImagePath;
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