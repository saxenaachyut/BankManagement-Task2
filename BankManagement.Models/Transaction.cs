using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Transaction
    {
        public string SrcAccountID { get; set; }
        public string DestAccountID { get; set; }
        public string ID { get; set; }
        public string TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string SrcBankID { get; set; }
        public string DestBankID { get; set; }
        public double Amount { get; set; }
    }
}
