using System;
using System.Collections.Generic;

namespace Bank
{
    public class BankManagementApplication
    {
        public static BankStore Banks;
        public static BankServices BankService;
        public static CustomerServices CustomerService;
        public static TransactionServices TransactionService;
        static void Main(string[] args)
        {
            Banks = new BankStore();
            BankService = new BankServices();
            CustomerService = new CustomerServices();
            TransactionService = new TransactionServices();
     
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("Welcome to the Bank Management System\n " +
                "\n Select and option from the menu : \n" +
                "---------------------------------------------------------\n" +
                "1. Setup a new Bank\n" +
                "2. Enter a Existing bank\n" +
                "3. Save");

            MainMenuOptions menuOption = Utilities.GetStartMenuOption();
            switch (menuOption)
            {
                case MainMenuOptions.SetupNewBank:
                    Console.Clear();
                    Console.WriteLine("Enter Bank Name :");
                    string bankName = Utilities.GetStringInput();
                    if (!BankService.CheckIfBankExists(Banks.BankList, bankName))
                    {
                        Bank bank = new Bank();
                        BankService.AddBank(Banks.BankList, bank, bankName);
                        //Console.Clear();
                        Console.WriteLine("Enter password for Admin Account");
                        BankStaff admin = new BankStaff();
                        BankService.SetupNewBankStaff(admin, "Admin", "Admin", Utilities.GetStringInput());
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
                    }
                    MainMenu();
                    break;

                case MainMenuOptions.OpenExistingBank:
                    Console.Clear();
                    Console.WriteLine("Enter Bank Name: ");
                    bankName = Utilities.GetStringInput();
                    if (BankService.CheckIfBankExists(Banks.BankList, bankName))
                    {
                        BankMenu(BankService.GetBank(Banks.BankList, bankName));
                    }
                    else
                    {
                        Console.WriteLine("Bank does not exists");
                        MainMenu();
                    }
                    break;

                case MainMenuOptions.Save:
                    MainMenu();
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    MainMenu();
                    break;
            }
        }

        public static void BankMenu(Bank bank)
        {
            Console.Clear();
            Console.WriteLine("Welcome to " + bank.Name + " Bank :\n" +
                "-------------------------------------------------------\n" +
                "1. Login as BankStaff\n" +
                "2. Login as Customer\n" +
                "3. Go Back");

            BankMenuOptions menuOption = Utilities.GetBankMenuOption();

            switch (menuOption)
            {
                case global::Bank.BankMenuOptions.LoginBankStaff:
                    Console.Clear();
                    Console.WriteLine("Enter Username : ");
                    string username = Utilities.GetStringInput();
                    if (BankService.CheckIfStaffExists(bank, username))
                    {
                        Console.WriteLine("Enter Password : ");
                        BankStaff bankStaff = BankService.GetBankStaff(bank, username);
                        while (!bankStaff.Password.Equals(Utilities.GetStringInput()))
                        {
                            Console.WriteLine("Incorrect Password Entered, Enter again : ");
                        }
                        Console.Clear();
                        StaffMenu(bank, bankStaff);
                    }
                    else
                    {
                        Console.WriteLine("User does not exists\n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        BankMenu(bank);
                    }
                    break;

                case global::Bank.BankMenuOptions.LoginCustomer:
                    Console.Clear();
                    Console.WriteLine("Enter Username : ");
                    username = Console.ReadLine();
                    if (CustomerService.CheckIfCustomerExists(bank, username))
                    {
                        Console.WriteLine("Enter Password : ");
                        AccountHolder customer = CustomerService.GetCustomer(bank, username);
                        while (!customer.Password.Equals(Utilities.GetStringInput()))
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
                        BankMenu(bank);
                    }
                    break;
                case global::Bank.BankMenuOptions.GoBack:
                    Console.Clear();
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Selection\n");
                    BankMenu(bank);
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
                "5. Modify Service Charges for same bank\n" +
                "6. Modify Service Charges for different bank\n" +
                "7. View Transaction History\n" +
                "8. Revert a Transaction\n" +
                "9. Logout");

            StaffMenuOptions menuOption = Utilities.GetStaffMenuOption();

            switch (menuOption)
            {
                case global::Bank.StaffMenuOptions.CreateNewCustomerAccount:
                    Console.Clear();
                    CreateNewCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.UpdateCustomerAccount:
                    Console.Clear();
                    UpdateCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.DeleteCustomerAccount:
                    Console.Clear();
                    DeleteCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.AddNewCurrency:
                    Console.Clear();
                    AddNewCurrency(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.ModifyServiceChargeSameBank:
                    Console.Clear();
                    ModifyServiceChargeSameBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.ModifyServiceChargeOtherBank:
                    Console.Clear();
                    ModifyServiceChargeOtherBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.ViewTransactionHistory:
                    Console.Clear();
                    ViewTransactionHistory(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.RevertTransaction:
                    Console.Clear();
                    RevertTransaction(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenuOptions.Logout:
                    BankMenu(bank);
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
                "4. View Transaction History\n" +
                "5. Logout");

            CustomerMenuOptions menuOption = Utilities.GetCustomerMenuOption();

            switch (menuOption)
            {
                case CustomerMenuOptions.Deposit:
                    Console.Clear();
                    Deposit(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOptions.Withdraw:
                    Console.Clear();
                    Withdraw(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOptions.Transfer:
                    Console.Clear();
                    TransferFundsMenu(bank, customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOptions.ViewTransactionHistory:
                    Console.Clear();
                    ViewTransactionHistory(customer);
                    CustomerMenu(bank, customer);
                    break;

                case CustomerMenuOptions.Logout:
                    BankMenu(bank);
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
            Console.WriteLine("Enter Name :");
            name = Utilities.GetStringInput();
            Console.WriteLine("Enter Username :");
            while (CustomerService.CheckIfCustomerExists(bank, username = Utilities.GetStringInput()))
            {
                Console.WriteLine("User already exist enter a new Username or 0 to exit : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("Enter Password : ");
            string password = Utilities.GetStringInput();
            AccountHolder customer = new AccountHolder();
            CustomerService.SetupNewCustomer(bank, customer, name, username, password);
            CustomerService.AddCustomer(bank, customer);
            Console.WriteLine("Customer Successfull added\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void UpdateCustomerAccount(Bank bank)
        {
            string username;
            Console.WriteLine("Enter Username :");
            while ( !CustomerService.CheckIfCustomerExists(bank, username = Utilities.GetStringInput()))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            
            Console.WriteLine("Enter New Password : ");
            CustomerService.GetCustomer(bank,username).Password = Utilities.GetStringInput();
            Console.WriteLine("Password Updated Successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void DeleteCustomerAccount(Bank bank)
        {
            string username;
            Console.WriteLine("Enter Username :");
           while ( !CustomerService.CheckIfCustomerExists(bank, username = Utilities.GetStringInput()))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }
            
            Console.WriteLine("Are you sure you want to delete Customer: " + username + ". Enter Y to continue or N to cancel");

            string confirmatiom;
            while (!(confirmatiom = Utilities.GetStringInput()).Equals("Y"))
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
            Console.WriteLine("Enter Currency Code :");
            while ( BankService.CheckIfCurrencyExists(bank, currencyCode = Utilities.GetStringInput()) )
            {
                Console.WriteLine("Currency already Exists, Enter a new Currency Code or 0 to exit :");
                if (currencyCode.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("Enter Currency Name : ");
            currencyName = Utilities.GetStringInput();
            Console.WriteLine("Enter Exchange Rate : ");
            exchangeRate = Utilities.GetExchangeRateFromUser();

            Currency newCurrency = new Currency();
            BankService.SetupNewCurrency(newCurrency, currencyName, currencyCode, exchangeRate);
            BankService.AddCurrency(bank, newCurrency);

            Console.Write("Currency succesfully added\n" +
                "Press any button to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void ModifyServiceChargeSameBank(Bank bank)
        {
            double newRate;
            Console.WriteLine("Enter Rate for Same Bank RTGS :");
            newRate = Utilities.GetExchangeRateFromUser();
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.RTGS);

            Console.WriteLine("Enter Rate for Same Bank IMPS :");
            newRate = Utilities.GetExchangeRateFromUser();
            BankService.SetSameBankRate(bank, newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for same bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ModifyServiceChargeOtherBank(Bank bank)
        {
            double newRate;
            Console.WriteLine("Enter Rate for Other Bank RTGS :");
            newRate = Utilities.GetExchangeRateFromUser();
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.RTGS);

            Console.WriteLine("Enter Rate for Other Bank IMPS :");
            newRate = Utilities.GetExchangeRateFromUser();
            BankService.SetOtherBankRate(bank, newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for other bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ViewTransactionHistory(Bank bank)
        {
            string accountHolderUsername;
            Console.WriteLine("Enter Customer Username :");
            while( !CustomerService.CheckIfCustomerExists(bank, accountHolderUsername = Utilities.GetStringInput()))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

            AccountHolder customer = CustomerService.GetCustomer(bank, accountHolderUsername);
            foreach (Transaction transaction in customer.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SourceAccount + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestinationAccount + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ViewTransactionHistory(AccountHolder accountHolder)
        {            
            foreach(Transaction transaction in accountHolder.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Source Account Number - " + transaction.SourceAccount + "\n");
                Console.WriteLine("Destination Account Number - " + transaction.DestinationAccount + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Deposit(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            Console.WriteLine("Enter Amount to Deposit :");
            amount = Utilities.GetAmount();
            Transaction newTransaction = new Transaction();
            TransactionService.SetupNewTransaction(newTransaction, accountHolder.AccountID, amount, bank.ID, accountHolder.AccountID, bank.Name, TransactionTypes.Deposit);

            TransactionService.DepositAmount(accountHolder,newTransaction);

            Console.WriteLine("Amount Deposited Successfully\n" +
                "Total Closing Amount : " + CustomerService.GetTotalAccountBalance(accountHolder) + " \n" +
                "\n Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Withdraw(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            Console.WriteLine("Enter Amount to Withdraw : ");
            amount = Utilities.GetAmount();
            Transaction newTransaction = new Transaction();
            TransactionService.SetupNewTransaction(newTransaction, accountHolder.AccountID, amount, bank.ID, accountHolder.AccountID, bank.Name, TransactionTypes.Withdrawl);

            if ( TransactionService.CheckIfSufficientFundsAvailable(accountHolder,amount) )
            {
                TransactionService.WithdrawAmount(accountHolder, newTransaction);

                Console.WriteLine("Amount Withdrawed Successfully\n" +
                "Total Closing Amount : " + CustomerService.GetTotalAccountBalance(accountHolder) + " \n" +
                "\n Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + CustomerService.GetTotalAccountBalance(accountHolder) + " \n" +
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

            FundTransferOptions bankOption = Utilities.GetTransferFundsMenuOption();

            switch(bankOption)
            {
                case FundTransferOptions.SameBankRTGS:
                    Console.Clear();
                    SameBankTransfer(bank, accountHolder, FundTransferOptions.SameBankRTGS);
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOptions.SameBankIMPS:
                    Console.Clear();
                    SameBankTransfer(bank, accountHolder, FundTransferOptions.SameBankIMPS);
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOptions.OtherBankRTGS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOptions.OtherBankRTGS);
                    CustomerMenu(bank, accountHolder);
                    break;

                case FundTransferOptions.OtherBankIMPS:
                    Console.Clear();
                    OtherBankTransfer(bank, accountHolder, FundTransferOptions.OtherBankIMPS);
                    CustomerMenu(bank, accountHolder);
                    break;

                default:
                    Console.WriteLine("Invlaid Selection");
                    TransferFundsMenu(bank, accountHolder);
                    break;

            }
        }

        public static void SameBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOptions bankOption)
        {
            string beneficiaryUsername;
            double amount;
            Console.WriteLine("Enter Benificary's Username : ");
            while( !CustomerService.CheckIfCustomerExists(bank,beneficiaryUsername = Utilities.GetStringInput()) )
            {
                Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    Console.Clear();
                    return;
                }

            }

            AccountHolder beneficiary = CustomerService.GetCustomer(bank, beneficiaryUsername);

            Console.WriteLine("Enter Amount to transfer :");
            amount = Utilities.GetAmount();

            amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

            if (TransactionService.CheckIfSufficientFundsAvailable(accountHolder, amount))
            {
                Transaction newDebitTransaction = new Transaction();
                TransactionService.SetupNewTransaction(newDebitTransaction, accountHolder.AccountID, beneficiary.AccountID, amount, bank.ID, accountHolder.AccountID, accountHolder.BankName, beneficiary.BankName, TransactionTypes.TransferDebit);

                Transaction newCreditTransaction = new Transaction();
                TransactionService.SetupNewTransaction(newDebitTransaction, accountHolder.AccountID, beneficiary.AccountID, amount, bank.ID, accountHolder.AccountID, accountHolder.BankName, beneficiary.BankName, TransactionTypes.TransferCredit);

                TransactionService.TransferFunds(accountHolder, beneficiary, newCreditTransaction, newDebitTransaction);

                Console.WriteLine("Amount Successfull Transfered \n" +
                    "Total closing amount " + CustomerService.GetTotalAccountBalance(accountHolder) +
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

        public static void OtherBankTransfer(Bank bank, AccountHolder customer, FundTransferOptions bankOption)
        {
            Console.WriteLine("Enter Bank Name :");
            string otherBankName = Utilities.GetStringInput();
            Bank otherBank;
            if (BankService.CheckIfBankExists(Banks.BankList, otherBankName))
            {
                otherBank = BankService.GetBank(Banks.BankList, otherBankName);
                string beneficiaryUsername;
                double amount;
                Console.WriteLine("Enter Benificary's Username : ");
                while (!CustomerService.CheckIfCustomerExists(otherBank, beneficiaryUsername = Utilities.GetStringInput()) )
                {
                    Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        Console.Clear();
                        return;
                    }
                }

                AccountHolder beneficiary = CustomerService.GetCustomer(otherBank, beneficiaryUsername);


                Console.WriteLine("Enter Amount to transfer :");
                amount = Utilities.GetAmount();

                amount = TransactionService.GetTrasferAmount(bank, bankOption, amount);

                if (TransactionService.CheckIfSufficientFundsAvailable(customer, amount))
                {
                    Transaction newDebitTransfer = new Transaction();
                    TransactionService.SetupNewTransaction(newDebitTransfer, customer.AccountID, beneficiary.AccountID, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferDebit);

                    Transaction newCreditTransfer = new Transaction();
                    TransactionService.SetupNewTransaction(newDebitTransfer, customer.AccountID, beneficiary.AccountID, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferCredit);


                    TransactionService.TransferFunds(customer, beneficiary, newDebitTransfer, newCreditTransfer);

                    Console.WriteLine("Amount Successfull Transfered \n" +
                        "Total closing amount " + CustomerService.GetTotalAccountBalance(customer) +
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
            string transactionID;
            Console.WriteLine("Enter Transaction ID :");
            transactionID = Utilities.GetStringInput();

            if (TransactionService.CheckIfTransactionExists(bank,transactionID))
            {
                if (TransactionService.GetSenderUsername(bank,transactionID).Equals(TransactionService.GetBeneficiaryUsername(bank, transactionID)) )
                {
                    AccountHolder accountHolder = CustomerService.GetCustomer(bank, TransactionService.GetSenderUsername(bank, transactionID));
                    Transaction transaction = TransactionService.GetTransaction(accountHolder, transactionID);

                    TransactionService.RevertTransaction(accountHolder, transaction);


                }
                else if(TransactionService.GetSenderBankName(bank,transactionID).Equals(TransactionService.GetBeneficiaryBankName(bank,transactionID)) )
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

                    Bank beneficiaryBank = BankService.GetBank(Banks.BankList, debitTransaction.DestinationAccountBankName);
                    AccountHolder beneficiary = CustomerService.GetCustomer(beneficiaryBank, debitTransaction.DestinationAccountBankName);
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
