using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class CustomerServices
    {

        public bool IsCustomerExists(Bank bank, string username)
        {
            return bank.AccountsList.Exists(b => b.UserName == username);
        }

        public bool AddCustomer(Bank bank, AccountHolder customer)
        {
            try
            {
                bank.AccountsList.Add(customer);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public AccountHolder GetCustomer(Bank bank, string username)
        {
            try
            {
                AccountHolder accountHolder = bank.AccountsList.Find(b => b.UserName == username);
                return accountHolder;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public AccountHolder GetCustomerThroughID(Bank bank, string accountID)
        {
            try
            {
                AccountHolder accountHolder = bank.AccountsList.Find(b => b.AccountID == accountID);
                return accountHolder;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public void RemoveCustomer(Bank bank, string username)
        {
            bank.AccountsList.Remove(GetCustomer(bank, username));
        }

        public double GetAccountBalance(AccountHolder customer)
        {
            return customer.AccountBalance;
        }
    }
}
