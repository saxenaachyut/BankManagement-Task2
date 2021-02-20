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
            foreach (var user in Bank.AccountsList)
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

        public BankStaff GetBankStaff(string username)
        {
            foreach (User user in Bank.AccountsList)
            {
                if (username.Equals(user.UserName))
                {
                    if(username.GetType() == typeof(Customer))
                    {
                        return null;
                    }
                    return (BankStaff)user;
                }
            }

            return null;
        }

        public Customer GetCustomer(string username)
        {
            foreach (User user in Bank.AccountsList)
            {
                if (username.Equals(user.UserName))
                {
                    if (username.GetType() == typeof(Customer))
                    {
                        return null;
                    }
                    return (Customer)user;
                }
            }

            return null;
        }

        public void RemoveUser(string username)
        {
            Bank.AccountsList.Remove(GetCustomer(username));
        }

        public Boolean IFCurrencyExists(string currencyCode)
        {
            foreach(var currencyCodeittr in Bank.CurrenyList)
            {
                if(currencyCodeittr.CurrencyCode.Equals(currencyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddCurrency(Currency currency)
        {
            Bank.CurrenyList.Add(currency);
        }

        public void SetSameBankRate(double newRate, ServiceCharges serviceChargeType)
        {
            if(serviceChargeType == ServiceCharges.RTGS)
            {
                Bank.SameBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                Bank.SameBankIMPS = newRate;
            }
        }

        public void SetOtherBankRate(double newRate, ServiceCharges serviceChargeType)
        {
            if (serviceChargeType == ServiceCharges.RTGS)
            {
                Bank.OtherBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                Bank.OtherBankIMPS = newRate;
            }
        }

        public void DepositAmount(Customer customer, double amount)
        {
            Transactions newTransaction = new Transactions(customer.UserName, amount, Bank.BankID, customer.AccountID, TransactionTypes.Deposit);
            customer.TransactionList.Add(newTransaction);
            customer.TotalAmmount += amount;
        }

        public double GetTotalAmount(Customer customer)
        {
            return customer.TotalAmmount;
        }
    }
}
