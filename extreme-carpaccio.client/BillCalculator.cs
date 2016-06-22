using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xCarpaccio.client
{
    public class BillCalculator
    {
        /// <summary>
        /// Association des codes pays avec leurs taxe 
        /// </summary>
        private static readonly Dictionary<string, decimal?> _countryTaxesAssoc = new Dictionary<string, decimal?>
        {
            {"DE",0.20m },
            {"UK",0.21m },
            {"FR",0.20m },
            {"IT",0.25m },
            {"ES",0.19m },
            {"PL",0.21m },
            {"RO",0.20m },
            {"NL",0.20m },
            {"BE",0.24m },
            {"EL",0.20m },
            {"CZ",0.19m },
            {"PT",0.23m },
            {"HU",0.27m },
            {"SE",0.23m },
            {"AT",0.22m },
            {"BG",0.21m },
            {"DK",0.21m },
            {"FI",0.17m },
            {"SK",0.18m },
            {"IE",0.21m },
            {"HR",0.23m },
            {"LT",0.23m },
            {"SI",0.23m },
            {"LV",0.20m },
            {"EE",0.22m },
            {"CY",0.21m },
            {"LU",0.25m },
            {"MT",0.20m }
        };

        /// <summary>
        /// Permet de récupérer la taxe en fonction du code du pays (Si le pays n'est pas gérré return null)
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static decimal? GetTaxeForCountry(string countryCode)
        {
            if (_countryTaxesAssoc.ContainsKey(countryCode))
                return _countryTaxesAssoc[countryCode];
            return null;
        }

        /// <summary>
        /// Obtient 
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static decimal ApplyReduction(decimal total)
        {
            decimal reduction;
            if (total >= 50000)
                reduction = 0.15m;
            else if (total >= 10000)
                reduction = 0.1m;
            else if (total >= 7000)
                reduction = 0.07m;
            else if (total >= 5000)
                reduction = 0.05m;
            else if (total >= 1000)
                reduction = 0.03m;
            else
                reduction = 0;
            return total - reduction* total;
        }

        /// <summary>
        /// Genere un objet bill
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Bill Calculate(Order order)
        {
            var bill = new Bill();
            var prices = order.Prices;
            var quantities = order.Quantities;
            if (prices.Length != quantities.Length)
                return null;
            for (int i = 0; i < prices.Length; i++)
            {
                bill.total += prices[i] * quantities[i];
            }
            var taxeForCountry = GetTaxeForCountry(order.Country);
            if (taxeForCountry == null)
                return null;
            bill.total += bill.total * taxeForCountry.Value;
            if(order.Reduction != "PAY THE PRICE")
                bill.total = ApplyReduction(bill.total);
            return bill;
        }

        /// <summary>
        /// Vérifie un objet order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool ValidOrder(Order order)
        {
            if (GetTaxeForCountry(order.Country) == null)
                return false;
            if (order.Prices.Length != order.Quantities.Length)
                return false;
            return true;
        }
    }
}
