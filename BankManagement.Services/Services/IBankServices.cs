using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank
{
    public interface IBankServices
    {
        BankContext BankContext { get; set; }

        Task AddBank(string bankName);
        Task AddBankStaff(BankStaff bankStaff);
        Task AddCurrency(Currency currency);
        Task<Bank> GetBank(string bankName);
        Task<int> GetBankCount();
        Task<string> GetBankID(string bankName);
        Task<List<Bank>> GetBankList();
        BankStaff GetBankStaff(string bankId, string username);
        Task<Bank> GetBankThroughID(string bankId);
        bool IsBankExists(string bankName);
        bool IsCurrencyExists(string bankId, string currencyCode);
        bool IsStaffExists(string bankID, string username);
        Task SetOtherBankRate(string bankId, double newRate, ServiceCharges serviceChargeType);
        Task SetSameBankRate(string bankId, double newRate, ServiceCharges serviceChargeType);
    }
}