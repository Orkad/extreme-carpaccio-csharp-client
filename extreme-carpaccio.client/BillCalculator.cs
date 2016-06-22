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
            {"DE",20 },
            {"UK",21 },
            {"FR",20 },
            {"IT",25 },

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
                reduction = (decimal)0.15;
            else if (total >= 10000)
                reduction = (decimal)0.1;
            else if (total >= 7000)
                reduction = (decimal)0.07;
            else if (total >= 5000)
                reduction = (decimal)0.05;
            else if (total >= 10000)
                reduction = (decimal)0.03;
            else
                reduction = 0;
            return total - reduction*total;
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
            bill.total *= taxeForCountry.Value;
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
