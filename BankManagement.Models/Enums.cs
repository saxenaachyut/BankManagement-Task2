using System;       

namespace Bank
{
    public enum MainMenu
    {
        CreateNewBank = 1, 
        Login = 2, 
        Exit = 3
    }

    public enum StaffMenu
    {
        CreateAccountHolder = 1,
        UpdateAccountHolder = 2,
        DeleteAccountHolder = 3,
        AddNewCurrency = 4,
        UpdateServiceChargeSameBank = 5,
        UpdateServiceChargeOtherBank = 6,
        TransactionHistory = 7,
        RevertTransaction = 8,
        Logout = 9
    }

    public enum AccountHolderMenu
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        TransactionHistory = 4,
        Logout = 5
    }

    public enum TransactionType
    {
        Credit = 1, 
        Debit = 2, 
        Transfer = 3
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
