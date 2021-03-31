using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank
{
    public interface IBankServices
    {
        BankContext BankContext { get; set; }

        Task AddBank(string bankName);
        Task AddBankStaff(BankStaff bankStaff);
        Task AddCurrency(int bankId, Currency currency);
        Task<Bank> GetBank(int bankId);
        Task<Bank> GetBank(string bankName);
        Task<int> GetBankCount();
        Task<int> GetBankID(string bankName);
        Task<List<Bank>> GetBankList();
        Task<BankStaff> GetBankStaff(int bankId, string username);
        bool IsBankExists(string bankName);
        bool IsCurrencyExists(int bankId, string currencyCode);
        bool IsStaffExists(int bankID, string username);
        Task SetOtherBankRate(int bankId, double newRate, ServiceCharges serviceChargeType);
        Task SetSameBankRate(int bankId, double newRate, ServiceCharges serviceChargeType);
    }
}