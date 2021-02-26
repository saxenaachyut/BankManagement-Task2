using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class TransactionServices
    {

        public void SetupNewTransaction(Transaction transaction, string senderAccountID, double amount, string bankID, string accountID, TransactionType transationType)
        {
            transaction.SrcAccountID = senderAccountID;
            transaction.DestAccountID = senderAccountID;
            transaction.SrcBankID = bankID;
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.DestBankID = bankID;
            transaction.Amount = amount;

            if (transationType == TransactionType.Deposit)
            {
                transaction.TransactionType = TransactionType.Deposit;
            }
            else if (transationType == TransactionType.Withdrawl)
            {
                transaction.TransactionType = TransactionType.Withdrawl;
            }
        }

        public void SetupNewTransaction(Transaction transaction, string senderAccountID, string destinationAccountID, double amount, string bankID, string accountID, string destBankID, TransactionType transationType)
        {
            transaction.SrcAccountID = senderAccountID;
            transaction.DestAccountID = destinationAccountID;
            transaction.SrcBankID = bankID;
            transaction.DestBankID = destBankID;
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.Amount = amount;

            if (transationType == TransactionType.TransferDebit)
            {
                transaction.TransactionType = TransactionType.TransferDebit;

            }
            else if (transationType == TransactionType.TransferCredit)
            {
                transaction.TransactionType = TransactionType.TransferCredit;
            }
        }

        public bool IsTransactionExists(Bank bank, string transactionID)
        {
            foreach (AccountHolder accountHolder in bank.AccountsList)
            {
                if (accountHolder.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    return accountHolder.TransactionList.Exists(b => b.ID == transactionID);
                }
            }
            return false;
        }

        public double GetTrasferAmount(Bank bank, FundTransferOption bankOption, double amount)
        {
            double totaltrasferamount = 0;

            if (bankOption == FundTransferOption.SameBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankRTGS / 100);
            }
            else if (bankOption == FundTransferOption.SameBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankIMPS / 100);
            }
            else if (bankOption == FundTransferOption.OtherBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankRTGS / 100);
            }
            else if (bankOption == FundTransferOption.OtherBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankIMPS / 100);
            }

            return totaltrasferamount;
        }

        public Transaction GetTransaction(AccountHolder accountHolder, string transactionID)
        {
            return accountHolder.TransactionList.Find(b => b.ID == transactionID);
        }

        public void DepositAmount(AccountHolder accountHolder, Transaction transaction)
        {
            accountHolder.TransactionList.Add(transaction);
            accountHolder.AccountBalance += transaction.Amount;
        }

        public bool IsSufficientFundsAvailable(AccountHolder accountHolder, double amount)
        {
            if (accountHolder.AccountBalance > amount)
                return true;
            else
                return false;
        }

        public void WithdrawAmount(AccountHolder accountHolder, Transaction transaction)
        {
            if (IsSufficientFundsAvailable(accountHolder, transaction.Amount))
            {
                accountHolder.TransactionList.Add(transaction);
                accountHolder.AccountBalance -= transaction.Amount;
            }
            else
                return;
        }

        public void TransferFunds(AccountHolder sender, AccountHolder beneficiary, Transaction debitTransaction, Transaction creditTransaction)
        {
            sender.AccountBalance -= debitTransaction.Amount;
            sender.TransactionList.Add(debitTransaction);
            beneficiary.AccountBalance += creditTransaction.Amount;
            beneficiary.TransactionList.Add(creditTransaction);
        }

        public void RevertTransaction(AccountHolder sender, AccountHolder beneficiary, Transaction debitTransaction, Transaction creditTrasactions)
        {
            sender.AccountBalance += debitTransaction.Amount;
            sender.TransactionList.Remove(debitTransaction);
            beneficiary.AccountBalance -= creditTrasactions.Amount;
            beneficiary.TransactionList.Remove(creditTrasactions);
        }

        public void RevertTransaction(AccountHolder accountHolder, Transaction transaction)
        {
            if (transaction.TransactionType == TransactionType.Withdrawl)
            {
                accountHolder.AccountBalance += transaction.Amount;
                accountHolder.TransactionList.Remove(transaction);
            }
            else if (transaction.TransactionType == TransactionType.Deposit)
            {
                accountHolder.AccountBalance -= transaction.Amount;
                accountHolder.TransactionList.Remove(transaction);
            }
        }

        public string GetSenderUsername(Bank bank, string transactionID)
        {
            foreach (AccountHolder accountHolder in bank.AccountsList)
            {
                if (accountHolder.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in accountHolder.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.SrcAccountID;
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
                            return transaction.DestAccountID;
                        }
                    }
                }
            }
            return null;
        }

        public string GetSrcBankID(Bank bank, string transactionID)
        {
            foreach (AccountHolder accountHolder in bank.AccountsList)
            {
                if (accountHolder.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in accountHolder.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.SrcBankID;
                        }
                    }
                }
            }
            return null;
        }

        public string GetDestBankID(Bank bank, string transactionID)
        {
            foreach (AccountHolder accountHolder in bank.AccountsList)
            {
                if (accountHolder.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in accountHolder.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.DestBankID;
                        }
                    }
                }
            }
            return null;
        }
    }
}
