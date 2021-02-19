using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Bank
    {
        public string Name { get; set; }
        public string BankID { get; set; }
        public double SameBankRTGS { get; set; }
        public double SameBankIMPS { get; set; }
        public double OtherBankRTGS { get; set; }
        public double OtherBankIMPS { get; set; }
        public Currency DefaultCurrency { get; set; }


        public List<User> AccountsList;

        public Bank(string bankName)
        {
            this.Name = bankName;
            DateTime thisDay = new DateTime();
            BankID = this.Name.Substring(0, 3) + thisDay.ToString("d");
            AccountsList = new List<User>();
            this.SameBankRTGS = 0;
            this.SameBankIMPS = 5;
            this.OtherBankRTGS = 2;
            this.OtherBankIMPS = 6;
            this.DefaultCurrency = new Currency("Indian National Rupee", "INR", 0);
        }

    }
}
