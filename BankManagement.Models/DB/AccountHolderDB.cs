using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank
{
    public class AccountHolderDB : UserDB
    {
        public AccountHolderDB()
        {
            this.Transactions = new List<Transaction>();
        }
        public string AccountNumber { get; set; }

        public double AvailableBalance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
