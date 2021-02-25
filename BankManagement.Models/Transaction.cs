using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Transaction
    {
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public string ID { get; set; }
        public string TransactionDate { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string BankID { get; set; }
        public double Amount { get; set; }
        public string SourceAccountBankName { get; set; }
        public string DestinationAccountBankName { get; set; }

    }
}
