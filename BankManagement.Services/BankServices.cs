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

        public Boolean IfSufficientFundsAvailable(Customer customer, double amount)
        {
            if (customer.TotalAmmount > amount)
                return true;
            else
                return false;
        }

        public void WithdrawAmount(Customer customer, double amount)
        {
            Transactions newTransaction = new Transactions(customer.UserName, amount, Bank.BankID, customer.AccountID, TransactionTypes.Withdrawl);
            if ( IfSufficientFundsAvailable(customer, amount) )
            {
                customer.TransactionList.Add(newTransaction);
                customer.TotalAmmount -= amount;
            }
            else
                return;
        }

        public double GetTrasferAmount(BankOptions bankOption, double amount)
        {
            double totaltrasferamount= 0;

            if(bankOption == BankOptions.SameBankRTGS)
            {
                totaltrasferamount = amount + amount*(Bank.SameBankRTGS/100);
            }
            else if(bankOption == BankOptions.SameBankIMPS)
            {
                totaltrasferamount = amount + amount*(Bank.SameBankIMPS/100);
            }
            else if (bankOption == BankOptions.OtherBankRTGS)
            {
                totaltrasferamount = amount + amount*(Bank.OtherBankRTGS/100);
            }
            else if (bankOption == BankOptions.OtherBankIMPS)
            {
                totaltrasferamount = amount + amount*(Bank.OtherBankIMPS/100);
            }

            return totaltrasferamount;
        }

        public void SameBankTransferFunds(Customer customer, string benificaryUsername, double amount)
        {
            Customer benificiary = GetCustomer(benificaryUsername);
            customer.TotalAmmount -= amount;
            customer.TransactionList.Add(new Transactions(customer.UserName, benificaryUsername, amount, Bank.BankID, customer.AccountID, TransactionTypes.TransferDebit));
            benificiary.TotalAmmount += amount;
            benificiary.TransactionList.Add(new Transactions(customer.UserName, benificaryUsername, amount, Bank.BankID, customer.AccountID, TransactionTypes.TransferCredit));
        }

        public void OtherTransferFunds(Customer customer, Customer benificiary, double amount)
        {
            customer.TotalAmmount -= amount;
            customer.TransactionList.Add(new Transactions(customer.UserName, benificiary.UserName, amount, Bank.BankID, customer.AccountID, TransactionTypes.TransferDebit));
            benificiary.TotalAmmount += amount;
            benificiary.TransactionList.Add(new Transactions(customer.UserName, benificiary.UserName, amount, Bank.BankID, customer.AccountID, TransactionTypes.TransferCredit));
        }
    }
}
