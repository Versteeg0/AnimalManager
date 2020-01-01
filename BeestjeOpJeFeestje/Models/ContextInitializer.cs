using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            context.Beestjes.Add(new Beestje() { Id = 1, Name = "Aap", Type = "Jungle", Price = 5.50m, ImagePath = "~/Resources/aap.png" });
            context.Beestjes.Add(new Beestje() { Id = 2, Name = "Olifant", Type = "Jungle", Price = 800.50m, ImagePath = "~/Resources/olifant.png" });
            context.Beestjes.Add(new Beestje() { Id = 3, Name = "Zebra", Type = "Jungle", Price = 506.50m, ImagePath = "~/Resources/zebra.png" });
            context.Beestjes.Add(new Beestje() { Id = 4, Name = "Leeuw", Type = "Jungle", Price = 9999.50m, ImagePath = "~/Resources/leeuw.png" });
            context.Beestjes.Add(new Beestje() { Id = 5, Name = "Hond", Type = "Boerderij", Price = 15.50m, ImagePath = "~/Resources/doggo.png" });
            context.Beestjes.Add(new Beestje() { Id = 6, Name = "Ezel", Type = "Boerderij", Price = 5.50m, ImagePath = "~/Resources/donkey.png" });
            context.Beestjes.Add(new Beestje() { Id = 7, Name = "Koe", Type = "Boerderij", Price = 567.00m, ImagePath = "~/Resources/koe.png" });
            context.Beestjes.Add(new Beestje() { Id = 8, Name = "Eend", Type = "Boerderij", Price = 1.00m, ImagePath = "~/Resources/duck.png" });
            context.Beestjes.Add(new Beestje() { Id = 9, Name = "Kuiken", Type = "Boerderij", Price = 80.52m, ImagePath = "~/Resources/kuiken.png" });
            context.Beestjes.Add(new Beestje() { Id = 10, Name = "Pinguïn", Type = "Sneeuw", Price = 780.50m, ImagePath = "~/Resources/pingwing.png" });
            context.Beestjes.Add(new Beestje() { Id = 11, Name = "Ijsbeer", Type = "Sneeuw", Price = 5.50m, ImagePath = "~/Resources/ijsbeer.png" });
            context.Beestjes.Add(new Beestje() { Id = 12, Name = "Zeehond", Type = "Sneeuw", Price = 69.50m, ImagePath = "~/Resources/zeehond.png" });
            context.Beestjes.Add(new Beestje() { Id = 13, Name = "Kat", Type = "Woestijn", Price = 80.30m, ImagePath = "~/Resources/kat.png" });

            context.Accessoires.Add(new Accessoires() { Id = 1, Name = "Banaan", Price = 2.00m, Beest = context.Beestjes.Find(1)});
            context.Accessoires.Add(new Accessoires() { Id = 2, Name = "Zadel", Price = 2.00m, Beest = context.Beestjes.Find(3)});
            context.Accessoires.Add(new Accessoires() { Id = 3, Name = "Krukje", Price = 2.00m, Beest = context.Beestjes.Find(4)});
            context.Accessoires.Add(new Accessoires() { Id = 4, Name = "Zweep", Price = 2.00m, Beest = context.Beestjes.Find(4)});
            context.Accessoires.Add(new Accessoires() { Id = 5, Name = "Bal", Price = 2.00m, Beest = context.Beestjes.Find(5)});
            context.Accessoires.Add(new Accessoires() { Id = 5, Name = "Dansschoenen", Price = 2.00m, Beest = context.Beestjes.Find(10)});
            context.Accessoires.Add(new Accessoires() { Id = 5, Name = "Bal", Price = 2.00m, Beest = context.Beestjes.Find(12)});
          
            context.SaveChanges();
        }
    }
}