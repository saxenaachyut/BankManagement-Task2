using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Customer : User
    {
        public string BankName { get; set; }
        public string AccountID { get; set; }
        public double TotalAmmount { get; set; }
        public List<Transaction> TransactionList;
    }
}
