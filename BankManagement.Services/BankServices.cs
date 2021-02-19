using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class BankServices
    {
        public Bank Bank;
        public string BankName;
        public BankServices(Bank bank)
        {
            this.Bank = bank;
            BankName = bank.Name;
        }

        public Boolean IFUserExists(string Username)
        {
            foreach (User user in Bank.AccountsList)
            {
                if (Username.Equals(user.UserName))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddUser(BankStaff bankStaff)
        {
            Bank.AccountsList.Add(bankStaff);
        }

        public void AddUser(Customer customer)
        {
            Bank.AccountsList.Add(customer);
        }

        public BankStaff GetBankStaff(string userName)
        {
            foreach (BankStaff user in Bank.AccountsList)
            {
                if (userName.Equals(user.UserName))
                {
                    return user;
                }
            }

            return null;
        }

        public Customer GetCustomer(string userName)
        {
            foreach (Customer user in Bank.AccountsList)
            {
                if (userName.Equals(user.UserName))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
