using System;
using System.Collections.Generic;

namespace Bank
{
    public class AccountHolder : User
    {
        public string BankName { get; set; }
        public string AccountID { get; set; }
        public double AccountBalance { get; set; }
        public string BankID { get; set; }
        public List<Transaction> TransactionList {get; set;}

        public AccountHolder()
        {
            this.TransactionList = new List<Transaction>();
        }
    }
}
