using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class AccountHolderServices : IAccountHolderServices
    {
        public BankContext BankContext { get; set; }

        public AccountHolderServices()
        {
            this.BankContext = new BankContext();
        }

        public bool IsAccountHolderExists(int bankId, string username)
        {
            var exists = BankContext.AccountHolders.Where(b => b.UserName == username && b.BankId == bankId).FirstOrDefault<AccountHolder>();
            if (exists != null)
                return true;
            else
                return false;
        }

        public async Task AddAccountHolder(int bankId, AccountHolder accountHolder)
        {
            try
            {
                accountHolder.AccountNumber = accountHolder.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                accountHolder.AvailableBalance = 0;
                accountHolder.BankId = bankId;

                BankContext.AccountHolders.Add(accountHolder);
                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public AccountHolder GetAccountHolder(int bankId, string username)
        {
            try
            {
                var accountHolders = BankContext.AccountHolders.SingleOrDefault(b => b.UserName == username && b.BankId == bankId);
                return accountHolders;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public AccountHolder GetAccountHolderThroughID(int bankId, string accountID)
        {
            try
            {
                var accountHolders = BankContext.AccountHolders.SingleOrDefault(b => b.AccountNumber == accountID && b.BankId == bankId);
                return accountHolders;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public async Task RemoveAccountHolder(int bankId, string username)
        {
            try
            {
                var accountHolder = BankContext.AccountHolders.SingleOrDefault(b => b.UserName == username && b.BankId == bankId);
                BankContext.AccountHolders.Remove(accountHolder);
                _ = await BankContext.SaveChangesAsync();

            }
            catch (Exception)
            {
            }

        }

        public async Task<double> GetAccountBalance(int bankId, int accountHolderId)
        {
            var accountHolder = await BankContext.AccountHolders.Where(b => b.Id == accountHolderId && b.BankId == bankId).ToListAsync();
            return accountHolder[0].AvailableBalance;
        }
        public async Task DepositAmount(Transaction transaction)
        {
            try
            {
                BankContext.Transactions.Add(transaction);

                var accountHolder = BankContext.AccountHolders.SingleOrDefault(b => b.Id == transaction.AccountHolderId);
                accountHolder.AvailableBalance += transaction.Amount;

                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public bool IsSufficientFundsAvailable(int accountHolderId, double amount)
        {
            var accountHolder = BankContext.AccountHolders.SingleOrDefault(b => b.Id == accountHolderId);
            if (accountHolder.AvailableBalance > amount)
                return true;
            else
                return false;
        }

        public async Task WithdrawAmount(Transaction transaction)
        {
            try
            {
                BankContext.Transactions.Add(transaction);
                var accountHolder = BankContext.AccountHolders.SingleOrDefault(b => b.Id == transaction.AccountHolderId);
                accountHolder.AvailableBalance -= transaction.Amount;

                _ = await BankContext.SaveChangesAsync();

            }
            catch (Exception)
            {
            }
        }

        public async Task TransferFunds(Transaction transaction)
        {
            try
            {
                BankContext.Transactions.Add(transaction);

                var sender = BankContext.AccountHolders.SingleOrDefault(b => b.AccountNumber == transaction.SrcAccountNumber);
                sender.AvailableBalance -= transaction.Amount;

                var beneficiary = BankContext.AccountHolders.SingleOrDefault(b => b.AccountNumber == transaction.DestAccountNumber);
                beneficiary.AvailableBalance += transaction.Amount;
  
                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

        }

        public async Task<List<Transaction>> GetTransactions(int accountHolderId)
        {
            var transactions = await BankContext.Transactions.Where(b => b.AccountHolderId == accountHolderId).ToListAsync();
            return transactions;
        }

        public async Task ChangePassword(int bankId, string username, string newPassword)
        {
            var accountHolder = GetAccountHolder(bankId, username);
            accountHolder.Password = newPassword;

            _ = await BankContext.SaveChangesAsync();
        }
    }
}
