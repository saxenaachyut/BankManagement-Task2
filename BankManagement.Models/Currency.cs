using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Currency
    {
        public int ID { get; set; }
        public string Name { get; set; }
       
        public string CurrencyCode { get; set; }
        
        public double ExcahngeRate { get; set; }
        
        public bool IsDefault { get; set; }

    }
}
