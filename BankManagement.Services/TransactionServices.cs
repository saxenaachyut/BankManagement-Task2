using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class TransactionServices
    {

        public void SetupNewTransaction(Transaction transaction, string senderAccountID, double amount, string bankID, string accountID, string senderBankName, TransactionTypes transationType)
        {
            transaction.SourceAccount = senderAccountID;
            transaction.DestinationAccount = senderAccountID;
            transaction.BankID = bankID;
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.SourceAccountBankName = senderBankName;
            transaction.DestinationAccountBankName = senderBankName;
            transaction.Amount = amount;

            if (transationType == TransactionTypes.Deposit)
            {
                transaction.TransactionType = TransactionTypes.Deposit;
            }
            else if (transationType == TransactionTypes.Withdrawl)
            {
                transaction.TransactionType = TransactionTypes.Withdrawl;
            }
        }

        public void SetupNewTransaction(Transaction transaction, string senderAccountID, string destinationAccountID, double amount, string bankID, string accountID, string senderBankName, string beneficiaryBankName, TransactionTypes transationType)
        {
            transaction.SourceAccount = senderAccountID;
            transaction.DestinationAccount = destinationAccountID;
            transaction.BankID = bankID;
            transaction.SourceAccountBankName = senderBankName;
            transaction.DestinationAccountBankName = beneficiaryBankName;
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.Amount = amount;

            if (transationType == TransactionTypes.TransferDebit)
            {
                transaction.TransactionType = TransactionTypes.TransferDebit;

            }
            else if (transationType == TransactionTypes.TransferCredit)
            {
                transaction.TransactionType = TransactionTypes.TransferCredit;
            }
        }

        public Boolean CheckIfTransactionExists(Bank bank, string transactionID)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public double GetTrasferAmount(Bank bank, FundTransferOptions bankOption, double amount)
        {
            double totaltrasferamount = 0;

            if (bankOption == FundTransferOptions.SameBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankRTGS / 100);
            }
            else if (bankOption == FundTransferOptions.SameBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankIMPS / 100);
            }
            else if (bankOption == FundTransferOptions.OtherBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankRTGS / 100);
            }
            else if (bankOption == FundTransferOptions.OtherBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankIMPS / 100);
            }

            return totaltrasferamount;
        }

        public Transaction GetTransaction(AccountHolder customer, string transactionID)
        {
            foreach (Transaction transaction in customer.TransactionList)
            {
                if (transaction.ID.Equals(transactionID))
                {
                    return transaction;
                }
            }

            return null;
        }

        public void DepositAmount(AccountHolder customer, Transaction transaction)
        {
            customer.TransactionList.Add(transaction);
            customer.AccountBalance += transaction.Amount;
        }

        public Boolean CheckIfSufficientFundsAvailable(AccountHolder customer, double amount)
        {
            if (customer.AccountBalance > amount)
                return true;
            else
                return false;
        }

        public void WithdrawAmount(AccountHolder customer, Transaction transaction)
        {
            if (CheckIfSufficientFundsAvailable(customer, transaction.Amount))
            {
                customer.TransactionList.Add(transaction);
                customer.AccountBalance -= transaction.Amount;
            }
            else
                return;
        }

        public void TransferFunds(AccountHolder customer, AccountHolder beneficiary, Transaction debitTransaction, Transaction creditTransaction)
        {
            customer.AccountBalance -= debitTransaction.Amount;
            customer.TransactionList.Add(debitTransaction);
            beneficiary.AccountBalance += creditTransaction.Amount;
            beneficiary.TransactionList.Add(creditTransaction);
        }

        public void RevertTransaction(AccountHolder customer, AccountHolder beneficiary, Transaction debitTransaction, Transaction creditTrasactions)
        {
            customer.AccountBalance += debitTransaction.Amount;
            customer.TransactionList.Remove(debitTransaction);
            beneficiary.AccountBalance -= creditTrasactions.Amount;
            beneficiary.TransactionList.Remove(creditTrasactions);
        }

        public void RevertTransaction(AccountHolder customer, Transaction transaction)
        {
            if (transaction.TransactionType == TransactionTypes.Withdrawl)
            {
                customer.AccountBalance += transaction.Amount;
                customer.TransactionList.Remove(transaction);
            }
            else if (transaction.TransactionType == TransactionTypes.Deposit)
            {
                customer.AccountBalance -= transaction.Amount;
                customer.TransactionList.Remove(transaction);
            }
        }

        public string GetSenderUsername(Bank bank, string transactionID)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.SourceAccount;
                        }
                    }
                }
            }
            return null;
        }

        public string GetSenderBankName(Bank bank, string transactionID)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.SourceAccountBankName;
                        }
                    }
                }
            }
            return null;
        }



        public string GetBeneficiaryUsername(Bank bank, string transactionID)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.DestinationAccount;
                        }
                    }
                }
            }
            return null;
        }
        public string GetBeneficiaryBankName(Bank bank, string transactionID)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.DestinationAccountBankName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
