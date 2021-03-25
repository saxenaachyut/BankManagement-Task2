using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class ServiceChargeRates
    {
        public double SameBankRTGS { get; set; }

        public double SameBankIMPS { get; set; }

        public double OtherBankRTGS { get; set; }

        public double OtherBankIMPS { get; set; }   

        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
