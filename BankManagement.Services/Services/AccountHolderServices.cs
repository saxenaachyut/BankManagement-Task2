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
        BankContext BankContext { get; set; }

        public AccountHolderServices()
        {
            this.BankContext = new BankContext();
        }

        public bool IsAccountHolderExists(string bankId, string username)
        {
            var exists = BankContext.Users.FirstOrDefault(b => b.UserName == username && b.BankId == bankId);
            if (exists != null)
                return true;
            else
                return false;
        }

        public async Task AddAccountHolder(string bankId, AccountHolder accountHolder)
        {
            try
            {
                accountHolder.Id = accountHolder.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyhhmmss");
                accountHolder.AvailableBalance = 0;
                accountHolder.BankId = bankId;
                accountHolder.UserType = UserType.AccountHolder;

                BankContext.Users.Add(accountHolder);
                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public AccountHolder GetAccountHolder(string bankId, string username)
        {
            try
            {
                var accountHolder = BankContext.Users.SingleOrDefault(b => b.UserName == username && b.BankId == bankId && b.UserType == UserType.AccountHolder);

                return (AccountHolder)accountHolder;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public AccountHolder GetAccountHolderThroughID(string bankId, string accountID)
        {
            try
            {
                var accountHolders = BankContext.Users.SingleOrDefault(b => b.Id == accountID && b.BankId == bankId);
                return (AccountHolder)accountHolders;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public async Task RemoveAccountHolder(string bankId, string username)
        {
            try
            {
                var accountHolder = BankContext.Users.SingleOrDefault(b => b.UserName == username && b.BankId == bankId);
                BankContext.Users.Remove(accountHolder);
                _ = await BankContext.SaveChangesAsync();

            }
            catch (Exception)
            {
            }

        }

        public async Task<double> GetAccountBalance(string bankId, string accountHolderId)
        {
            var user = await BankContext.Users.Where(b => b.Id == accountHolderId && b.BankId == bankId).ToListAsync();
            var accountHolder = (AccountHolder)user[0];
            return accountHolder.AvailableBalance;
        }
        public async Task DepositAmount(Transaction transaction)
        {
            try
            {
                BankContext.Transactions.Add(transaction);

                var accountHolder = (AccountHolder)BankContext.Users.SingleOrDefault(b => b.Id == transaction.SrcAccountNumber);
                accountHolder.AvailableBalance += transaction.Amount;

                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public bool IsSufficientFundsAvailable(string accountHolderId, double amount)
        {
            var user = BankContext.Users.SingleOrDefault(b => b.Id == accountHolderId);
            var accountHolder = (AccountHolder)user;

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
                var user = BankContext.Users.SingleOrDefault(b => b.Id == transaction.SrcAccountNumber);
                var accountHolder = (AccountHolder)user;

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

                var user = BankContext.Users.SingleOrDefault(b => b.Id == transaction.SrcAccountNumber);
                var sender = (AccountHolder)user;

                sender.AvailableBalance -= transaction.Amount;

                user = BankContext.Users.SingleOrDefault(b => b.Id == transaction.DestAccountNumber);
                var beneficiary = (AccountHolder)user;

                beneficiary.AvailableBalance += transaction.Amount;

                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

        }

        public async Task<List<Transaction>> GetTransactions(string accountHolderId)
        {
            var transactions = await BankContext.Transactions.Where(b => b.Id == accountHolderId).ToListAsync();
            return transactions;
        }

        public async Task ChangePassword(string bankId, string username, string newPassword)
        {
            var accountHolder = GetAccountHolder(bankId, username);
            accountHolder.Password = newPassword;

            _ = await BankContext.SaveChangesAsync();
        }
    }
}
