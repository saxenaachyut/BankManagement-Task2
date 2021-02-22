using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public enum FirstMenu
    {
        SetupNewBank = 1, OpenExistingBank = 2
    }

    public enum SecondMenu
    {
        LoginBankStaff = 1, LoginCustomer = 2, GoBack = 3

    }

    public enum BankStaffMenu
    {
        CreateNewCustomerAccount = 1,
        UpdateCustomerAccount = 2,
        DeleteCustomerAccount = 3,
        AddNewCurrency = 4,
        ModifyServiceChargeSameBank = 5,
        ModifyServiceChargeOtherBank = 6,
        ViewTransactionHistory = 7,
        RevertTransaction = 8,
        Logout = 9
    }

    public enum CustomerMenuOptions
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ViewTransactionHistory = 4,
        Logout = 5
    }

    public enum TransactionTypes
    {
        Deposit = 1, Withdrawl = 2, TransferDebit = 3, TransferCredit
    }

    public enum ServiceCharges
    {
        RTGS = 1, IMPS = 2
    }

    public enum BankOptions
    {
        SameBankRTGS = 1,
        SameBankIMPS = 2,
        OtherBankRTGS = 3,
        OtherBankIMPS = 4
    }
}
