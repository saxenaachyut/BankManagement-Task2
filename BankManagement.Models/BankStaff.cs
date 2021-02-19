using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class BankStaff : User
    {
        public string AccountID { get; set; }


        public BankStaff(string name, string userName, string password)
        {
            this.Name = name;
            this.UserName = userName;
            this.Password = password;
            DateTime thisDay = DateTime.Today;
            this.AccountID = Name.Substring(0, 3) + thisDay.ToString("d");
        }
    }


}
