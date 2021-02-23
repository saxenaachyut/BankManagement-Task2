using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Customer : User
    {
        public string BankName { get; set; }
        public string AccountID { get; set; }
        public double TotalAmmount { get; set; }
        public List<Transactions> TransactionList;

        public Customer(string name, string userName, string password, string bankName)
        {
            this.Name = name;
            this.UserName = userName;
            this.Password = password;
            DateTime thisDay = DateTime.Today;
            this.AccountID = Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            this.TotalAmmount = 0;
            this.TransactionList = new List<Transactions>();
            this.BankName = bankName;
        }
    }
}
