using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class AccountHolderServices
    {

        public bool IsCustomerExists(Bank bank, string username)
        {
            return bank.Accounts.Exists(b => b.UserName == username);
        }

        public bool AddAccountHolder(Bank bank, AccountHolder customer)
        {
            try
            {
                bank.Accounts.Add(customer);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public AccountHolder GetAccountHolder(Bank bank, string username)
        {
            try
            {
                AccountHolder accountHolder = bank.Accounts.Find(b => b.UserName == username);
                return accountHolder;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public AccountHolder GetAccountHolderThroughID(Bank bank, string accountID)
        {
            try
            {
                AccountHolder accountHolder = bank.Accounts.Find(b => b.AccountNumber == accountID);
                return accountHolder;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveAccountHolder(Bank bank, string username)
        {
            try
            {
                bank.Accounts.Remove(GetAccountHolder(bank, username));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public double GetAccountBalance(AccountHolder customer)
        {
            return customer.AvailableBalance;
        }

        public bool DepositAmount(AccountHolder accountHolder, double amount)
        {
            try
            {
                accountHolder.AvailableBalance += amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsSufficientFundsAvailable(AccountHolder accountHolder, double amount)
        {
            if (accountHolder.AvailableBalance > amount)
                return true;
            else
                return false;
        }

        public bool WithdrawAmount(AccountHolder accountHolder, double amount)
        {
            try
            {
                accountHolder.AvailableBalance -= amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TransferFunds(AccountHolder sender, AccountHolder beneficiary, Transaction transaction)
        {
            try
            {
                sender.AvailableBalance -= transaction.Amount;
                sender.Transactions.Add(transaction);
                beneficiary.AvailableBalance += transaction.Amount;
                beneficiary.Transactions.Add(transaction);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
