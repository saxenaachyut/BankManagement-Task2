using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Bank
    {
        public string ID { get; set; }  

        public string Name { get; set; }

        public double SameBankRTGS { get; set; }

        public double SameBankIMPS { get; set; } 

        public double OtherBankRTGS { get; set; }

        public double OtherBankIMPS { get; set; }

        public List<AccountHolder> Accounts { get; set; }

        public List<BankStaff> Employees { get; set; }
        
        public List<Currency> Currencies { get; set; }

        public Bank()
        {
            this.Accounts = new List<AccountHolder>();
            this.Employees = new List<BankStaff>();
            this.Currencies = new List<Currency>();
        }

    }
}
