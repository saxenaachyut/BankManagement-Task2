using System;
using System.Collections.Generic;

namespace Bank
{
    public class AccountHolder : User
    {
        public string AccountNumber { get; set; }
       
        public double AccountBalance { get; set; }
        
        public string Email { get; set; }
        
        public string BankID { get; set; }
        
        public List<Transaction> Transactions {get; set;}

        public AccountHolder()
        {
            this.Transactions = new List<Transaction>();
        }
    }
}
