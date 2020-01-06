﻿using BeestjeOpJeFeestje.Models;
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

            int counterWoestijnType = 0;
            int counterSneeuwType = 0;
            int counterBoerderijType = 0;
            int counterJungleType = 0;

            foreach (Beestje b in boekingVM.SelectedBeestjes)
            {
                decimal beestprice = b.Price;

                if (boekingVM.SelectedBeestjes.Count > 2)
                {
                    if (b.Type == "Woestijn")
                    {
                        counterWoestijnType++;
                    }
                    else if (b.Type == "Sneeuw")
                    {
                        counterSneeuwType++;
                    }
                    else if (b.Type == "Boerderij")
                    {
                        counterBoerderijType++;
                    }
                    else
                    {
                        counterJungleType++;
                    }
                }

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

                    totalPrice = b.Price;
            }

            if(counterBoerderijType > 2 || counterJungleType > 2 || counterSneeuwType > 2 || counterWoestijnType > 2)
            {
                DiscountList.Add("3 Types: 10%");
                discountAmount += 10;
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