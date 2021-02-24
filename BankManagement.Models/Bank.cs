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


        public List<Customer> AccountsList;
        public List<BankStaff> StaffList;
        public List<Currency> CurrenyList;

    }
}
