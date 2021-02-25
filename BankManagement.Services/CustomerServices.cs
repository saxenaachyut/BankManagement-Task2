using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class CustomerServices
    {
        public Boolean CheckIfCustomerExists(Bank bank, string Username)
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

        public void SetupNewCustomer(Bank bank, AccountHolder customer, string name, string username, string password)
        {
            customer.Name = name;
            customer.UserName = username;
            customer.Password = password;
            customer.AccountID = customer.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            customer.AccountBalance = 0;
            customer.BankName = bank.Name;
        }

        public void AddCustomer(Bank bank, AccountHolder customer)
        {
            bank.AccountsList.Add(customer);
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

        public void RemoveCustomer(Bank bank, string username)
        {
            bank.AccountsList.Remove(GetCustomer(bank, username));
        }

        public double GetTotalAccountBalance(AccountHolder customer)
        {
            return customer.AccountBalance;
        }
    }
}
