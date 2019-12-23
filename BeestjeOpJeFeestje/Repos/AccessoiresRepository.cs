using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public class AccessoiresRepository
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

        public void CreateAccessoire(Accessoires accessoire)
        {
            db.Accessoires.Add(accessoire);
            db.SaveChanges();
        }

        public void EditAccessoire(Accessoires accessoire)
        {
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