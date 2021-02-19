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
        LoginBankStaff = 1, LoginCustomer = 2

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

    public enum TransactionTypes
    {
        Deposit = 1, Withdrawl = 2, Transfer = 3
    }
}
