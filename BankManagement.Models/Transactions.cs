using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Transactions
    {
        public string Sender { get; set; }
        public string Beneficiary { get; set; }
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string BankID { get; set; }
        public double Amount { get; set; }

        public Transactions(string sender, int amount, string bankID, string accountID, TransactionTypes transationType)
        {
            DateTime thisDay = new DateTime();
            if (transationType == TransactionTypes.Deposit)
            {
                this.TransactionType = TransactionTypes.Deposit;
                this.Sender = sender;
                this.Beneficiary = sender;
                this.BankID = bankID;
                this.Amount = amount;
                this.TransactionDate = thisDay.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");
            }
            else if (transationType == TransactionTypes.Withdrawl)
            {
                this.TransactionType = TransactionTypes.Withdrawl;
                this.Sender = sender;
                this.Beneficiary = sender;
                this.BankID = bankID;
                this.Amount = amount;
                this.TransactionDate = thisDay.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");

            }
        }

        public Transactions(string sender, string beneficiary, int amount, string bankID, string accountID, TransactionTypes transationType)
        {
            DateTime thisDay = new DateTime();
            this.TransactionType = TransactionTypes.Transfer;
            this.Sender = sender;
            this.Beneficiary = beneficiary;
            this.Beneficiary = sender;
            this.BankID = bankID;
            this.Amount = amount;
            this.TransactionDate = thisDay.ToString("f");
            this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");
        }


    }
}
