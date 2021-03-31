using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class TransactionServices : ITransactionServices
    {
        public BankContext BankContext { get; set; }

        public TransactionServices()
        {
            this.BankContext = new BankContext();
        }
        public bool IsTransactionExists(string transactionUID)
        {
            var transaction = BankContext.Transactions.FirstOrDefault(b => b.TransactionUId == transactionUID);
            if (transaction != null)
                return true;
            else
                return false;

        }

        public double GetTrasferAmount(int bankId, FundTransferOption bankOption, double amount)
        {
            double totaltrasferamount = 0;
            var serviceCharge = BankContext.ServiceCharges.FirstOrDefault(b => b.BankId == bankId);

            switch (bankOption)
            {
                case FundTransferOption.SameBankRTGS:

                    totaltrasferamount = amount + amount * (serviceCharge.SameBankRTGS / 100);
                    break;
                case FundTransferOption.SameBankIMPS:
                    totaltrasferamount = amount + amount * (serviceCharge.SameBankIMPS / 100);
                    break;
                case FundTransferOption.OtherBankRTGS:
                    totaltrasferamount = amount + amount * (serviceCharge.OtherBankRTGS / 100);
                    break;
                case FundTransferOption.OtherBankIMPS:
                    totaltrasferamount = amount + amount * (serviceCharge.OtherBankIMPS / 100);
                    break;
                default:
                    break;

            }

            return totaltrasferamount;
        }

        public Transaction GetTransaction(string transactionUId)
        {
            var transaction = BankContext.Transactions.Where(b => b.TransactionUId == transactionUId).FirstOrDefault<Transaction>();
            return transaction;
        }

        public async Task RevertTransaction(string transactionUId)
        {
            var transaction = GetTransaction(transactionUId);
            var accountHolder = BankContext.AccountHolders.SingleOrDefault(b => b.AccountNumber == transaction.SrcAccountNumber);
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
                        var beneficiary = BankContext.AccountHolders.SingleOrDefault(b => b.AccountNumber == transaction.DestAccountNumber);

                        accountHolder.AvailableBalance += transaction.Amount;
                        beneficiary.AvailableBalance -= transaction.Amount;
                        break;
                    default:
                        break;
                }

                transaction.IsReverted = true;

                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

        }
    }
}
