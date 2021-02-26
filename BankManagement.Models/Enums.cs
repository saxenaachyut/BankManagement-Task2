using System;       

namespace Bank
{
    public enum MainMenuOption
    {
        CreateNewBank = 1, 
        LogintoExistingBank = 2, 
        Exit = 3
    }

    public enum BankMenuOption
    {
        Login = 1, 
        GoBack = 2

    }

    public enum StaffMenuOption
    {
        CreateNewCustomerAccount = 1,
        UpdateCustomerAccount = 2,
        DeleteCustomerAccount = 3,
        AddNewCurrency = 4,
        UpdateServiceChargeSameBank = 5,
        UpdateServiceChargeOtherBank = 6,
        TransactionHistory = 7,
        RevertTransaction = 8,
        Logout = 9
    }

    public enum CustomerMenuOption
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        TransactionHistory = 4,
        Logout = 5
    }

    public enum TransactionType
    {
        Deposit = 1, 
        Withdrawl = 2, 
        TransferDebit = 3, 
        TransferCredit = 4
    }

    public enum ServiceCharges
    {
        RTGS = 1, 
        IMPS = 2
    }

    public enum FundTransferOption
    {
        SameBankRTGS = 1,
        SameBankIMPS = 2,
        OtherBankRTGS = 3,
        OtherBankIMPS = 4
    }
}
