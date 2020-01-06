using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Discount
{
    public class CalculateDiscount
    {

        public List<string> DiscountList { get; set; } = new List<string>();

        public decimal CalculateTotalPrice(BoekingVM boekingVM)
        {
            DiscountList.Clear();
            decimal totalPrice = 0;
            List<string> kortinglijst = new List<string>();
            int discountAmount = 0;

            foreach (Beestje b in boekingVM.SelectedBeestjes)
            {
                decimal beestprice = b.Price;

                if (b.Name == "Eend")
                {
                    Random r = new Random();
                    int korting = r.Next(0, 7);

                    if (korting == 1)
                    {
                        //beestprice =- b.Price / 2;
                        DiscountList.Add("Eend 50%");
                        discountAmount += 50;
                    }
                }
                DayOfWeek day = boekingVM.Date.DayOfWeek;
                if (day == DayOfWeek.Monday || day == DayOfWeek.Tuesday)
                {
                    //totalprice = b.Price - ((b.Price / 100) * 15);
                    DiscountList.Add("Maandag en Dinsdag korting 15%");
                    discountAmount += 15;
                }

                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (b.Name.ToLower().Contains(c))
                    {
                        DiscountList.Add("Bevat letter " + c + " 2%");
                        discountAmount += 2;
                    }
                    else
                        break;
                }

                totalPrice += b.Price;
            }

            if (boekingVM.SelectedBeestjes.Count > 2)
            {
                int counterWoestijnType = boekingVM.SelectedBeestjes.FindAll(b => b.Type == "Woestijn").Count();
                int counterSneeuwType = boekingVM.SelectedBeestjes.FindAll(b => b.Type == "Sneeuw").Count();
                int counterBoerderijType = boekingVM.SelectedBeestjes.FindAll(b => b.Type == "Boerderij").Count();
                int counterJungleType = boekingVM.SelectedBeestjes.FindAll(b => b.Type == "Jungle").Count();

                if (counterBoerderijType > 2 || counterJungleType > 2 || counterSneeuwType > 2 || counterWoestijnType > 2)
                {
                    DiscountList.Add("3 Types: 10%");
                    discountAmount += 10;
                }
            }


            foreach (Accessoires a in boekingVM.SelectedAccessoires)
                totalPrice += a.Price;

            if (discountAmount > 60)
                discountAmount = 60;

            decimal discountPrice = (totalPrice / 100) * discountAmount;

            return totalPrice - discountPrice;
        }
    }
}