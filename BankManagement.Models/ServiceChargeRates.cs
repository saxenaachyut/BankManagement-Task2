using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class ServiceChargeRates
    {
        public int Id { get; set; }
        public double SameBankRTGS { get; set; }

        public double SameBankIMPS { get; set; }

        public double OtherBankRTGS { get; set; }

        public double OtherBankIMPS { get; set; }   

        public string BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
