using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class CustomerServices
    {

        public bool IsCustomerExists(Bank bank, string Username)
        {
            foreach (var customer in bank.AccountsList)
            {
                if (Username.Equals(customer.UserName))
                {
                    return true;
                }
            }
            return false;
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
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (username.Equals(customer.UserName))
                {
                    return customer;
                }
            }

            return null;
        }

        public AccountHolder GetCustomerThroughID(Bank bank, string username)
        {
            foreach (AccountHolder customer in bank.AccountsList)
            {
                if (username.Equals(customer.AccountID))
                {
                    return customer;
                }
            }

            return null;
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
