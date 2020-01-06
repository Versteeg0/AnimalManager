using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateDiscount
{
    public class Discount
    {
        private decimal CalculateTotalPrice(BoekingVM boekingVM)
        {
            decimal totalprice = 0;
            List<string> kortinglijst = new List<string>();

            foreach (Beestje b in boekingVM.SelectedBeestjes)
            {
                if (b.Name == "Eend")
                {
                    Random r = new Random();
                    int korting = r.Next(0, 7);

                    if (korting == 1)
                    {
                        totalprice = b.Price / 2;
                        kortinglijst.Add("Eend 50%");
                    }

                }
                DayOfWeek day = boekingVM.Date.DayOfWeek;
                if (day == DayOfWeek.Monday || day == DayOfWeek.Tuesday)
                    totalprice = b.Price - ((b.Price / 100) * 15);
            }

            foreach (Accessoires a in boekingVM.SelectedAccessoires)
                totalprice += a.Price;

            return totalprice;
        }
    }
}
