using System;
using System.Collections.Generic;

namespace Bank
{
    public class BankApplication
    {
        public static BankServices BankService;
        public static CustomerServices CustomerService;
        public static TransactionServices TransactionService;
        static void Main()
        {
            BankService = new BankServices();
            CustomerService = new CustomerServices();
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
                        bank.DefaultCurrency.CurrencyCode = "INR";
                        bank.DefaultCurrency.Name = "Indian National Rupee";
                        bank.DefaultCurrency.ExcahngeRate = 0;
                        bank.CurrenyList.Add(bank.DefaultCurrency);

                        if (!BankService.AddBank(BankStore.Banks, bank))
                        {
                            Console.WriteLine("Failed to add Bank\n" +
                                "Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            MainMenu();
                        }

                        BankStaff admin = new BankStaff();
                        admin.Name = Utilities.GetStringInput("Enter name for Admin account :"); ;
                        admin.UserName = Utilities.GetStringInput("Enter Username for Admin account :"); ;
                        admin.Password = Utilities.GetStringInput("Enter password for Admin Account :");
                        admin.AccountID = admin.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                        BankService.AddBankStaff(bank, admin);

                        Console.WriteLine("Bank Successfull created and added \n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Bank already exists\n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    MainMenu();
                    break;

                case MainMenuOption.LogintoExistingBank:
                    Console.Clear();
                    bankName = Utilities.GetStringInput("Enter Bank Name :");
                    if (BankService.IsBankExists(BankStore.Banks, bankName))
                    {
                        Login(BankService.GetBank(BankStore.Banks, bankName));
                    }
                    else
                    {
                        Console.WriteLine("Bank does not exists\n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu();
                    }
                    break;

                case MainMenuOption.Exit:
                    return; 

                default:
                    Console.WriteLine("Invalid Selection");
                    MainMenu();
                    break;
            }
        }

        public static void Login(Bank bank)
        {
            Console.Clear();
            Console.WriteLine("Welcome to " + bank.Name + " Bank :\n" +
                "-------------------------------------------------------\n" +
                "1. Login\n" +
                "2. Go Back");

            BankMenuOption menuOption = (BankMenuOption)Utilities.GetIntInput();

            switch (menuOption)
            {
                case BankMenuOption.Login:
                    Console.Clear();
                    string username = Utilities.GetStringInput("Enter Username");
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
                    else if(CustomerService.IsCustomerExists(bank, username))
                    {
                        AccountHolder customer = CustomerService.GetCustomer(bank, username);
                        while (!customer.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                        {
                            Console.WriteLine("Incorrect Password Entered, Enter again : ");
                        }
                        Console.Clear();
                        CustomerMenu(bank, customer);
                    }
                    else
                    {
                        Console.WriteLine("User does not exists\n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        Login(bank);
                    }
                    break;

                case BankMenuOption.GoBack:
                    Console.Clear();
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Selection\n");
                    Login(bank);
                    break;
            }


        }


        public static void StaffMenu(Bank bank, BankStaff bankStaff)
        {
            Console.WriteLine("Welcome " + bankStaff.Name + " :\n" +
                "----------------------------------------------------\n" +
                "1. Create new Customer Account\n" +
                "2. Update Customer Account\n" +
                "3. Delete Customer Account\n" +
                "4. Add New Currency\n" +
                "5. Update Service Charges for same bank\n" +
                "6. Update Service Charges for different bank\n" +
                "7. Transaction History\n" +
                "8. Revert a Transaction\n" +
                "9. Logout");

            StaffMenuOption menuOption = (StaffMenuOption)Utilities.GetIntInput();

            switch (menuOption)
            {
                case StaffMenuOption.CreateNewCustomerAccount:
                    Console.Clear();
                    CreateNewCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.UpdateCustomerAccount:
                    Console.Clear();
                    UpdateCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case StaffMenuOption.DeleteCustomerAccount:
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
                    Login(bank);
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
                    Login(bank);
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
            while (CustomerService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User already exist enter a new Username or 0 to exit : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }
            AccountHolder customer = new AccountHolder();
            customer.Name = name;
            customer.UserName = username;
            customer.Password = Utilities.GetStringInput("Enter Password :");
            customer.PhoneNumber = Utilities.GetStringInput("Enter User Phone Number :");
            customer.AccountID = customer.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            customer.AccountBalance = 0;
            customer.BankID = bank.ID;

            if(!CustomerService.AddCustomer(bank, customer))
            {
                Console.WriteLine("Failed to add Customer" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Customer Successfull added\n" +
                "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            
        }

        public static void UpdateCustomerAccount(Bank bank)
        {
            string username;
            while ( !CustomerService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            CustomerService.GetCustomer(bank,username).Password = Utilities.GetStringInput("Enter New Password :");

            Console.WriteLine("Password Updated Successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void DeleteCustomerAccount(Bank bank)
        {
            string username;
           while ( !CustomerService.IsCustomerExists(bank, username = Utilities.GetStringInput("Enter Username :")))
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

            CustomerService.RemoveCustomer(bank,username);
            Console.WriteLine("Customer Successfully Removed\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
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

            Console.Write("Currency succesfully added\n" +
                "Press any button to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void UpdateServiceChargeSameBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for same bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void UpdateServiceChargeOtherBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for other bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TransactionHistory(Bank bank)
        {
            string accountHolderUsername;
            while( !CustomerService.IsCustomerExists(bank, accountHolderUsername = Utilities.GetStringInput("Enter Customer Username")))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

            AccountHolder customer = CustomerService.GetCustomer(bank, accountHolderUsername);
            foreach (Transaction transaction in customer.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SrcAccountID + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestAccountID + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TransactionHistory(AccountHolder accountHolder)
        {            
            foreach(Transaction transaction in accountHolder.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SrcAccountID + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestAccountID + "\n");
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
            Transaction newTransaction = new Transaction();
            TransactionService.SetupNewTransaction(newTransaction, accountHolder.AccountID, amount, bank.ID, accountHolder.AccountID, TransactionType.Deposit);

            TransactionService.DepositAmount(accountHolder,newTransaction);

            Console.WriteLine("Amount Deposited Successfully\n" +
                "Total Closing Amount : " + CustomerService.GetAccountBalance(accountHolder) + " \n" +
                "\n Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Withdraw(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            amount = Utilities.GetDoubleInput("Enter Amount to Withdraw", "Only Numbers Accepted, Enter Amount again");
            Transaction newTransaction = new Transaction();
            TransactionService.SetupNewTransaction(newTransaction, accountHolder.AccountID, amount, bank.ID, accountHolder.AccountID, TransactionType.Withdrawl);

            if ( TransactionService.IsSufficientFundsAvailable(accountHolder,amount) )
            {
                TransactionService.WithdrawAmount(accountHolder, newTransaction);

                Console.WriteLine("Amount Withdrawed Successfully\n" +
                "Total Closing Amount : " + CustomerService.GetAccountBalance(accountHolder) + " \n" +
                "\n Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + CustomerService.GetAccountBalance(accountHolder) + " \n" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
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
            while( !CustomerService.IsCustomerExists(bank,beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
            {
                Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    Console.Clear();
                    return;
                }

            }

            AccountHolder beneficiary = CustomerService.GetCustomer(bank, beneficiaryUsername);

            amount = Utilities.GetDoubleInput("Enter Amount to transfer","Invalid input, Enter valid Amount :");

            amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

            if (TransactionService.IsSufficientFundsAvailable(accountHolder, amount))
            {
                Transaction newDebitTransaction = new Transaction();
                TransactionService.SetupNewTransaction(newDebitTransaction, accountHolder.AccountID, beneficiary.AccountID, amount, bank.ID, accountHolder.AccountID, beneficiary.BankID, TransactionType.TransferDebit);

                Transaction newCreditTransaction = new Transaction();
                TransactionService.SetupNewTransaction(newDebitTransaction, accountHolder.AccountID, beneficiary.AccountID, amount, bank.ID, accountHolder.AccountID, beneficiary.BankID, TransactionType.TransferCredit);

                TransactionService.TransferFunds(accountHolder, beneficiary, newCreditTransaction, newDebitTransaction);

                Console.WriteLine("Amount Successfull Transfered \n" +
                    "Total closing amount " + CustomerService.GetAccountBalance(accountHolder) +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Insufficent Funds \n" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                CustomerMenu(bank, accountHolder);
            }
        }

        public static void OtherBankTransfer(Bank bank, AccountHolder customer, FundTransferOption bankOption)
        {
            string otherBankName = Utilities.GetStringInput("Enter Bank Name :");
            Bank otherBank;
            if (BankService.IsBankExists(BankStore.Banks, otherBankName))
            {
                otherBank = BankService.GetBank(BankStore.Banks, otherBankName);
                string beneficiaryUsername;
                double amount;
                while (!CustomerService.IsCustomerExists(otherBank, beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")) )
                {
                    Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        Console.Clear();
                        return;
                    }
                }

                AccountHolder beneficiary = CustomerService.GetCustomer(otherBank, beneficiaryUsername);


                amount = Utilities.GetDoubleInput("Enter Amount to transfer", "Invalid input, Enter valid Amount :");

                amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

                if (TransactionService.IsSufficientFundsAvailable(customer, amount))
                {
                    Transaction newDebitTransfer = new Transaction();
                    TransactionService.SetupNewTransaction(newDebitTransfer, customer.AccountID, beneficiary.AccountID, amount, customer.BankID, customer.AccountID, beneficiary.BankID, TransactionType.TransferDebit);

                    Transaction newCreditTransfer = new Transaction();
                    TransactionService.SetupNewTransaction(newDebitTransfer, customer.AccountID, beneficiary.AccountID, amount, customer.BankID, customer.AccountID, beneficiary.BankID, TransactionType.TransferCredit);


                    TransactionService.TransferFunds(customer, beneficiary, newDebitTransfer, newCreditTransfer);

                    Console.WriteLine("Amount Successfull Transfered \n" +
                        "Total closing amount " + CustomerService.GetAccountBalance(customer) +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();

                }
                else
                {
                    Console.WriteLine("Insufficent Funds \n" +
                        "Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    CustomerMenu(bank,customer);
                }
            }
            else
            {
                Console.WriteLine("Bank does not exists");
                CustomerMenu(bank, customer);
            }
        }

        public static void RevertTransaction(Bank bank)
        {
            string transactionID = Utilities.GetStringInput("Enter Transaction ID :");

            if (TransactionService.IsTransactionExists(bank,transactionID))
            {
                if (TransactionService.GetSenderUsername(bank,transactionID).Equals(TransactionService.GetBeneficiaryUsername(bank, transactionID)) )
                {
                    AccountHolder accountHolder = CustomerService.GetCustomer(bank, TransactionService.GetSenderUsername(bank, transactionID));
                    Transaction transaction = TransactionService.GetTransaction(accountHolder, transactionID);

                    TransactionService.RevertTransaction(accountHolder, transaction);


                }
                else if(TransactionService.GetSrcBankID(bank,transactionID).Equals(TransactionService.GetDestBankID(bank,transactionID)) )
                {
                    AccountHolder accountHolder = CustomerService.GetCustomer(bank, TransactionService.GetSenderUsername(bank, transactionID));
                    AccountHolder beneficiary = CustomerService.GetCustomer(bank, TransactionService.GetBeneficiaryUsername(bank, transactionID));

                    Transaction debitTransaction = TransactionService.GetTransaction(accountHolder, transactionID);
                    Transaction creditTransaction = TransactionService.GetTransaction(beneficiary, transactionID);

                    TransactionService.RevertTransaction(accountHolder, beneficiary, debitTransaction, creditTransaction);

                }
                else
                {
                    AccountHolder accountHolder = CustomerService.GetCustomer(bank, TransactionService.GetSenderUsername(bank, transactionID));
                    Transaction debitTransaction = TransactionService.GetTransaction(accountHolder, transactionID);

                    Bank beneficiaryBank = BankService.GetBankThroughID(BankStore.Banks, debitTransaction.DestBankID);
                    AccountHolder beneficiary = CustomerService.GetCustomerThroughID(beneficiaryBank, debitTransaction.DestAccountID);
                    Transaction creditTransaction = TransactionService.GetTransaction(beneficiary, transactionID);

                    TransactionService.RevertTransaction(accountHolder, beneficiary, debitTransaction, creditTransaction);
                }
            }
            else
            {
                Console.WriteLine("Transaction does not exists\n" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
