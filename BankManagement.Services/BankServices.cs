using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankServices
    {
        public bool IsBankExists(List<Bank> bankList, string bankName)
        {
            return bankList.Exists(b => b.Name == bankName);
        }

        public bool AddBank(List<Bank> bankList, Bank bank)
        {
            try
            {
                bankList.Add(bank);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Bank GetBank(List<Bank> bankList, string bankName)
        {
            try
            {
                Bank bank = bankList.Find(b => b.Name == bankName);
                return bank;
            }

            catch (Exception)
            {
                return null;
            }

        }

        public Bank GetBankThroughID(List<Bank> bankList, string bankID)
        {
            try
            {
                Bank bank = bankList.Find(b => b.ID == bankID);
                return bank;
            }

            catch (Exception)
            {
                return null;
            }

        }

        public Boolean IsStaffExists(Bank bank, string username)
        {
            return bank.StaffList.Exists(b => b.UserName == username);
        }

        public void AddBankStaff(Bank bank, BankStaff bankStaff)
        {
            bank.StaffList.Add(bankStaff);
        }
        
        public BankStaff GetBankStaff(Bank bank, string username)
        {
            return bank.StaffList.Find(b => b.UserName == username);
        }

        public Boolean IsCurrencyExists(Bank bank, string currencyCode)
        {
            return bank.CurrenyList.Exists(b => b.CurrencyCode == currencyCode);
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

        
        
    }
}
