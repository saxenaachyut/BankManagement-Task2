using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankServices : IBankServices
    {
        public BankContext BankContext { get; set; }

        public BankServices()
        {
            this.BankContext = new BankContext();
        }

        public bool IsBankExists(string bankName)
        {
            var exists = BankContext.Banks.Where(b => b.Name == bankName).FirstOrDefault<Bank>();
            if (exists != null)
                return true;
            else
                return false;
        }

        public async Task AddBank(string bankName)
        {
            try
            {
                string bankUid = bankName.Substring(0, 3) + System.DateTime.Now.ToString("ddmmyyyy");
                var bank = new Bank { Id = bankUid, Name = bankName };

                BankContext.Banks.Add(bank);

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

                BankContext.ServiceCharges.Add(serviceCharge);
                BankContext.Currencies.Add(currency);
                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<Bank> GetBank(string bankName)
        {
            try
            {
                var bank = await BankContext.Banks.Where(b => b.Name == bankName).ToListAsync();
                return bank[0];
            }

            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Bank> GetBankThroughID(string bankId)
        {
            try
            {
                var bank = await BankContext.Banks.Where(b => b.Id == bankId).ToListAsync();
                return bank[0];
            }

            catch (Exception)
            {
                return null;
            }

        }

        public async Task<int> GetBankCount()
        {
            var banks = await BankContext.Banks.ToListAsync();
            return banks.Count;
        }

        public async Task<List<Bank>> GetBankList()
        {
            var banks = await BankContext.Banks.ToListAsync();
            return banks;
        }

        public async Task<string> GetBankID(string bankName)
        {
            var banks = await BankContext.Banks.Where(b => b.Name == bankName).ToListAsync();
            return banks[0].Id;
        }


        public bool IsStaffExists(string bankID, string username)
        {
            var exists = BankContext.Users.FirstOrDefault(b => b.UserName == username && b.BankId == bankID);
            if (exists != null)
                return true;
            else
                return false;
        }

        public async Task AddBankStaff(BankStaff bankStaff)
        {
            try
            {
                var count = await BankContext.Users.Where(b => b.BankId == bankStaff.BankId).ToListAsync();
                bankStaff.EmployeeID = (count.Count + 1).ToString();
                bankStaff.Id = bankStaff.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");

                BankContext.Users.Add(bankStaff);
                _ = await BankContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }

        public BankStaff GetBankStaff(string bankId, string username)
        {
            var user = BankContext.Users.FirstOrDefault(b => b.UserName == username && b.BankId == bankId);
            var staff = (BankStaff)user;
            return staff;
        }

        public bool IsCurrencyExists(string bankId, string currencyCode)
        {
            var exists = BankContext.Currencies.Where(b => b.CurrencyCode == currencyCode && b.BankId == bankId).FirstOrDefault<Currency>();
            if (exists != null)
                return true;
            else
                return false;
        }



        public async Task AddCurrency(Currency currency)
        {
            try
            {
                BankContext.Currencies.Add(currency);
                _ = await BankContext.SaveChangesAsync();

            }
            catch (Exception)
            {
            }
        }

        public async Task SetSameBankRate(string bankId, double newRate, ServiceCharges serviceChargeType)
        {
            var serviceCharge = BankContext.ServiceCharges.SingleOrDefault(b => b.BankId == bankId);

            if (serviceChargeType == ServiceCharges.RTGS)
            {
                serviceCharge.SameBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                serviceCharge.SameBankIMPS = newRate;
            }

            _ = await BankContext.SaveChangesAsync();
        }

        public async Task SetOtherBankRate(string bankId, double newRate, ServiceCharges serviceChargeType)
        {
            var serviceCharge = BankContext.ServiceCharges.SingleOrDefault(b => b.BankId == bankId);

            if (serviceChargeType == ServiceCharges.RTGS)
            {
                serviceCharge.OtherBankRTGS = newRate;
            }
            else if (serviceChargeType == ServiceCharges.IMPS)
            {
                serviceCharge.OtherBankIMPS = newRate;
            }

            _ = await BankContext.SaveChangesAsync();
        }

    }
}
