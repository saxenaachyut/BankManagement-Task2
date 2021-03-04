using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class TransactionServices : ITransactionServices
    {
        public bool IsTransactionExists(AccountHolder accountHolder, string transactionID)
        {
            return accountHolder.Transactions.Exists(b => b.ID == transactionID);
        }

        public double GetTrasferAmount(Bank bank, FundTransferOption bankOption, double amount)
        {
            double totaltrasferamount = 0;

            switch (bankOption)
            {
                case FundTransferOption.SameBankRTGS:
                    totaltrasferamount = amount + amount * (bank.ServiceChargeRates.SameBankRTGS / 100);
                    break;
                case FundTransferOption.SameBankIMPS:
                    totaltrasferamount = amount + amount * (bank.ServiceChargeRates.SameBankIMPS / 100);
                    break;
                case FundTransferOption.OtherBankRTGS:
                    totaltrasferamount = amount + amount * (bank.ServiceChargeRates.OtherBankRTGS / 100);
                    break;
                case FundTransferOption.OtherBankIMPS:
                    totaltrasferamount = amount + amount * (bank.ServiceChargeRates.OtherBankIMPS / 100);
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

        public bool RevertTransaction(AccountHolder accountHolder, Transaction transaction)
        {
            try
            {
                switch (transaction.Type)
                {
                    case TransactionType.Debit:
                        accountHolder.AvailableBalance += transaction.Amount;
                        break;

                    case TransactionType.Credit:
                        accountHolder.AvailableBalance -= transaction.Amount;
                        break;

                    case TransactionType.Transfer:
                        if (transaction.SrcAccountNumber == accountHolder.AccountNumber)
                        {
                            accountHolder.AvailableBalance += transaction.Amount;
                        }
                        else if (transaction.DestAccountNumber == accountHolder.AccountNumber)
                        {
                            accountHolder.AvailableBalance -= transaction.Amount;
                        }
                        break;

                    default:
                        return false;
                }

                transaction.IsReverted = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
