using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services
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
            var transaction = BankContext.Transactions.FirstOrDefault(b => b.Id == transactionUID);
            if (transaction != null)
                return true;
            else
                return false;

        }

        public double GetTrasferAmount(string bankId, FundTransferOption bankOption, double amount)
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
            var transaction = BankContext.Transactions.Where(b => b.Id == transactionUId).FirstOrDefault<Transaction>();
            return transaction;
        }

        public async Task<string> RevertTransaction(string transactionUId)
        {
            var transaction = GetTransaction(transactionUId);
            var user = BankContext.Users.SingleOrDefault(b => b.Id == transaction.SrcAccountNumber);
            var accountHolder = (AccountHolder)user;
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
                        user = BankContext.Users.SingleOrDefault(b => b.Id == transaction.DestAccountNumber);
                        var beneficiary = (AccountHolder)user;

                        accountHolder.AvailableBalance += transaction.Amount;
                        beneficiary.AvailableBalance -= transaction.Amount;
                        break;
                    default:
                        break;
                }

                transaction.Type = TransactionType.Reverted;

                _ = await BankContext.SaveChangesAsync();

                return transaction.Id;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
