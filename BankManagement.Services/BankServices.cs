using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankServices
    {
        public bool IsBankExists(List<Bank> bankList, string bankName)
        {
            foreach (Bank bank in bankList)
            {
                if (bank.Name == bankName)
                {
                    return true;
                }
            }

            return false;
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
            foreach (Bank bank in bankList)
            {
                if (bank.Name.Equals(bankName))
                {
                    return bank;
                }
            }

            return null;

        }

        public Bank GetBankThroughID(List<Bank> bankList, string bankName)
        {
            foreach (Bank bank in bankList)
            {
                if (bank.ID.Equals(bankName))
                {
                    return bank;
                }
            }

            return null;

        }

        public Boolean IsStaffExists(Bank bank, string Username)
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

        public void AddBankStaff(Bank bank, BankStaff bankStaff)
        {
            bank.StaffList.Add(bankStaff);
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

        public Boolean IsCurrencyExists(Bank bank, string currencyCode)
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

        
        
    }
}
