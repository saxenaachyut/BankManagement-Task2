using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankServices
    {

        public void SetupNewBankStaff(BankStaff bankStaff, string name, string username, string password)
        {
            bankStaff.Name = name;
            bankStaff.UserName = username;
            bankStaff.Password = password;
            DateTime thisDay = DateTime.Today;
            bankStaff.AccountID = bankStaff.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
        }

        public void SetupNewCustomer(Bank bank, Customer customer, string name, string username, string password)
        {
            customer.Name = name;
            customer.UserName = username;
            customer.Password = password;
            DateTime thisDay = DateTime.Today;
            customer.AccountID = customer.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            customer.TotalAmmount = 0;
            customer.TransactionList = new List<Transaction>();
            customer.BankName = bank.Name;
        }
        public Boolean IfStaffExists(Bank bank, string Username)
        {
            foreach (var staff in bank.StaffList)
            {
                if (Username.Equals(staff.UserName))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean IFCustomerExists(Bank bank, string Username)
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

        public void AddBankStaff(Bank bank, BankStaff bankStaff)
        {
            bank.StaffList.Add(bankStaff);
        }

        public void AddCustomer(Bank bank, Customer customer)
        {
            bank.AccountsList.Add(customer);
        }

        public BankStaff GetBankStaff(Bank bank, string username)
        {
            foreach (BankStaff bankStaff in bank.StaffList)
            {
                if (username.Equals(bankStaff.UserName))
                {
                    return bankStaff;
                }
            }

            return null;
        }

        public Customer GetCustomer(Bank bank, string username)
        {
            foreach (Customer customer in bank.AccountsList)
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
            bank.AccountsList.Remove(GetCustomer(bank,username));
        }

        public Boolean IFCurrencyExists(Bank bank, string currencyCode)
        {
            foreach (var currencyCodeittr in bank.CurrenyList)
            {
                if (currencyCodeittr.CurrencyCode.Equals(currencyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public void SetupNewCurrency(Currency currency, string name, string currencyCode, double exchangeRate)
        {
            currency.Name = name;
            currency.CurrencyCode = currencyCode;
            currency.ExcahngeRate = exchangeRate;
        }

        public void AddCurrency(Bank bank, Currency currency)
        {
            bank.CurrenyList.Add(currency);
        }

        public void SetSameBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType)
        {
            if (serviceChargeType == ServiceCharges.RTGS)
            {
                bank.SameBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                bank.SameBankIMPS = newRate;
            }
        }

        public void SetOtherBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType)
        {
            if (serviceChargeType == ServiceCharges.RTGS)
            {
                bank.OtherBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                bank.OtherBankIMPS = newRate;
            }
        }

        public void DepositAmount(Customer customer, Transaction transaction)
        {
            customer.TransactionList.Add(transaction);
            customer.TotalAmmount += transaction.Amount;
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

        public void WithdrawAmount(Customer customer, Transaction transaction)
        {
            if (IfSufficientFundsAvailable(customer, transaction.Amount))
            {
                customer.TransactionList.Add(transaction);
                customer.TotalAmmount -= transaction.Amount;
            }
            else
                return;
        }

        public double GetTrasferAmount(Bank bank, FundTransferOptions bankOption, double amount)
        {
            double totaltrasferamount = 0;

            if (bankOption == FundTransferOptions.SameBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankRTGS / 100);
            }
            else if (bankOption == FundTransferOptions.SameBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.SameBankIMPS / 100);
            }
            else if (bankOption == FundTransferOptions.OtherBankRTGS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankRTGS / 100);
            }
            else if (bankOption == FundTransferOptions.OtherBankIMPS)
            {
                totaltrasferamount = amount + amount * (bank.OtherBankIMPS / 100);
            }

            return totaltrasferamount;
        }

        public void TransferFunds(Customer customer, Customer beneficiary, Transaction debitTransaction, Transaction creditTransaction)
        {
            customer.TotalAmmount -= debitTransaction.Amount;
            customer.TransactionList.Add(debitTransaction);
            beneficiary.TotalAmmount += creditTransaction.Amount;
            beneficiary.TransactionList.Add(creditTransaction);
        }

        public Boolean IFTransactionExists(Bank bank, string transactionID)
        {
            foreach (Customer customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Transaction GetTransaction(Customer customer, string transactionID)
        {
            foreach (Transaction transaction in customer.TransactionList)
            {
                if (transaction.ID.Equals(transactionID))
                {
                    return transaction;
                }
            }


            return null;
        }

        public string GetSenderUsername(Bank bank, string transactionID)
        {
            foreach (Customer customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.Sender;
                        }
                    }
                }
            }
            return null;
        }

        public string GetSenderBankName(Bank bank, string transactionID)
        {
            foreach (Customer customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.SenderBankName;
                        }
                    }
                }
            }
            return null;
        }



        public string GetBeneficiaryUsername(Bank bank, string transactionID)
        {
            foreach (Customer customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.Beneficiary;
                        }
                    }
                }
            }
            return null;
        }
        public string GetBeneficiaryBankName(Bank bank, string transactionID)
        {
            foreach (Customer customer in bank.AccountsList)
            {
                if (customer.AccountID.Equals(transactionID.Substring(14, 11)))
                {
                    foreach (Transaction transaction in customer.TransactionList)
                    {
                        if (transaction.ID.Equals(transactionID))
                        {
                            return transaction.BeneficiaryBankName;
                        }
                    }
                }
            }
            return null;
        }
        public void RevertTransaction(Customer customer, Customer beneficiary, Transaction debitTransaction, Transaction creditTrasactions)
        {
            customer.TotalAmmount += debitTransaction.Amount;
            customer.TransactionList.Remove(debitTransaction);
            beneficiary.TotalAmmount -= creditTrasactions.Amount;
            beneficiary.TransactionList.Remove(creditTrasactions);
        }

        public void RevertTransaction(Customer customer, Transaction transaction)
        {
            if (transaction.TransactionType == TransactionTypes.Withdrawl)
            {
                customer.TotalAmmount += transaction.Amount;
                customer.TransactionList.Remove(transaction);
            }
            else if (transaction.TransactionType == TransactionTypes.Deposit)
            {
                customer.TotalAmmount -= transaction.Amount;
                customer.TransactionList.Remove(transaction);
            }
        }

    }
}
