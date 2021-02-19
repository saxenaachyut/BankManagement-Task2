using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Currency
    {
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public double ExcahngeRate { get; set; }

        public Currency(string name, string currencyCode, double exchangeRate)
        {
            this.Name = name;
            this.CurrencyCode = currencyCode;
            this.ExcahngeRate = exchangeRate;
        }

    }
}
