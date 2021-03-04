using System.Collections.Generic;

namespace Bank
{
    public interface IBankServices
    {
        bool AddBank(List<Bank> banks, Bank bank);
        bool AddBankStaff(Bank bank, BankStaff bankStaff);
        bool AddCurrency(Bank bank, Currency currency);
        Bank GetBank(List<Bank> banks, string bankName);
        BankStaff GetBankStaff(Bank bank, string username);
        Bank GetBankThroughID(List<Bank> banks, string bankID);
        bool IsBankExists(List<Bank> banks, string bankName);
        bool IsCurrencyExists(Bank bank, string currencyCode);
        bool IsStaffExists(Bank banks, string username);
        void SetOtherBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType);
        void SetSameBankRate(Bank bank, double newRate, ServiceCharges serviceChargeType);
    }
}