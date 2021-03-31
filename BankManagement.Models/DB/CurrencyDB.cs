using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class CurrencyDB
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CurrencyCode { get; set; }

        public double ExcahngeRate { get; set; }

        public bool IsDefault { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }

    }
}
