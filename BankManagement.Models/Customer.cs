using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Customer : User
    {
        public string AccountID { get; set; }
        public double TotalAmmount { get; set; }

        public Customer(string name, string userName, string password)
        {
            this.Name = name;
            this.UserName = userName;
            this.Password = password;
            DateTime thisDay = DateTime.Today;
            this.AccountID = Name.Substring(0, 3) + thisDay.ToString("d");
            this.TotalAmmount = 0;

        }
    }
}
