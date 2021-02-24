using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Transaction
    {
        public string Sender { get; set; }
        public string Beneficiary { get; set; }
        public string ID { get; set; }
        public string TransactionDate { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string BankID { get; set; }
        public double Amount { get; set; }
        public string SenderBankName { get; set; }
        public string BeneficiaryBankName { get; set; }

    }
}
