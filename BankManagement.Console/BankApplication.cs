using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Bank.Services;

namespace Bank.Console
{
    public class Program
    {
        static async Task Main()
        {
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IBankApplication>();
                await app.MainMenu();
            }

        }
    }
    public class BankApplication : IBankApplication
    {
        public IBankServices BankService { get; set; }
        public IAccountHolderServices AccountHolderService { get; set; }
        public ITransactionServices TransactionService { get; set; }

        public BankApplication(IBankServices bankService, IAccountHolderServices accountHolderService, ITransactionServices transactionService)
        {
            this.BankService = bankService;
            this.AccountHolderService = accountHolderService;
            this.TransactionService = transactionService;
        }


        public async Task MainMenu()
        {
            System.Console.WriteLine("Welcome to the Bank Management System\n " +
                "\n Select an option from the menu : \n" +
                "---------------------------------------------------------\n" +
                "1. Create new Bank\n" +
                "2. Login to Existing Bank\n" +
                "3. Exit");

            MainMenu menuOption = (MainMenu)Utilities.GetIntInput();
            switch (menuOption)
            {
                case global::Bank.MainMenu.CreateNewBank:
                    System.Console.Clear();
                    await CreateBank();
                    await MainMenu();
                    break;

                case global::Bank.MainMenu.Login:
                    System.Console.Clear();
                    await Login();
                    await MainMenu();
                    break;

                case global::Bank.MainMenu.Exit:
                    System.Environment.Exit(1);
                    return;

                default:
                    System.Console.WriteLine("Invalid Selection");
                    await MainMenu();
                    break;
            }
        }

        public async Task CreateBank()
        {
            string bankName = bankName = Utilities.GetStringInput("Enter Bank Name :");

            while (BankService.IsBankExists(bankName))
            {
                bankName = Utilities.GetStringInput("Bank Already Exists, Enter a new Bank Name :");
            }

            if (await BankService.AddBank(bankName) != null)
            {
                System.Console.WriteLine("Added bank");

                BankStaff admin = new BankStaff();
                admin.Name = Utilities.GetStringInput("Enter name for Admin account :"); ;
                admin.UserName = Utilities.GetStringInput("Enter Username for Admin account :");
                admin.Email = Utilities.GetStringInput("Enter Email Address for Admin account :");
                admin.Password = Utilities.GetStringInput("Enter password for Admin Account :");
                admin.BankId = BankService.GetBankID(bankName);

               if( await BankService.AddBankStaff(admin) != null)
                {
                    System.Console.WriteLine("Admin successfully added");
                }
               else
                {

                }
            }

            else
            {
                System.Console.WriteLine("bank not added");
                await MainMenu();
            }
        }

        public async Task Login()
        {
            System.Console.WriteLine("Select the Bank to Login to");
            await DisplayBanks();
            int selectBank = Utilities.GetIntInput();
            var bankCount = await BankService.GetBankCount();
            while (bankCount < selectBank)
            {
                System.Console.WriteLine("Select a valid option :");
                selectBank = Utilities.GetIntInput();
            }

            var banks = await BankService.GetBankList();
            Bank selectedBank = banks[selectBank - 1];
            System.Console.Clear();
            await BankLogin(selectedBank);
        }

        public async Task BankLogin(Bank bank)
        {
            System.Console.WriteLine("Welcome to " + bank.Name + " Bank :\n" +
               "-------------------------------------------------------\n");

            string username = Utilities.GetStringInput("Enter Username to Login or Type Exit to go back");
            if (BankService.IsStaffExists(bank.Id, username))
            {
                BankStaff bankStaff = BankService.GetBankStaff(bank.Id, username);
                while (!bankStaff.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                {
                    System.Console.WriteLine("Incorrect Password Entered, Enter again : ");
                }
                System.Console.Clear();
                await StaffMenu(bank, bankStaff);
            }
            else if (AccountHolderService.IsAccountHolderExists(bank.Id, username))
            {
                AccountHolder accountHolder = AccountHolderService.GetAccountHolder(bank.Id, username);
                while (!accountHolder.Password.Equals(Utilities.GetStringInput("Enter Password :")))
                {
                    System.Console.WriteLine("Incorrect Password Entered, Enter again : ");
                }
                System.Console.Clear();
                await AccountHolderMenu(bank, accountHolder);
            }
            else if (username.ToLower().Equals("exit"))
            {
                System.Console.Clear();
                await MainMenu();
            }
            else
            {
                Utilities.DisplayMessage("User does not exists");
                System.Console.Clear();
                await BankLogin(bank);
            }


        }

        public async Task StaffMenu(Bank bank, BankStaff bankStaff)
        {
            System.Console.WriteLine("Welcome " + bankStaff.Name + " :\n" +
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
                    System.Console.Clear();
                    await CreateNewAccountHolder(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateAccountHolder:
                    System.Console.Clear();
                    await UpdateAccountHolder(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.DeleteAccountHolder:
                    System.Console.Clear();
                    await DeleteCustomerAccount(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.AddNewCurrency:
                    System.Console.Clear();
                    await AddNewCurrency(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateServiceChargeSameBank:
                    System.Console.Clear();
                    await UpdateServiceChargeSameBank(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.UpdateServiceChargeOtherBank:
                    System.Console.Clear();
                    await UpdateServiceChargeOtherBank(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.TransactionHistory:
                    System.Console.Clear();
                    await TransactionHistory(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.RevertTransaction:
                    System.Console.Clear();
                    await RevertTransaction(bank);
                    await StaffMenu(bank, bankStaff);
                    break;

                case global::Bank.StaffMenu.Logout:
                    System.Console.Clear();
                    await BankLogin(bank);
                    break;
                default:
                    System.Console.WriteLine("Invalid Selection");
                    await StaffMenu(bank, bankStaff);
                    break;
            }

        }

        public async Task AccountHolderMenu(Bank bank, AccountHolder accountHolder)
        {
            System.Console.WriteLine("Welcome " + accountHolder.Name + " :\n" +
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
                    System.Console.Clear();
                    await Deposit(bank, accountHolder);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Withdraw:
                    System.Console.Clear();
                    await Withdraw(bank, accountHolder);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Transfer:
                    System.Console.Clear();
                    await TransferFundsMenu(bank, accountHolder);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.TransactionHistory:
                    System.Console.Clear();
                    await TransactionHistory(accountHolder);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case global::Bank.AccountHolderMenu.Logout:
                    System.Console.Clear();
                    await BankLogin(bank);
                    break;

                default:
                    System.Console.WriteLine("Invalid Selection");
                    await AccountHolderMenu(bank, accountHolder);
                    break;
            }
        }

        public async Task CreateNewAccountHolder(Bank bank)
        {
            string name, username;
            name = Utilities.GetStringInput("Enter Name");
            while (AccountHolderService.IsAccountHolderExists(bank.Id, username = Utilities.GetStringInput("Enter Username :")))
            {
                System.Console.WriteLine("User already exist enter a new Username or 0 to exit : ");
                if (username.Equals("0"))
                {
                    System.Console.Clear();
                    return;
                }
            }
            AccountHolder accountHolder = new AccountHolder();
            accountHolder.Name = name;
            accountHolder.UserName = username;
            accountHolder.Password = Utilities.GetStringInput("Enter Password :");
            accountHolder.PhoneNumber = Utilities.GetStringInput("Enter User Phone Number :");
            accountHolder.Email = Utilities.GetStringInput("Enter User Email Address");

            if (await AccountHolderService.AddAccountHolder(bank.Id, accountHolder) != null)
            {
                System.Console.WriteLine("Account Holder added successfully");
            }
            else
                System.Console.WriteLine("Account Holder not added successfully");
            System.Console.Clear();
        }

        public async Task UpdateAccountHolder(Bank bank)
        {
            string username;
            while (!AccountHolderService.IsAccountHolderExists(bank.Id, username = Utilities.GetStringInput("Enter Username :")))
            {
                System.Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    System.Console.Clear();
                    return;
                }
            }

            string password = Utilities.GetStringInput("Enter New Password :");
            await AccountHolderService.ChangePassword(bank.Id, username, password);

            Utilities.DisplayMessage("Password Updated Successfully");

        }

        public async Task DeleteCustomerAccount(Bank bank)
        {
            string username;
            while (!AccountHolderService.IsAccountHolderExists(bank.Id, username = Utilities.GetStringInput("Enter Username :")))
            {
                System.Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    System.Console.Clear();
                    return;
                }
            }

            char confirmatiom;
            while (!(confirmatiom = Convert.ToChar(Utilities.GetStringInput("Are you sure you want to delete Customer: " + username + ". Enter Y to continue or N to cancel"))).Equals('Y'))
            {
                System.Console.WriteLine("Enter Y or N");
                if (confirmatiom.Equals("N"))
                {
                    System.Console.Clear();
                    return;
                }

            }

            await AccountHolderService.RemoveAccountHolder(bank.Id, username);
            Utilities.DisplayMessage("Customer Successfully Removed");
        }

        public async Task AddNewCurrency(Bank bank)
        {
            string currencyCode;
            Currency newCurrency = new Currency();
            while (BankService.IsCurrencyExists(bank.Id, currencyCode = Utilities.GetStringInput("Enter Currency Code :")))
            {
                System.Console.WriteLine("Currency already Exists, Enter a new Currency Code or 0 to exit :");
                if (currencyCode.Equals("0"))
                {
                    System.Console.Clear();
                    return;
                }
            }

            newCurrency.BankId = bank.Id;
            newCurrency.Name = Utilities.GetStringInput("Enter Currency Name :");
            newCurrency.ExcahngeRate = Utilities.GetDoubleInput("Enter Exchange Rate", "Only numbers are acccepted, Enter valid Rate again");
            await BankService.AddCurrency(newCurrency);

            Utilities.DisplayMessage("Currency succesfully added");
        }

        public async Task UpdateServiceChargeSameBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            await BankService.SetSameBankRate(bank.Id, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Same Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            await BankService.SetSameBankRate(bank.Id, newRate, ServiceCharges.IMPS);

            Utilities.DisplayMessage("Rates for same bank transfers updated successfully");
        }

        public async Task UpdateServiceChargeOtherBank(Bank bank)
        {
            double newRate;
            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank RTGS", "Only numbers are acccepted, Enter valid Rate again");
            await BankService.SetOtherBankRate(bank.Id, newRate, ServiceCharges.RTGS);

            newRate = Utilities.GetDoubleInput("Enter Rate for Other Bank IMPS", "Only numbers are acccepted, Enter valid Rate again");
            await BankService.SetOtherBankRate(bank.Id, newRate, ServiceCharges.IMPS);

            Utilities.DisplayMessage("Rates for other bank transfers updated successfully");
        }

        public async Task TransactionHistory(Bank bank)
        {
            string accountHolderUsername;
            while (!AccountHolderService.IsAccountHolderExists(bank.Id, accountHolderUsername = Utilities.GetStringInput("Enter Customer Username")))
            {
                System.Console.WriteLine("User does not exists, Enter valid Username :");
            }

            var accountHolder = AccountHolderService.GetAccountHolder(bank.Id, accountHolderUsername);
            await TransactionHistory(accountHolder);
        }

        public async Task TransactionHistory(AccountHolder accountHolder)
        {
            var transactions = await AccountHolderService.GetTransactions(accountHolder.Id);

            foreach (Transaction transaction in transactions)
            {
                System.Console.WriteLine("Transaction ID - " + transaction.Id + "\n");
                System.Console.WriteLine("Transaction Date - " + transaction.CreatedOn + "\n");
                System.Console.WriteLine("Transaction Type - " + transaction.Type + "\n");
                System.Console.WriteLine("Source Account Number - " + transaction.SrcAccountNumber + "\n");
                System.Console.WriteLine("Destination Account Number - " + transaction.DestAccountNumber + "\n");
                System.Console.WriteLine("Amount - " + transaction.Amount + "\n");
            }

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
            System.Console.Clear();
        }

        public async Task Deposit(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            amount = Utilities.GetDoubleInput("Enter Amount to Deposit", "Only Numbers Accepted, Enter Amount again");
            Transaction transaction = new Transaction()
            {
                SrcAccountNumber = accountHolder.Id,
                SrcBankID = accountHolder.BankId,
                CreatedOn = DateTime.Now.ToString("f"),
                CreatedBy = accountHolder.Id,
                Id = "TXN" + accountHolder.BankId + accountHolder.Id + DateTime.Now.ToString("ddMMyyyyhhmmss"),
                DestBankID = accountHolder.BankId,
                Amount = amount,
                Type = TransactionType.Credit
            };

            if( await AccountHolderService.DepositAmount(transaction) != null)
            {
                System.Console.WriteLine("Transaction Successfull");
            }
            else
            {
                System.Console.WriteLine("Transaction not Successfull");
            }

            Utilities.DisplayMessage("Total Closing Amount : " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));
        }

        public async Task Withdraw(Bank bank, AccountHolder accountHolder)
        {
            double amount;
            amount = Utilities.GetDoubleInput("Enter Amount to Withdraw", "Only Numbers Accepted, Enter Amount again");
            Transaction transaction = new Transaction()
            {
                SrcAccountNumber = accountHolder.Id,
                SrcBankID = accountHolder.BankId,
                CreatedOn = DateTime.Now.ToString("f"),
                CreatedBy = accountHolder.Id,
                Id = "TXN" + accountHolder.BankId + accountHolder.Id + DateTime.Now.ToString("ddMMyyyyhhmmss"),
                DestBankID = accountHolder.BankId,
                Amount = amount,
                Type = TransactionType.Debit
            };

            if (AccountHolderService.IsSufficientFundsAvailable(accountHolder.Id, amount))
            {
                if (await AccountHolderService.WithdrawAmount(transaction) != null)
                {
                    System.Console.WriteLine("Transaction Successfull");

                }
                else
                {
                    System.Console.WriteLine("Transaction not Successfull");
                }

                Utilities.DisplayMessage("Total Closing Amount : " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));
            }
            else
            {
                Utilities.DisplayMessage("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));
            }

        }

        public async Task TransferFundsMenu(Bank bank, AccountHolder accountHolder)
        {
            System.Console.WriteLine("Bank of the Benificiary :\n" +
                "1. Same bank (RTGS)\n" +
                "2. Same bank (IMPS)\n" +
                "3. Other bank (RTGS)\n" +
                "4. Other bank (IMPS)");

            FundTransferOption bankOption = (FundTransferOption)Utilities.GetIntInput();

            switch (bankOption)
            {
                case FundTransferOption.SameBankRTGS:
                    System.Console.Clear();
                    await SameBankTransfer(bank, accountHolder, FundTransferOption.SameBankRTGS);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.SameBankIMPS:
                    System.Console.Clear();
                    await SameBankTransfer(bank, accountHolder, FundTransferOption.SameBankIMPS);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankRTGS:
                    System.Console.Clear();
                    await OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankRTGS);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                case FundTransferOption.OtherBankIMPS:
                    System.Console.Clear();
                    await OtherBankTransfer(bank, accountHolder, FundTransferOption.OtherBankIMPS);
                    await AccountHolderMenu(bank, accountHolder);
                    break;

                default:
                    System.Console.WriteLine("Invalid Selection");
                    await TransferFundsMenu(bank, accountHolder);
                    break;

            }
        }

        public async Task SameBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption)
        {
            string beneficiaryUsername;
            double amount;
            while (!AccountHolderService.IsAccountHolderExists(bank.Id, beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")))
            {
                System.Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                if (beneficiaryUsername.Equals("0"))
                {
                    System.Console.Clear();
                    return;
                }
            }

            AccountHolder beneficiary = AccountHolderService.GetAccountHolder(bank.Id, beneficiaryUsername);

            amount = Utilities.GetDoubleInput("Enter Amount to transfer", "Invalid input, Enter valid Amount :");

            amount = TransactionService.GetTrasferAmount(bank.Id, bankOption, amount);

            if (AccountHolderService.IsSufficientFundsAvailable(accountHolder.Id, amount))
            {
                Transaction transaction = new Transaction()
                {
                    SrcAccountNumber = accountHolder.Id,
                    DestAccountNumber = beneficiary.Id,
                    SrcBankID = accountHolder.BankId,
                    DestBankID = beneficiary.BankId,
                    Id = "TXN" + accountHolder.BankId + accountHolder.Id + DateTime.Now.ToString("ddMMyyyyhhmmss"),
                    CreatedOn = DateTime.Now.ToString("f"),
                    CreatedBy = accountHolder.Id,
                    Amount = amount,
                    Type = TransactionType.Transfer
                };

                if (await AccountHolderService.TransferFunds(transaction) != null)
                {
                    Utilities.DisplayMessage("Amount Successfull Transfered \n" +
                    "Total closing amount " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));

                }
                else
                {
                    System.Console.WriteLine("Transaction not Successfull");
                }          
            }
            else
            {
                Utilities.DisplayMessage("Insufficent Funds");
            }
        }

        public async Task OtherBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption)
        {
            string otherBankName = Utilities.GetStringInput("Enter Bank Name :");
            Bank otherBank;
            if (BankService.IsBankExists(otherBankName))
            {
                otherBank = await BankService.GetBank(otherBankName);
                string beneficiaryUsername;
                double amount;
                while (!AccountHolderService.IsAccountHolderExists(otherBank.Id, beneficiaryUsername = Utilities.GetStringInput("Enter Benificary's Username :")))
                {
                    System.Console.WriteLine("User does not exists, Enter a valid Username or 0 to exit :");
                    if (beneficiaryUsername.Equals("0"))
                    {
                        System.Console.Clear();
                        return;
                    }
                }

                AccountHolder beneficiary = AccountHolderService.GetAccountHolder(otherBank.Id, beneficiaryUsername);


                amount = Utilities.GetDoubleInput("Enter Amount to transfer", "Invalid input, Enter valid Amount :");

                amount = TransactionService.GetTrasferAmount(bank.Id, bankOption, amount);

                if (AccountHolderService.IsSufficientFundsAvailable(accountHolder.Id, amount))
                {
                    Transaction transaction = new Transaction()
                    {
                        SrcAccountNumber = accountHolder.Id,
                        DestAccountNumber = beneficiary.Id,
                        SrcBankID = accountHolder.BankId,
                        DestBankID = beneficiary.BankId,
                        Id = "TXN" + accountHolder.BankId + accountHolder.Id + DateTime.Now.ToString("ddMMyyyyhhmmss"),
                        CreatedOn = DateTime.Now.ToString("f"),
                        CreatedBy = accountHolder.Id,
                        Amount = amount,
                        Type = TransactionType.Transfer
                    };

                    
                    if (await AccountHolderService.TransferFunds(transaction) != null)
                    {
                        Utilities.DisplayMessage("Amount Successfull Transfered \n" +
                        "Total closing amount " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));

                    }
                    else
                    {
                        System.Console.WriteLine("Transaction not Successfull");
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

        public async Task RevertTransaction(Bank bank)
        {
            string username = Utilities.GetStringInput("Enter Username :");

            if (AccountHolderService.IsAccountHolderExists(bank.Id, username))
            {
                AccountHolder accountHolder = AccountHolderService.GetAccountHolder(bank.Id, username);

                string transactionID = Utilities.GetStringInput("Enter Transaction ID :");
                if (TransactionService.IsTransactionExists(transactionID))
                {
                    if (await TransactionService.RevertTransaction(transactionID) != null)
                    {
                        Utilities.DisplayMessage("Amount Successfull Reverted \n" +
                        "Total closing amount " + await AccountHolderService.GetAccountBalance(bank.Id, accountHolder.Id));

                    }
                    else
                    {
                        System.Console.WriteLine("Transaction not Successfull");
                    }
                }
                else
                {
                    Utilities.DisplayMessage("Transaction Does not exist");
                }
            }
            else
            {
                Utilities.DisplayMessage("AccountHolder does not exist");
            }

        }

        public async Task DisplayBanks()
        {
            var banks = await BankService.GetBankList();
            int i = 1;
            foreach (Bank bank in banks)
            {
                System.Console.WriteLine((i++) + ". " + bank.Name + "\n");
            }
        }
    }
}
