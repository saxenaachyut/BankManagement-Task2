using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankServices
    {
        public bool IsBankExists(BankContext bankContext, string bankName)
        {
            var exists = bankContext.Banks.Where(b => b.Name == bankName).FirstOrDefault<Bank>();
            if (exists != null)
                return true;
            else
                return false;
        }

        public async Task AddBank(BankContext bankContext, string bankName)
        {
            try
            {
                string bankUid = bankName.Substring(0, 3) + System.DateTime.Now.ToString("ddmmyyyy");
                var bank = new Bank { BankUId = bankUid, Name = bankName };
                
                bankContext.Banks.Add(bank);

                var serviceCharge = new ServiceChargeRates
                {
                    SameBankRTGS = 0,
                    SameBankIMPS = 5,
                    OtherBankRTGS = 2,
                    OtherBankIMPS = 6,
                    BankId = bank.Id,
                    Bank = bank
                };

                var currency = new Currency()
                {
                    CurrencyCode = Constants.DefaultCurrencyCode,
                    Name = Constants.DefaultCurrencyName,
                    ExcahngeRate = Constants.DefaultExchangeRate,
                    IsDefault = true,
                    BankId = bank.Id,
                    Bank = bank
                };

                bankContext.ServiceCharges.Add(serviceCharge);
                bankContext.Currencies.Add(currency);
                _ = await bankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
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
                Bank bank = banks.Find(b => b.BankUId == bankID);
                return bank;
            }

            catch (Exception)
            {
                return null;
            }

        }

        public async Task<int> GetBankID(BankContext bankContext, string bankName)
        {
            var banks = await bankContext.Banks.Where(b => b.Name == bankName).ToListAsync();
            return banks[0].Id;
        }


        public bool IsStaffExists(BankContext bankContext, int bankID, string username)
        {
            var exists =  bankContext.Employees.Where(b => b.UserName == username && b.BankId == bankID);
            if (exists != null)
                return true;
            else
                return false;

        }

        public async Task AddBankStaff(BankContext bankContext, BankStaff bankStaff)
        {
            try
            {
                bankStaff.AccountNumber = bankStaff.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                bankContext.Employees.Add(bankStaff);
                _ = await bankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }   
        }

        public async Task<BankStaff> GetBankStaff(BankContext bankContext, int bankId, string username)
        {
            var staff = await bankContext.Employees.Where(b => b.UserName == username && b.BankId == bankId).ToListAsync();
            return staff[0];
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
