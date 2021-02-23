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
        public double CreditAmount { get; set; }
        public double DebitAmount { get; set; }
        public string SenderBankName { get; set; }
        public string BeneficiaryBankName { get; set; }

        public Transactions(string senderUsername, double amount, string bankID, string accountID, string senderBankName, TransactionTypes transationType)
        {
            DateTime thisDay = new DateTime();
            if (transationType == TransactionTypes.Deposit)
            {
                this.TransactionType = TransactionTypes.Deposit;
                this.Sender = senderUsername;
                this.Beneficiary = senderUsername;
                this.BankID = bankID;
                this.CreditAmount = amount;
                this.DebitAmount = 0;
                this.TransactionDate = thisDay.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");
                this.SenderBankName = senderBankName;
                this.BeneficiaryBankName = senderBankName;
            }
            else if (transationType == TransactionTypes.Withdrawl)
            {
                this.TransactionType = TransactionTypes.Withdrawl;
                this.Sender = senderUsername;
                this.Beneficiary = senderUsername;
                this.BankID = bankID;
                this.CreditAmount = 0;
                this.DebitAmount = amount;
                this.TransactionDate = thisDay.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");
                this.SenderBankName = senderBankName;
                this.BeneficiaryBankName = senderBankName;

            }
        }

        public Transactions(string senderUsername, string beneficiaryUsername, double amount, string bankID, string accountID, string senderBankName, string beneficiaryBankName, TransactionTypes transationType)
        {
            if (transationType == TransactionTypes.TransferDebit)
            {
                DateTime thisDay = new DateTime();
                this.TransactionType = TransactionTypes.TransferDebit;
                this.Sender = senderUsername;
                this.Beneficiary = beneficiaryUsername;
                this.BankID = bankID;
                this.CreditAmount = 0;
                this.DebitAmount = amount;
                this.TransactionDate = thisDay.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + thisDay.ToString("d");
                this.SenderBankName = senderBankName;
                this.BeneficiaryBankName = beneficiaryBankName;
            }
            else if(transationType == TransactionTypes.TransferCredit)
            {
                this.TransactionType = TransactionTypes.TransferCredit;
                this.Sender = senderUsername;
                this.Beneficiary = beneficiaryUsername;
                this.BankID = bankID;
                this.CreditAmount = amount;
                this.DebitAmount = 0;
                this.TransactionDate = DateTime.Now.ToString("f");
                this.TransactionID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
                this.SenderBankName = senderBankName;
                this.BeneficiaryBankName = beneficiaryBankName;
            }
        }


    }
}
