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
     
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("Welcome to the Bank Management System\n " +
                "\n Select an option from the menu : \n" +
                "---------------------------------------------------------\n" +
                "1. Setup a new Bank\n" +
                "2. Login to Existing Bank\n" +
                "3. Exit");

            MainMenuOption menuOption = (MainMenuOption)Utilities.GetIntInput();
            switch (menuOption)
            {
                case MainMenuOption.CreateNewBank:
                    Console.Clear();
                    string bankName = Utilities.GetStringInput("Enter Bank Name :");
                    if (!BankService.IsBankExists(BankStore.Banks, bankName))
                    {
                        Bank bank = new Bank();
                        bank.Name = bankName;
                        bank.ID = bank.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                        bank.SameBankRTGS = 0;
                        bank.SameBankIMPS = 5;
                        bank.OtherBankRTGS = 2;
                        bank.OtherBankIMPS = 6;
                        Currency defaultCurrency = new Currency();
                        defaultCurrency.CurrencyCode = "INR";
                        defaultCurrency.Name = "Indian National Rupee";
                        defaultCurrency.ExcahngeRate = 0;
                        defaultCurrency.IsDefault = true;
                        bank.Currencies.Add(defaultCurrency);

                        if (!BankService.AddBank(BankStore.Banks, bank))
                        {
                            Utilities.DisplayMessage("Failed to add Bank");
                            MainMenu();
                        }

                        BankStaff admin = new BankStaff();
                        admin.Name = Utilities.GetStringInput("Enter name for Admin account :"); ;
                        admin.UserName = Utilities.GetStringInput("Enter Username for Admin account :"); ;
                        admin.Password = Utilities.GetStringInput("Enter password for Admin Account :");
                        admin.AccountNumber = admin.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                        admin.EmployeeID = Convert.ToString(BankStore.Banks.Count + 1);
                        if(BankService.AddBankStaff(bank, admin))
                        {
                            Utilities.DisplayMessage("Admin Account Created Successfully\n" +
                                "Bank Successfull created and added");
                        }
                        else
                        {
                            Utilities.DisplayMessage("Could not add Admin account successfully");
                            MainMenu();
                        }
                    }
                    else
                    {
                        Utilities.DisplayMessage("Bank already exists");
                    }
                    MainMenu();
                    break;

                case MainMenuOption.Login:
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

                case MainMenuOption.Exit:
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
            else if (AccountHolderService.IsCustomerExists(bank, username))
            {
                AccountHolder customer = AccountHolderService.GetCustomer(bank, username);
                while (!customer.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                {
                    Console.WriteLine("Incorrect Password Entered, Enter again : ");
                }
                Console.Clear();
                CustomerMenu(bank, customer);
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

            StaffMenuOption menuOption = (StaffMenuOption)Utilities.GetIntInput();

            switch (menuOption)
            {
                case StaffMenuOption.CreateAccountHolder:
                    Console.Clear();
                    CreateNewCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.UpdateAccountHolder:
                    Console.Clear();
                    UpdateCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.DeleteAccountHolder:
                    Console.Clear();
                    DeleteCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.AddNewCurrency:
                    Console.Clear();
                    AddNewCurrency(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.UpdateServiceChargeSameBank:
                    Console.Clear();
                    UpdateServiceChargeSameBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.UpdateServiceChargeOtherBank:
                    Console.Clear();
                    UpdateServiceChargeOtherBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.TransactionHistory:
                    Console.Clear();
                    TransactionHistory(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.RevertTransaction:
                    Console.Clear();
                    RevertTransaction(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.Logout:
                    Console.Clear();
                    BankLogin(bank);
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    StaffMenu(bank, bankStaff);
                    break;
            }

        }

        public static void CustomerMenu(Bank bank, AccountHolder customer)
        {
            Console.WriteLine("Welcome " + customer.Name + " :\n" +
                "----------------------------------------------------\n" +
                "1. Deposit Amount\n" +
                "2. Withdraw Amount\n" +
                "3. Transfer funds\n" +
                "4. Transaction History\n" +
                "5. Logout");

            CustomerMenuOption menuOption = (CustomerMenuOption)Utilities.GetIntInput();

            switch (menuOption)
            {
                case CustomerMenuOption.Deposit:
                    Console.Clear();
                    Deposit(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOption.Withdraw:
                    Console.Clear();
                    Withdraw(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOption.Transfer:
                    Console.Clear();
                    TransferFundsMenu(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOption.TransactionHistory:
                    Console.Clear();
                    TransactionHistory(customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOption.Logout:
                    BankLogin(bank);
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    CustomerMenu(bank, customer);
                    break;
            }




        }

        public static void CreateNewCustomerAccount(Bank bank)
        {
            string name, username;
            name = Utilities.GetStringInput("Enter Name");
            while (AccountHolderService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
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
            accountHolder.AccountNumber = accountHolder.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            accountHolder.AccountBalance = 0;
            accountHolder.BankID = bank.ID;

            if(!AccountHolderService.AddAccountHolder(bank, accountHolder))
            {
                Utilities.DisplayMessage("Failed to add Account Holder");
            }
            else
            {
                Utilities.DisplayMessage("Account Holder Successfull added");
            }

            
        }

        public static void UpdateCustomerAccount(Bank bank)
        {
            string username;
            while ( !AccountHolderService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            AccountHolderService.GetCustomer(bank,username).Password = Utilities.GetStringInput("Enter New Password :");

            Utilities.DisplayMessage("Password Updated Successfully");

        }

        public static void DeleteCustomerAccount(Bank bank)
        {
            string username;
           while ( !AccountHolderService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            string confirmatiom;
            while (!(confirmatiom = Utilities.GetStringInput("Are you sure you want to delete Customer: " + username + ". Enter Y to continue or N to cancel")).Equals("Y"))
            {
                Console.WriteLine("Enter Y or N");
                if( confirmatiom.Equals("N") )
                {
                    Console.Clear();
                    return;
                }

            }

            AccountHolderService.RemoveCustomer(bank,username);
            Utilities.DisplayMessage("Customer Successfully Removed");
        }

        public static void AddNewCurrency(Bank bank)
        {
            string currencyCode, currencyName;
            double exchangeRate;
            while ( BankService.IsCurrencyExists(bank, currencyCode = Utilities.GetStringInput("Enter Currency Code :")) )
            {
                Console.WriteLine("Currency already Exists, Enter a new Currency Code or 0 to exit :");
                if (currencyCode.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            currencyName = Utilities.GetStringInput("Enter Currency Name :");
            exchangeRate = Utilities.GetDoubleInput("Enter Exchange Rate", "Only numbers are acccepted, Enter valid Rate again");

            Currency newCurrency = new Currency();
            BankService.SetupNewCurrency(newCurrency, currencyName, currencyCode, exchangeRate);
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
            while( !AccountHolderService.IsCustomerExists(bank, accountHolderUsername = Utilities.GetStringInput("Enter Customer Username")))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

            AccountHolder customer = AccountHolderService.GetCustomer(bank, accountHolderUsername);
            foreach (Transaction transaction in customer.Transactions)
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
                DestAccountNumber = accountHolder.AccountNumber,
                SrcBankID = accountHolder.BankID,
                CreatedOn = DateTime.Now.ToString("f"),
                CreatedBy = accountHolder.AccountNumber,
                ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                DestBankID = accountHolder.BankID,
                Amount = amount,
                Type = TransactionType.Credit,
                IsReverted = false
            };

            if(AccountHolderService.DepositAmount(accountHolder,amount) && TransactionService.AddTransaction(accountHolder, transaction))
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
            Transaction newTransaction = new Transaction()
            {
                SrcAccountNumber = accountHolder.AccountNumber,
                DestAccountNumber = accountHolder.AccountNumber,
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
                if(AccountHolderService.WithdrawAmount(accountHolder, amount) && TransactionService.AddTransaction(accountHolder, newTransaction))
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
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOption.SameBankIMPS:
                    Console.Clear();
                    SameBankTransfer(bank, accountHolder, FundTransferOption.SameBankIMPS);
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankRTGS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankRTGS);
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankIMPS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankIMPS);
                    CustomerMenu(bank, accountHolder);
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
            while( !AccountHolderService.IsCustomerExists(bank,beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
            {
                Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            AccountHolder beneficiary = AccountHolderService.GetCustomer(bank, beneficiaryUsername);

            amount = Utilities.GetDoubleInput("Enter Amount to transfer","Invalid input, Enter valid Amount :");

            amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

            if (AccountHolderService.IsSufficientFundsAvailable(accountHolder, amount))
            {
                Transaction debitTransaction = new Transaction()
                {
                    SrcAccountNumber = accountHolder.AccountNumber,
                    DestAccountNumber = beneficiary.AccountNumber,
                    SrcBankID = accountHolder.BankID,
                    DestBankID = beneficiary.BankID,
                    ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                    CreatedOn = DateTime.Now.ToString("f"),
                    CreatedBy = accountHolder.AccountNumber,
                    Amount = amount,
                    Type = TransactionType.Debit,
                    IsReverted = false
                };

                Transaction creditTransaction = new Transaction()
                {
                    SrcAccountNumber = accountHolder.AccountNumber,
                    DestAccountNumber = beneficiary.AccountNumber,
                    SrcBankID = accountHolder.BankID,
                    DestBankID = beneficiary.BankID,
                    ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                    CreatedOn = DateTime.Now.ToString("f"),
                    CreatedBy = accountHolder.AccountNumber,
                    Amount = amount,
                    Type = TransactionType.Credit,
                    IsReverted = false
                };

                if(AccountHolderService.TransferFunds(accountHolder, beneficiary, amount) && TransactionService.AddTransaction(accountHolder, debitTransaction) && TransactionService.AddTransaction(beneficiary, creditTransaction))
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
                while (!AccountHolderService.IsCustomerExists(otherBank, beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
                {
                    Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        Console.Clear();
                        return;
                    }
                }

                AccountHolder beneficiary = AccountHolderService.GetCustomer(otherBank, beneficiaryUsername);


                amount = Utilities.GetDoubleInput("Enter Amount to transfer", "Invalid input, Enter valid Amount :");

                amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

                if (AccountHolderService.IsSufficientFundsAvailable(accountHolder, amount))
                {
                    Transaction newDebitTransfer = new Transaction()
                    {
                        SrcAccountNumber = accountHolder.AccountNumber,
                        DestAccountNumber = beneficiary.AccountNumber,
                        SrcBankID = accountHolder.BankID,
                        DestBankID = beneficiary.BankID,
                        ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                        CreatedOn = DateTime.Now.ToString("f"),
                        CreatedBy = accountHolder.AccountNumber,
                        Amount = amount,
                        Type = TransactionType.Debit,
                        IsReverted = false
                    };

                    Transaction newCreditTransfer = new Transaction()
                    {
                        SrcAccountNumber = accountHolder.AccountNumber,
                        DestAccountNumber = beneficiary.AccountNumber,
                        SrcBankID = accountHolder.BankID,
                        DestBankID = beneficiary.BankID,
                        ID = "TXN" + accountHolder.BankID + accountHolder.AccountNumber + DateTime.Now.ToString("ddMMyyyy"),
                        CreatedOn = DateTime.Now.ToString("f"),
                        CreatedBy = accountHolder.AccountNumber,
                        Amount = amount,
                        Type = TransactionType.Credit,
                        IsReverted = false
                    };


                    if(AccountHolderService.TransferFunds(accountHolder, beneficiary, amount) && TransactionService.AddTransaction(accountHolder, newDebitTransfer) && TransactionService.AddTransaction(beneficiary, newCreditTransfer))
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
   
            if(AccountHolderService.IsCustomerExists(bank, username))
            {
                AccountHolder accountHolder = AccountHolderService.GetCustomer(bank, username);

                string transactionID = Utilities.GetStringInput("Enter Transaction ID :");
                if (TransactionService.IsTransactionExists(accountHolder, transactionID))
                {
                    Transaction transaction = TransactionService.GetTransaction(accountHolder, transactionID);

                    if(transaction.SrcAccountNumber.Equals(transaction.DestAccountNumber))
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
                    else if(transaction.SrcBankID.Equals(transaction.DestBankID))
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
            AccountHolder beneficiary = AccountHolderService.GetCustomerThroughID(bank, transaction.DestAccountNumber);
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
            AccountHolder beneficiary = AccountHolderService.GetCustomerThroughID(beneficiaryBank, transaction.DestAccountNumber);
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
