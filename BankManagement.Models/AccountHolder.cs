using System;
using System.Collections.Generic;

namespace Bank
{
    public class AccountHolder : User
    {
        public AccountHolder()
        {
            this.Transactions = new List<Transaction>();
        }

        public int ID { get; set; }

        public string AccountNumber { get; set; }
       
        public double AvailableBalance { get; set; }

        public string BankID { get; set; }
        
        public List<Transaction> Transactions {get; set;}
    }
}
