using System;
using System.Collections.Generic;

namespace Bank
{
    public class BankApplication
    {
        public static BankServices BankService;
        public static AccountHolderServices AccountHolderService;
        public static TransactionServices TransactionService;
        static void Main()
        {
            BankService = new BankServices();
            AccountHolderService = new AccountHolderServices();
            TransactionService = new TransactionServices();

            BackendJson.Load();
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("Welcome to the Bank Management System\n " +
                "\n Select an option from the menu : \n" +
                "---------------------------------------------------------\n" +
                "1. Create new Bank\n" +
                "2. Login to Existing Bank\n" +
                "3. Save and Exit");

            MainMenu menuOption = (MainMenu)Utilities.GetIntInput();
            switch (menuOption)
            {
                case global::Bank.MainMenu.CreateNewBank:
                    Console.Clear();
                    string bankName = bankName = Utilities.GetStringInput("Enter Bank Name :");
                    
                    while(BankService.IsBankExists(BankStore.Banks, bankName))
                    {
                        bankName = Utilities.GetStringInput("Bank Already Exists, Enter a new Bank Name :");
                    }

                    Bank bank = new Bank();
                    bank.Name = bankName;

                    if (!BankService.AddBank(BankStore.Banks, bank))
                    {
                        Utilities.DisplayMessage("Failed to add Bank");
                        MainMenu();
                    }

                    BankStaff admin = new BankStaff();
                    admin.Name = Utilities.GetStringInput("Enter name for Admin account :"); ;
                    admin.UserName = Utilities.GetStringInput("Enter Username for Admin account :");
                    admin.Email = Utilities.GetStringInput("Enter Email Address for Admin account :");
                    admin.Password = Utilities.GetStringInput("Enter password for Admin Account :");

                    if (BankService.AddBankStaff(bank, admin))
                    {
                        Utilities.DisplayMessage("Admin Account Created Successfully\n" +
                            "Bank Successfull created and added");
                    }
                    else
                    {
                        Utilities.DisplayMessage("Could not add Admin account successfully");
                        MainMenu();
                    }


                    MainMenu();
                    break;

                case global::Bank.MainMenu.Login:
                    Console.Clear();
                    Console.WriteLine("Select the Bank to Login to");
                    Utilities.DisplayBankList(BankStore.Banks);
                    int selectBank = Utilities.GetIntInput();
                    while (BankStore.Banks.Count < selectBank)
                    {
                        Console.WriteLine("Select a valid option :");
                        selectBank = Utilities.GetIntInput();
                    }

                    Bank selectedBank = BankStore.Banks[selectBank - 1];
                    Console.Clear();
                    BankLogin(selectedBank);
                    
                    break;

                case global::Bank.MainMenu.Exit:
                    BackendJson.Save();
                    return; 

                default:
                    Console.WriteLine("Invalid Selection");           
                    MainMenu();
                    break;
            }
        }

        public static void BankLogin(Bank bank)
        {
            Console.WriteLine("Welcome to " + bank.Name + " Bank :\n" +
               "-------------------------------------------------------\n");

            string username = Utilities.GetStringInput("Enter Username to Login or Type Exit to go back");
            if (BankService.IsStaffExists(bank, username))
            {
                BankStaff bankStaff = BankService.GetBankStaff(bank, username);
                while (!bankStaff.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                {
                    Console.WriteLine("Incorrect Password Entered, Enter again : ");
                }
                Console.Clear();
                StaffMenu(bank, bankStaff);
            }
            else if (AccountHolderService.IsAccountHolderExists(bank, username))
            {
                AccountHolder accountHolder = AccountHolderService.GetAccountHolder(bank, username);
                while (!accountHolder.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                {
                    Console.WriteLine("Incorrect Password Entered, Enter again : ");
                }
                Console.Clear();
                AccountHolderMenu(bank, accountHolder);
            }
            else if (username.ToLower().Equals("exit"))
            {
                Console.Clear();
                MainMenu();
            }
            else
            {
                Utilities.DisplayMessage("User does not exists");
                Console.Clear();
                BankLogin(bank);
            }


        }

        public static void StaffMenu(Bank bank, BankStaff bankStaff)
        {
            Console.WriteLine("Welcome " + bankStaff.Name + " :\n" +
                "----------------------------------------------------\n" +
                "1. Create new Account Holder\n" +
                "2. Update Account Holder\n" +
                "3. Delete Account Holder\n" +
                "4. Add New Currency\n" +
                "5. Update Service Charges for same bank\n" +
                "6. Update Service Charges for different bank\n" +
                "7. Transaction History\n" +
                "8. Revert a Transaction\n" +
                "9. Logout");

            StaffMenu menuOption = (StaffMenu)Utilities.GetIntInput();

            switch (menuOption)
            {
                case global::Bank.StaffMenu.CreateAccountHolder:
                    Console.Clear();
                    CreateNewAccountHolder(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateAccountHolder:
                    Console.Clear();
                    UpdateAccountHolder(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.DeleteAccountHolder:
                    Console.Clear();
                    DeleteCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.AddNewCurrency:
                    Console.Clear();
                    AddNewCurrency(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateServiceChargeSameBank:
                    Console.Clear();
                    UpdateServiceChargeSameBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateServiceChargeOtherBank:
                    Console.Clear();
                    UpdateServiceChargeOtherBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.TransactionHistory:
                    Console.Clear();
                    TransactionHistory(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.RevertTransaction:
                    Console.Clear();
                    RevertTransaction(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.Logout:
                    Console.Clear();
                    BankLogin(bank);
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    StaffMenu(bank, bankStaff);
                    break;
            }

        }

        public static void AccountHolderMenu(Bank bank, AccountHolder accountHolder)
        {
            Console.WriteLine("Welcome " + accountHolder.Name + " :\n" +
                "----------------------------------------------------\n" +
                "1. Deposit Amount\n" +
                "2. Withdraw Amount\n" +
                "3. Transfer funds\n" +
                "4. Transaction History\n" +
                "5. Logout");

            AccountHolderMenu menuOption = (AccountHolderMenu)Utilities.GetIntInput();

            switch (menuOption)
            {
                case global::Bank.AccountHolderMenu.Deposit:
                    Console.Clear();
                    Deposit(bank, accountHolder);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Withdraw:
                    Console.Clear();
                    Withdraw(bank, accountHolder);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Transfer:
                    Console.Clear();
                    TransferFundsMenu(bank, accountHolder);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.TransactionHistory:
                    Console.Clear();
                    TransactionHistory(accountHolder);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Logout:
                    Console.Clear();
                    BankLogin(bank);
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    AccountHolderMenu(bank, accountHolder);
                    break;
            }
        }

        public static void CreateNewAccountHolder(Bank bank)
        {
            string name, username;
            name = Utilities.GetStringInput("Enter Name");
            while (AccountHolderService.IsAccountHolderExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User already exist enter a new Username or 0 to exit : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }
            AccountHolder accountHolder = new AccountHolder();
            accountHolder.Name = name;
            accountHolder.UserName = username;
            accountHolder.Password = Utilities.GetStringInput("Enter Password :");
            accountHolder.PhoneNumber = Utilities.GetStringInput("Enter User Phone Number :");
            accountHolder.Email = Utilities.GetStringInput("Enter User Email Address");
           
            if(!AccountHolderService.AddAccountHolder(bank, accountHolder))
            {
                Utilities.DisplayMessage("Failed to add Account Holder");
            }
            else
            {
                Utilities.DisplayMessage("Account Holder Successfull added");
            }           
        }

        public static void UpdateAccountHolder(Bank bank)
        {
            string username;
            while ( !AccountHolderService.IsAccountHolderExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            AccountHolderService.GetAccountHolder(bank,username).Password = Utilities.GetStringInput("Enter New Password :");

            Utilities.DisplayMessage("Password Updated Successfully");

        }

        public static void DeleteCustomerAccount(Bank bank)
        {
            string username;
           while ( !AccountHolderService.IsAccountHolderExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            char confirmatiom;
            while (!(confirmatiom = Convert.ToChar(Utilities.GetStringInput("Are you sure you want to delete Customer: " + username + ". Enter Y to continue or N to cancel"))).Equals('Y'))
            {
                Console.WriteLine("Enter Y or N");
                if( confirmatiom.Equals("N") )
                {
                    Console.Clear();
                    return;
                }

            }

            AccountHolderService.RemoveAccountHolder(bank,username);
            Utilities.DisplayMessage("Customer Successfully Removed");
        }

        public static void AddNewCurrency(Bank bank)
        {
            string currencyCode;
            Currency newCurrency = new Currency();
            while ( BankService.IsCurrencyExists(bank, currencyCode = Utilities.GetStringInput("Enter Currency Code :")) )
            {
                Console.WriteLine("Currency already Exists, Enter a new Currency Code or 0 to exit :");
                if (currencyCode.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            newCurrency.Name = Utilities.GetStringInput("Enter Currency Name :");
            newCurrency.ExcahngeRate = Utilities.GetDoubleInput("Enter Exchange Rate", "Only numbers are acccepted, Enter valid Rate again");
            BankService.AddCurrency(bank, newCurrency);

            Utilities.DisplayMessage("Currency succesfully added");
        }

        public static void UpdateServiceChargeSameBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.IMPS);

            Utilities.DisplayMessage("Rates for same bank transfers updated successfully");
        }

        public static void UpdateServiceChargeOtherBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.IMPS);

            Utilities.DisplayMessage("Rates for other bank transfers updated successfully");
        }

        public static void TransactionHistory(Bank bank)
        {
            string accountHolderUsername;
            while( !AccountHolderService.IsAccountHolderExists(bank, accountHolderUsername = Utilities.GetStringInput("Enter Customer Username")))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

            AccountHolder accountHolder = AccountHolderService.GetAccountHolder(bank, accountHolderUsername);
            foreach (Transaction transaction in accountHolder.Transactions)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.CreatedOn + "\n");
                Console.WriteLine("Transaction Type - " + transaction.Type + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SrcAccountNumber + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestAccountNumber + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TransactionHistory(AccountHolder accountHolder)
        {            
            foreach(Transaction transaction in accountHolder.Transactions)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.CreatedOn + "\n");
                Console.WriteLine("Transaction Type - " + transaction.Type + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SrcAccountNumber + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestAccountNumber + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Deposit(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            amount = Utilities.GetDoubleInput("Enter Amount to Deposit", "Only Numbers Accepted, Enter Amount again");
            Transaction transaction = new Transaction()
            {
                SrcAccountNumber = accountHolder.AccountNumber,
                SrcBankID = accountHolder.BankID,
                CreatedOn = DateTime.Now.ToString("f"),
                CreatedBy = accountHolder.AccountNumber,
                ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                DestBankID = accountHolder.BankID,
                Amount = amount,
                Type = TransactionType.Credit,
                IsReverted = false
            };

            if(AccountHolderService.DepositAmount(accountHolder,transaction))
            {
                Utilities.DisplayMessage("Amount Deposited Successfully\n" +
                    "Total Closing Amount : " + AccountHolderService.GetAccountBalance(accountHolder));
            }
            else
            {
                Utilities.DisplayMessage("Failed to Deposit Amount\n" +
                    "Total Closing Amount : " + AccountHolderService.GetAccountBalance(accountHolder));
            }        
        }

        public static void Withdraw(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            amount = Utilities.GetDoubleInput("Enter Amount to Withdraw", "Only Numbers Accepted, Enter Amount again");
            Transaction transaction = new Transaction()
            {
                SrcAccountNumber = accountHolder.AccountNumber,
                SrcBankID = accountHolder.BankID,
                CreatedOn = DateTime.Now.ToString("f"),
                CreatedBy = accountHolder.AccountNumber,
                ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                DestBankID = accountHolder.BankID,
                Amount = amount,
                Type = TransactionType.Debit,
                IsReverted = false
            };

            if (AccountHolderService.IsSufficientFundsAvailable(accountHolder,amount) )
            {
                if(AccountHolderService.WithdrawAmount(accountHolder, transaction))
                {
                    Utilities.DisplayMessage("Amount Withdrawed Successfully\n" +
                        "Total Closing Amount : " + AccountHolderService.GetAccountBalance(accountHolder));
                }
                else
                {
                    Utilities.DisplayMessage("Failed to Withdraw Amount\n" +
                        "Total Closing Amount : " + AccountHolderService.GetAccountBalance(accountHolder));
                }               
            }
            else
            {
                Utilities.DisplayMessage("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + AccountHolderService.GetAccountBalance(accountHolder));
            }

        }

        public static void TransferFundsMenu(Bank bank, AccountHolder accountHolder)
        {
            Console.WriteLine("Bank of the Benificiary :\n" +
                "1. Same bank (RTGS)\n" +
                "2. Same bank (IMPS)\n" +
                "3. Other bank (RTGS)\n" +
                "4. Other bank (IMPS)");

            FundTransferOption bankOption = (FundTransferOption)Utilities.GetIntInput();

            switch(bankOption)
            {
                case FundTransferOption.SameBankRTGS:
                    Console.Clear();
                    SameBankTransfer(bank, accountHolder, FundTransferOption.SameBankRTGS);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.SameBankIMPS:
                    Console.Clear();
                    SameBankTransfer(bank, accountHolder, FundTransferOption.SameBankIMPS);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankRTGS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankRTGS);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankIMPS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankIMPS);
                    AccountHolderMenu(bank, accountHolder);
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    TransferFundsMenu(bank, accountHolder);
                    break;

            }
        }

        public static void SameBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption)
        {
            string beneficiaryUsername;
            double amount;
            while( !AccountHolderService.IsAccountHolderExists(bank,beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
            {
                Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            AccountHolder beneficiary = AccountHolderService.GetAccountHolder(bank, beneficiaryUsername);

            amount = Utilities.GetDoubleInput("Enter Amount to transfer","Invalid input, Enter valid Amount :");

            amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

            if (AccountHolderService.IsSufficientFundsAvailable(accountHolder, amount))
            {
                Transaction transaction = new Transaction()
                {
                    SrcAccountNumber = accountHolder.AccountNumber,
                    DestAccountNumber = beneficiary.AccountNumber,
                    SrcBankID = accountHolder.BankID,
                    DestBankID = beneficiary.BankID,
                    ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                    CreatedOn = DateTime.Now.ToString("f"),
                    CreatedBy = accountHolder.AccountNumber,
                    Amount = amount,
                    Type = TransactionType.Transfer,
                    IsReverted = false
                };

                if( AccountHolderService.TransferFunds(accountHolder, beneficiary, transaction) )
                {
                    Utilities.DisplayMessage("Amount Successfull Transfered \n" +
                        "Total closing amount " + AccountHolderService.GetAccountBalance(accountHolder));
                }
                else
                {
                    Utilities.DisplayMessage("Amount Transfer Failed \n" +
                        "Total closing amount " + AccountHolderService.GetAccountBalance(accountHolder));
                }
            }
            else
            {
                Utilities.DisplayMessage("Insufficent Funds");
            }
        }

        public static void OtherBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption)
        {
            string otherBankName = Utilities.GetStringInput("Enter Bank Name :");
            Bank otherBank;
            if (BankService.IsBankExists(BankStore.Banks, otherBankName))
            {
                otherBank = BankService.GetBank(BankStore.Banks, otherBankName);
                string beneficiaryUsername;
                double amount;
                while (!AccountHolderService.IsAccountHolderExists(otherBank, beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
                {
                    Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        Console.Clear();
                        return;
                    }
                }

                AccountHolder beneficiary = AccountHolderService.GetAccountHolder(otherBank, beneficiaryUsername);


                amount = Utilities.GetDoubleInput("Enter Amount to transfer", "Invalid input, Enter valid Amount :");

                amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

                if (AccountHolderService.IsSufficientFundsAvailable(accountHolder, amount))
                {
                    Transaction transaction = new Transaction()
                    {
                        SrcAccountNumber = accountHolder.AccountNumber,
                        DestAccountNumber = beneficiary.AccountNumber,
                        SrcBankID = accountHolder.BankID,
                        DestBankID = beneficiary.BankID,
                        ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                        CreatedOn = DateTime.Now.ToString("f"),
                        CreatedBy = accountHolder.AccountNumber,
                        Amount = amount,
                        Type = TransactionType.Transfer,
                        IsReverted = false
                    };

                    if(AccountHolderService.TransferFunds(accountHolder, beneficiary, transaction))
                    {
                        Utilities.DisplayMessage("Amount Successfull Transfered \n" +
                            "Total closing amount " + AccountHolderService.GetAccountBalance(accountHolder));
                    }
                    else
                    {
                        Utilities.DisplayMessage("Amount Transfer Failed \n" +
                            "Total closing amount " + AccountHolderService.GetAccountBalance(accountHolder));
                    }
                }
                else
                {
                    Utilities.DisplayMessage("Insufficent Funds");
                }
            }
            else
            {
                Utilities.DisplayMessage("Bank does not exists");
            }
        }

        public static void RevertTransaction(Bank bank)
        {
            string username = Utilities.GetStringInput("Enter Username :");
   
            if(AccountHolderService.IsAccountHolderExists(bank, username))
            {
                AccountHolder accountHolder = AccountHolderService.GetAccountHolder(bank, username);

                string transactionID = Utilities.GetStringInput("Enter Transaction ID :");
                if ( TransactionService.IsTransactionExists(accountHolder, transactionID) )
                {
                    Transaction transaction = TransactionService.GetTransaction(accountHolder, transactionID);

                    if( transaction.Type == TransactionType.Credit || transaction.Type == TransactionType.Debit )
                    {
                       if( TransactionService.RevertTransaction(accountHolder, transaction) )
                        {
                            Utilities.DisplayMessage("Transaction Successfully Reverted");
                        }
                       else
                        {
                            Utilities.DisplayMessage("Revert Transaction failed");
                        }
                    }
                    else if( transaction.SrcBankID.Equals(transaction.DestBankID) )
                    {
                        RevertTransactionSameBank(bank, accountHolder, transaction);
                    }
                    else
                    {
                        RevertTransactionOtherBank(bank, accountHolder, transaction);
                    }
                }
                else
                {
                    Utilities.DisplayMessage("Transaction Does not exist");
                }    
            }
            else
            {
                Utilities.DisplayMessage("Account Holder does not exist");
            }

        }

        public static void RevertTransactionSameBank(Bank bank, AccountHolder accountHolder, Transaction transaction)
        {
            AccountHolder beneficiary = AccountHolderService.GetAccountHolderThroughID(bank, transaction.DestAccountNumber);
            Transaction beneficiaryTransaction = TransactionService.GetTransaction(beneficiary, transaction.ID);

            if (TransactionService.RevertTransaction(accountHolder, transaction) && TransactionService.RevertTransaction(beneficiary, beneficiaryTransaction))
            {
                Utilities.DisplayMessage("Transaction Successfully Reverted");
            }
            else
            {
                Utilities.DisplayMessage("Revert Transaction failed");
            }

        }

        public static void RevertTransactionOtherBank(Bank bank, AccountHolder accountHolder, Transaction transaction)
        {
            Bank beneficiaryBank = BankService.GetBank(BankStore.Banks, transaction.DestBankID);
            AccountHolder beneficiary = AccountHolderService.GetAccountHolderThroughID(beneficiaryBank, transaction.DestAccountNumber);
            Transaction beneficiaryTransaction = TransactionService.GetTransaction(beneficiary, transaction.ID);

            if (TransactionService.RevertTransaction(accountHolder, transaction) && TransactionService.RevertTransaction(beneficiary, beneficiaryTransaction))
            {
                Utilities.DisplayMessage("Transaction Successfully Reverted");
            }
            else
            {
                Utilities.DisplayMessage("Revert Transaction failed");
            }
        }
    }
}
