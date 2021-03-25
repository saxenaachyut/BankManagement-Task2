using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankServices : IBankServices
    {
        public bool IsBankExists(List<Bank> banks, string bankName)
        {
            return banks.Exists(b => b.Name == bankName);
        }

        public bool AddBank(List<Bank> banks, Bank bank)
        {
            try
            {
                bank.Id = bank.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                bank.ServiceChargeRates.SameBankRTGS = 0;
                bank.ServiceChargeRates.SameBankIMPS = 5;
                bank.ServiceChargeRates.OtherBankRTGS = 2;
                bank.ServiceChargeRates.OtherBankIMPS = 6;
                Currency defaultCurrency = new Currency();
                defaultCurrency.CurrencyCode = Constants.DefaultCurrencyCode;
                defaultCurrency.Name = Constants.DefaultCurrencyName;
                defaultCurrency.ExcahngeRate = Constants.DefaultExchangeRate;
                defaultCurrency.IsDefault = true;
                bank.Currencies.Add(defaultCurrency);
                banks.Add(bank);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Bank GetBank(List<Bank> banks, string bankName)
        {
            try
            {
                Bank bank = banks.Find(b => b.Name == bankName);
                return bank;
            }

            catch (Exception)
            {
                return null;
            }

        }

        public Bank GetBankThroughID(List<Bank> banks, string bankID)
        {
            try
            {
                Bank bank = banks.Find(b => b.ID == bankID);
                return bank;
            }

            catch (Exception)
            {
                return null;
            }

        }

        public Boolean IsStaffExists(Bank banks, string username)
        {
            return banks.Employees.Exists(b => b.UserName == username);
        }

        public bool AddBankStaff(Bank bank, BankStaff bankStaff)
        {
            try
            {
                bankStaff.AccountNumber = bankStaff.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                bankStaff.EmployeeID = Convert.ToString(bank.Employees.Count + 1);
                bank.Employees.Add(bankStaff);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public BankStaff GetBankStaff(Bank bank, string username)
        {
            return bank.Employees.Find(b => b.UserName == username);
        }

        public Boolean IsCurrencyExists(Bank bank, string currencyCode)
        {
            return bank.Currencies.Exists(b => b.CurrencyCode == currencyCode);
        }

        public bool AddCurrency(Bank bank, Currency currency)
        {
            try
            {
                bank.Currencies.Add(currency);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetSameBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType)
        {
            if (serviceChargeType == ServiceCharges.RTGS)
            {
                bank.ServiceChargeRates.SameBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                bank.ServiceChargeRates.SameBankIMPS = newRate;
            }
        }

        public void SetOtherBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType)
        {
            if (serviceChargeType == ServiceCharges.RTGS)
            {
                bank.ServiceChargeRates.OtherBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                bank.ServiceChargeRates.OtherBankIMPS = newRate;
            }
        }

    }
}
