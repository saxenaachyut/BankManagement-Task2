using System;
using System.Collections.Generic;

namespace Bank
{
    public class BankManagementApplication
    {
        public static List<Bank> BankList;
        public static BankServices BankService;
        static void Main(string[] args)
        {
            BankList = new List<Bank>();
            BankService = new BankServices();
           
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

            FirstMenu menuOption = Utilities.GetStartMenuOption();
            switch (menuOption)
            {
                case FirstMenu.SetupNewBank:
                    Console.Clear();
                    Console.WriteLine("Enter Bank Name :");
                    string bankName = Utilities.GetUserInput();
                    if (!IfBankExists(BankList, bankName))
                    {
                        Bank bank = new Bank();
                        AddBank(BankList, bank, bankName);
                        //Console.Clear();
                        Console.WriteLine("Enter password for Admin Account");
                        BankStaff admin = new BankStaff();
                        BankService.SetupNewBankStaff(admin, "Admin", "Admin", Utilities.GetUserInput());
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

                case FirstMenu.OpenExistingBank:
                    Console.Clear();
                    Console.WriteLine("Enter Bank Name: ");
                    bankName = Utilities.GetUserInput();
                    if (IfBankExists(BankList, bankName))
                    {
                        BankMenu(GetBank(BankList, bankName));
                    }
                    else
                    {
                        Console.WriteLine("Bank does not exists");
                        MainMenu();
                    }
                    break;

                case FirstMenu.Save:
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

            SecondMenu menuOption = Utilities.GetBankMenuOption();

            switch (menuOption)
            {
                case SecondMenu.LoginBankStaff:
                    Console.Clear();
                    Console.WriteLine("Enter Username : ");
                    string username = Utilities.GetUserInput();
                    if (BankService.IfStaffExists(bank, username))
                    {
                        Console.WriteLine("Enter Password : ");
                        BankStaff bankStaff = BankService.GetBankStaff(bank, username);
                        while (!bankStaff.Password.Equals(Utilities.GetUserInput()))
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

                case SecondMenu.LoginCustomer:
                    Console.Clear();
                    Console.WriteLine("Enter Username : ");
                    username = Console.ReadLine();
                    if (BankService.IFCustomerExists(bank,username))
                    {
                        Console.WriteLine("Enter Password : ");
                        Customer customer = BankService.GetCustomer(bank, username);
                        while (!customer.Password.Equals(Utilities.GetUserInput()))
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
                case SecondMenu.GoBack:
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

            BankStaffMenu menuOption = Utilities.GetStaffMenuOption();

            switch (menuOption)
            {
                case BankStaffMenu.CreateNewCustomerAccount:
                    Console.Clear();
                    CreateNewCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.UpdateCustomerAccount:
                    Console.Clear();
                    UpdateCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.DeleteCustomerAccount:
                    Console.Clear();
                    DeleteCustomerAccount(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.AddNewCurrency:
                    Console.Clear();
                    AddNewCurrency(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.ModifyServiceChargeSameBank:
                    Console.Clear();
                    ModifyServiceChargeSameBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.ModifyServiceChargeOtherBank:
                    Console.Clear();
                    ModifyServiceChargeOtherBank(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.ViewTransactionHistory:
                    Console.Clear();
                    ViewTransactionHistory(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.RevertTransaction:
                    Console.Clear();
                    RevertTransaction(bank);
                    StaffMenu(bank, bankStaff);
                    break;

                case BankStaffMenu.Logout:
                    BankMenu(bank);
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    StaffMenu(bank, bankStaff);
                    break;
            }

        }

        public static void CustomerMenu(Bank bank, Customer customer)
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

        public static Boolean IfBankExists(List<Bank> bankList, string bankName)
        {
            foreach (Bank bank in bankList)
            {
                if (bank.Name == bankName)
                {
                    return true;
                }
            }

            return false;
        }

        public static void AddBank(List<Bank> bankList, Bank bank, string bankName)
        {
            bank.Name = bankName;
            bank.ID = bank.Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            bank.AccountsList = new List<Customer>();
            bank.StaffList = new List<BankStaff>();
            bank.CurrenyList = new List<Currency>();
            bank.SameBankRTGS = 0;
            bank.SameBankIMPS = 5;
            bank.OtherBankRTGS = 2;
            bank.OtherBankIMPS = 6;
            Currency defaultCurrency = new Currency();
            defaultCurrency.CurrencyCode = "INR";
            defaultCurrency.Name = "Indian National Rupee";
            defaultCurrency.ExcahngeRate = 0;
            bank.CurrenyList.Add(defaultCurrency);

            bankList.Add(bank);
        }

        public static Bank GetBank(List<Bank> bankList, string bankName)
        {
            foreach (Bank bank in bankList)
            {
                if (bank.Name.Equals(bankName))
                {
                    return bank;
                }
            }

            return null;

        }


        public static void CreateNewCustomerAccount(Bank bank)
        {
            string name, username;
            Console.WriteLine("Enter Name :");
            name = Utilities.GetUserInput();
            Console.WriteLine("Enter Username :");
            while (BankService.IFCustomerExists(bank, username = Utilities.GetUserInput()))
            {
                Console.WriteLine("User already exist enter a new Username or 0 to exit : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("Enter Password : ");
            string password = Utilities.GetUserInput();
            Customer customer = new Customer();
            BankService.SetupNewCustomer(bank, customer, name, username, password);
            BankService.AddCustomer(bank, customer);
            Console.WriteLine("Customer Successfull added\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void UpdateCustomerAccount(Bank bank)
        {
            string username;
            Console.WriteLine("Enter Username :");
            while ( !BankService.IFCustomerExists(bank, username = Utilities.GetUserInput()))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            
            Console.WriteLine("Enter New Password : ");
            BankService.GetCustomer(bank,username).Password = Utilities.GetUserInput();
            Console.WriteLine("Password Updated Successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void DeleteCustomerAccount(Bank bank)
        {
            string username;
            Console.WriteLine("Enter Username :");
           while ( !BankService.IFCustomerExists(bank, username = Utilities.GetUserInput()))
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
            while (!(confirmatiom = Utilities.GetUserInput()).Equals("Y"))
            {
                Console.WriteLine("Enter Y or N");
                if( confirmatiom.Equals("N") )
                {
                    Console.Clear();
                    return;
                }

            }

            BankService.RemoveCustomer(bank,username);
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
            while ( BankService.IFCurrencyExists(bank, currencyCode = Utilities.GetUserInput()) )
            {
                Console.WriteLine("Currency already Exists, Enter a new Currency Code or 0 to exit :");
                if (currencyCode.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("Enter Currency Name : ");
            currencyName = Utilities.GetUserInput();
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
            string customerUsername;
            Console.WriteLine("Enter Customer Username :");
            while( !BankService.IFCustomerExists(bank, customerUsername = Utilities.GetUserInput()))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

            Customer customer = BankService.GetCustomer(bank, customerUsername);
            foreach (Transaction transaction in customer.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Sender - " + transaction.Sender + "\n");
                Console.WriteLine("Benificiary - " + transaction.Beneficiary + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ViewTransactionHistory(Customer customer)
        {            
            foreach(Transaction transaction in customer.TransactionList)
            {
                Console.WriteLine("Transaction ID - " + transaction.ID + "\n");
                Console.WriteLine("Transaction Date - " + transaction.TransactionDate + "\n");
                Console.WriteLine("Transaction Type - " + transaction.TransactionType + "\n");
                Console.WriteLine("Sender - " + transaction.Sender + "\n");
                Console.WriteLine("Benificiary - " + transaction.Beneficiary + "\n");
                Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void SetupNewTransaction(Transaction transaction, string senderUsername, double amount, string bankID, string accountID, string senderBankName, TransactionTypes transationType)
        {
            transaction.Sender = senderUsername;
            transaction.Beneficiary = senderUsername;
            transaction.BankID = bankID;
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.SenderBankName = senderBankName;
            transaction.BeneficiaryBankName = senderBankName;
            transaction.Amount = amount;

            if (transationType == TransactionTypes.Deposit)
            {
                transaction.TransactionType = TransactionTypes.Deposit;
            }
            else if (transationType == TransactionTypes.Withdrawl)
            {
                transaction.TransactionType = TransactionTypes.Withdrawl;
            }
        }

        public static void SetupNewTransaction(Transaction transaction, string senderUsername, string beneficiaryUsername, double amount, string bankID, string accountID, string senderBankName, string beneficiaryBankName, TransactionTypes transationType)
        {
            transaction.Sender = senderUsername;
            transaction.Beneficiary = beneficiaryUsername;
            transaction.BankID = bankID;
            transaction.SenderBankName = senderBankName;
            transaction.BeneficiaryBankName = beneficiaryBankName;
            transaction.ID = "TXN" + bankID + accountID + DateTime.Now.ToString("ddMMyyyy");
            transaction.TransactionDate = DateTime.Now.ToString("f");
            transaction.Amount = amount;

            if (transationType == TransactionTypes.TransferDebit)
            {
                transaction.TransactionType = TransactionTypes.TransferDebit;

            }
            else if (transationType == TransactionTypes.TransferCredit)
            {
                transaction.TransactionType = TransactionTypes.TransferCredit;
            }
        }
        public static void Deposit(Bank bank, Customer customer)
        {
            double amount;
            Console.WriteLine("Enter Amount to Deposit :");
            amount = Utilities.GetAmount();
            Transaction newTransaction = new Transaction();
            SetupNewTransaction(newTransaction, customer.UserName, amount, bank.ID, customer.AccountID, bank.Name, TransactionTypes.Deposit);

            BankService.DepositAmount(customer,newTransaction);

            Console.WriteLine("Amount Deposited Successfully\n" +
                "Total Closing Amount : " + BankService.GetTotalAmount(customer) + " \n" +
                "\n Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Withdraw(Bank bank, Customer customer)
        {
            double amount;
            Console.WriteLine("Enter Amount to Withdraw : ");
            amount = Utilities.GetAmount();
            Transaction newTransaction = new Transaction();
            SetupNewTransaction(newTransaction, customer.UserName, amount, bank.ID, customer.AccountID, bank.Name, TransactionTypes.Withdrawl);

            if ( BankService.IfSufficientFundsAvailable(customer,amount) )
            {
                BankService.WithdrawAmount(customer, newTransaction);

                Console.WriteLine("Amount Withdrawed Successfully\n" +
                "Total Closing Amount : " + BankService.GetTotalAmount(customer) + " \n" +
                "\n Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + BankService.GetTotalAmount(customer) + " \n" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

        }

        public static void TransferFundsMenu(Bank bank, Customer customer)
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
                    SameBankTransfer(bank, customer, FundTransferOptions.SameBankRTGS);
                    CustomerMenu(bank, customer);
                    break;

                case FundTransferOptions.SameBankIMPS:
                    Console.Clear();
                    SameBankTransfer(bank, customer, FundTransferOptions.SameBankIMPS);
                    CustomerMenu(bank, customer);
                    break;

                case FundTransferOptions.OtherBankRTGS:
                    Console.Clear();
                    OtherBankTransfer(bank, customer, FundTransferOptions.OtherBankRTGS);
                    CustomerMenu(bank, customer);
                    break;

                case FundTransferOptions.OtherBankIMPS:
                    Console.Clear();
                    OtherBankTransfer(bank, customer, FundTransferOptions.OtherBankIMPS);
                    CustomerMenu(bank, customer);
                    break;

                default:
                    Console.WriteLine("Invlaid Selection");
                    TransferFundsMenu(bank, customer);
                    break;

            }
        }

        public static void SameBankTransfer(Bank bank, Customer customer, FundTransferOptions bankOption)
        {
            string beneficiaryUsername;
            double amount;
            Console.WriteLine("Enter Benificary's Username : ");
            while( !BankService.IFCustomerExists(bank,beneficiaryUsername = Utilities.GetUserInput()) )
            {
                Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    Console.Clear();
                    return;
                }

            }

            Customer beneficiary = BankService.GetCustomer(bank, beneficiaryUsername);

            Console.WriteLine("Enter Amount to transfer :");
            amount = Utilities.GetAmount();

            amount = BankService.GetTrasferAmount(bank, bankOption, amount);

            if (BankService.IfSufficientFundsAvailable(customer, amount))
            {
                Transaction newDebitTransaction = new Transaction();
                SetupNewTransaction(newDebitTransaction, customer.UserName, beneficiary.UserName, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferDebit);

                Transaction newCreditTransaction = new Transaction();
                SetupNewTransaction(newDebitTransaction, customer.UserName, beneficiary.UserName, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferCredit);

                BankService.TransferFunds(customer, beneficiary, newCreditTransaction, newDebitTransaction);

                Console.WriteLine("Amount Successfull Transfered \n" +
                    "Total closing amount " + BankService.GetTotalAmount(customer) +
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
                CustomerMenu(bank, customer);
            }
        }

        public static void OtherBankTransfer(Bank bank, Customer customer, FundTransferOptions bankOption)
        {
            Console.WriteLine("Enter Bank Name :");
            string otherBankName = Utilities.GetUserInput();
            Bank otherBank;
            if (IfBankExists(BankList, otherBankName))
            {
                otherBank = GetBank(BankList, otherBankName);
                string beneficiaryUsername;
                double amount;
                Console.WriteLine("Enter Benificary's Username : ");
                while (!BankService.IFCustomerExists(otherBank, beneficiaryUsername = Utilities.GetUserInput()) )
                {
                    Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        Console.Clear();
                        return;
                    }
                }

                Customer beneficiary = BankService.GetCustomer(otherBank, beneficiaryUsername);


                Console.WriteLine("Enter Amount to transfer :");
                amount = Utilities.GetAmount();

                amount = BankService.GetTrasferAmount(bank, bankOption, amount);

                if (BankService.IfSufficientFundsAvailable(customer, amount))
                {
                    Transaction newDebitTransfer = new Transaction();
                    SetupNewTransaction(newDebitTransfer, customer.UserName, beneficiary.UserName, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferDebit);

                    Transaction newCreditTransfer = new Transaction();
                    SetupNewTransaction(newDebitTransfer, customer.UserName, beneficiary.UserName, amount, bank.ID, customer.AccountID, customer.BankName, beneficiary.BankName, TransactionTypes.TransferCredit);


                    BankService.TransferFunds(customer, beneficiary, newDebitTransfer, newCreditTransfer);

                    Console.WriteLine("Amount Successfull Transfered \n" +
                        "Total closing amount " + BankService.GetTotalAmount(customer) +
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
            transactionID = Utilities.GetUserInput();

            if (BankService.IFTransactionExists(bank,transactionID))
            {
                if ( BankService.GetSenderUsername(bank,transactionID).Equals(BankService.GetBeneficiaryUsername(bank, transactionID)) )
                {
                    Customer customer = BankService.GetCustomer(bank, BankService.GetSenderUsername(bank, transactionID));
                    Transaction transaction = BankService.GetTransaction(customer, transactionID);

                    BankService.RevertTransaction(customer, transaction);


                }
                else if( BankService.GetSenderBankName(bank,transactionID).Equals(BankService.GetBeneficiaryBankName(bank,transactionID)) )
                {
                    Customer customer = BankService.GetCustomer(bank, BankService.GetSenderUsername(bank, transactionID));
                    Customer beneficiary = BankService.GetCustomer(bank, BankService.GetBeneficiaryUsername(bank, transactionID));

                    Transaction debitTransaction = BankService.GetTransaction(customer, transactionID);
                    Transaction creditTransaction = BankService.GetTransaction(beneficiary, transactionID);

                    BankService.RevertTransaction(customer, beneficiary, debitTransaction, creditTransaction);

                }
                else
                {
                    Customer customer = BankService.GetCustomer(bank, BankService.GetSenderUsername(bank, transactionID));
                    Transaction debitTransaction = BankService.GetTransaction(customer, transactionID);

                    Bank beneficiaryBank = GetBank(BankList, debitTransaction.BeneficiaryBankName);
                    Customer beneficiary = BankService.GetCustomer(beneficiaryBank, debitTransaction.BeneficiaryBankName);
                    Transaction creditTransaction = BankService.GetTransaction(beneficiary, transactionID);

                    BankService.RevertTransaction(customer, beneficiary, debitTransaction, creditTransaction);
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
