﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class TransactionServices
    {
        public bool IsTransactionExists(AccountHolder accountHolder, string transactionID)
        {
            return accountHolder.Transactions.Exists(b => b.ID == transactionID);
        }

        public double GetTrasferAmount(Bank bank, FundTransferOption bankOption, double amount)
        {
            double totaltrasferamount = 0;

            switch(bankOption)
            {
                case FundTransferOption.SameBankRTGS:
                    totaltrasferamount = amount + amount * (bank.SameBankRTGS / 100);
                    break;
                case FundTransferOption.SameBankIMPS:
                    totaltrasferamount = amount + amount * (bank.SameBankIMPS / 100);
                    break;
                case FundTransferOption.OtherBankRTGS:
                    totaltrasferamount = amount + amount * (bank.OtherBankRTGS / 100);
                    break;
                case FundTransferOption.OtherBankIMPS:
                    totaltrasferamount = amount + amount * (bank.OtherBankIMPS / 100);
                    break;
                default:
                    break;

            }

            return totaltrasferamount;
        }

        public Transaction GetTransaction(AccountHolder accountHolder, string transactionID)
        {
            return accountHolder.Transactions.Find(b => b.ID == transactionID);
        }

        public bool AddTransaction(AccountHolder accountHolder, Transaction transaction)
        {
            try
            {
                accountHolder.Transactions.Add(transaction);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool RevertTransaction(AccountHolder accountHolder, Transaction transaction)
        {
            try
            {
                if (transaction.Type == TransactionType.Debit)
                {
                    accountHolder.AccountBalance += transaction.Amount;                   
                }
                else if (transaction.Type == TransactionType.Credit)
                {
                    accountHolder.AccountBalance -= transaction.Amount;
                }

                transaction.IsReverted = true;

                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }
    }
}
