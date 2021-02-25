using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Bank
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public double SameBankRTGS { get; set; }
        public double SameBankIMPS { get; set; }
        public double OtherBankRTGS { get; set; }
        public double OtherBankIMPS { get; set; }
        public Currency DefaultCurrency { get; set; }


        public List<AccountHolder> AccountsList { get; set; }
        public List<BankStaff> StaffList { get; set; }
        public List<Currency> CurrenyList { get; set; }

        public Bank()
        {
            this.AccountsList = new List<AccountHolder>();
            this.StaffList = new List<BankStaff>();
            this.CurrenyList = new List<Currency>();
            Currency defaultCurrency = new Currency();
            defaultCurrency.CurrencyCode = "INR";
            defaultCurrency.Name = "Indian National Rupee";
            defaultCurrency.ExcahngeRate = 0;
            this.CurrenyList.Add(defaultCurrency);
        }

    }
}
