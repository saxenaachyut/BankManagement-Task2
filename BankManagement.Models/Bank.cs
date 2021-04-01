using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Bank
    {
        public Bank()
        {
            this.Accounts = new List<AccountHolder>();
            this.Employees = new List<BankStaff>();
            this.Currencies = new List<Currency>();
            this.ServiceChargeRates = new ServiceChargeRates();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ServiceChargeRates ServiceChargeRates {get; set;}

        public List<AccountHolder> Accounts { get; set; }

        public List<BankStaff> Employees { get; set; }
        
        public List<Currency> Currencies { get; set; }
    }
}
