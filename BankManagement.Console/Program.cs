using System;

namespace InternshipTaskBankManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Master master = new Master();
            StartMenu(master);
        }

        public static void StartMenu(Master master)
        {
            Console.WriteLine("Welcome to the Bank Management System\n " +
                "\n Select and option from the menu : \n" +
                "---------------------------------------------------------\n" +
                "1. Setup a new Bank\n" +
                "2. Enter a Existing bank");
            int integerInput;
            FirstMenu menuOption;
            if (int.TryParse(Console.ReadLine(), out integerInput))
            {
                menuOption = (FirstMenu)integerInput;

                switch (menuOption)
                {
                    case FirstMenu.SetupNewBank:
                        Console.Clear();
                        Console.WriteLine("Enter Bank Name :");
                        string bankName = Console.ReadLine();
                        if (!master.IfBankExists(bankName))
                        {
                            master.AddBank(bankName);
                            Console.Clear();
                            Console.WriteLine("Enter password for Admin Account");
                            BankServices bankServices = new BankServices(master.GetBank(bankName));
                            bankServices.AddUser(new BankStaff("Admin", "Admin", Console.ReadLine()));
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Bank already exists\n" +
                                "Press any key to continue...");
                            Console.ReadKey();
                        }
                        StartMenu(master);
                        break;

                    case FirstMenu.OpenExistingBank:
                        Console.Clear();
                        Console.WriteLine("Enter Bank Name: ");
                        bankName = Console.ReadLine();
                        if (master.IfBankExists(bankName))
                        {
                            BankMenu(new BankServices(master.GetBank(bankName)));
                        }
                        else
                        {
                            Console.WriteLine("Bank does not exists");
                            StartMenu(master);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        StartMenu(master);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid Selection, only Numbers are accepted \n" +
                            "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                StartMenu(master);
            }
        }

        public static void BankMenu(BankServices bankServices)
        {
            Console.Clear();
            Console.WriteLine("Welcome to " + bankServices.BankName + " Bank :\n" +
                "-------------------------------------------------------\n" +
                "1. Login as BankStaff\n" +
                "2. Login as Customer");
            int integerInput;
            SecondMenu menuOption;
            if (int.TryParse(Console.ReadLine(), out integerInput))
            {
                menuOption = (SecondMenu)integerInput;

                switch (menuOption)
                {
                    case SecondMenu.LoginBankStaff:
                        Console.Clear();
                        Console.WriteLine("Enter Username : ");
                        string username = Console.ReadLine();
                        if (bankServices.IFUserExists(username))
                        {
                            Console.WriteLine("Enter Password : ");
                            BankStaff bankStaff = bankServices.GetBankStaff(username);
                            while (!bankStaff.Password.Equals(Console.ReadLine()))
                            {
                                Console.WriteLine("Incorrect Password Entered, Enter again : ");
                            }
                            Console.Clear();
                            StaffMenu(bankStaff, bankServices);
                        }
                        else
                        {
                            Console.WriteLine("User does not exists\n" +
                                "Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            BankMenu(bankServices);
                        }
                        break;

                    case SecondMenu.LoginCustomer:
                        Console.Clear();
                        Console.WriteLine("Enter Username : ");
                        username = Console.ReadLine();
                        if (bankServices.IFUserExists(username))
                        {
                            Console.WriteLine("Enter Password : ");
                            Customer customer = bankServices.GetCustomer(username);
                            while (!customer.Password.Equals(Console.ReadLine()))
                            {
                                Console.WriteLine("Incorrect Password Entered, Enter again : ");
                            }
                            Console.Clear();
                            CustomerMenu(customer, bankServices);
                        }
                        else
                        {
                            Console.WriteLine("User does not exists\n" +
                                "Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            BankMenu(bankServices);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Selection\n");
                        BankMenu(bankServices);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid Selection");
                BankMenu(bankServices);

            }
        }


        public static void StaffMenu(BankStaff bankStaff, BankServices bankServices)
        {
            Console.WriteLine("Welcome " + bankStaff.Name + " :\n" +
                "----------------------------------------------------" +
                "1. Create new Customer Account\n" +
                "2. Update Customer Account\n" +
                "3. Delete Customer Account\n" +
                "4. Add New Currency\n" +
                "5. Modify Service Charges for same bank\n" +
                "6. Modify Service Charges for different bank\n" +
                "7. View Transaction History\n" +
                "8. Revert a Transaction\n" +
                "9. Logout");

            int integerInput;
            BankStaffMenu menuOption;
            if (int.TryParse(Console.ReadLine(), out integerInput))
            {
                menuOption = (BankStaffMenu)integerInput;

                switch (menuOption)
                {
                    case BankStaffMenu.CreateNewCustomerAccount:
                        Console.Clear();
                        CreateNewCustomerAccount(bankServices);
                        StaffMenu(bankStaff, bankServices);
                        break;

                    case BankStaffMenu.UpdateCustomerAccount:
                        Console.Clear();
                        UpdateCustomerAccount(bankServices);
                        StaffMenu(bankStaff, bankServices);
                        break;

                    case BankStaffMenu.DeleteCustomerAccount:
                        Console.Clear();
                        DeleteCustomerAccount(bankServices);
                        StaffMenu(bankStaff,bankServices);
                        break;

                    case BankStaffMenu.AddNewCurrency:
                        Console.Clear();
                        AddNewCurrency(bankServices);
                        StaffMenu(bankStaff, bankServices);
                        break;

                    case BankStaffMenu.ModifyServiceChargeSameBank:
                        Console.Clear();
                        ModifyServiceChargeSameBank(bankServices);
                        StaffMenu(bankStaff, bankServices);
                        break;

                    case BankStaffMenu.ModifyServiceChargeOtherBank:
                        Console.Clear();
                        ModifyServiceChargeOtherBank(bankServices);
                        StaffMenu(bankStaff, bankServices);
                        break;
                        
                    case BankStaffMenu.ViewTransactionHistory:
                        Console.Clear();
                        ViewTransactionHistory(bankServices);
                        break;

                    case BankStaffMenu.RevertTransaction:
                        break;

                    case BankStaffMenu.Logout:
                        BankMenu(bankServices);
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        StaffMenu(bankStaff, bankServices);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid Selection");
                StaffMenu(bankStaff, bankServices);

            }

        }

        public static void CustomerMenu(Customer customer, BankServices bankServices)
        {
            Console.WriteLine("Welcome " + customer.Name + " :\n" +
                "----------------------------------------------------" +
                "1. Deposit Amount\n" +
                "2. Withdraw Amount\n" +
                "3. Transfer funds\n" +
                "4. View Transaction History\n" +
                "5. Logout");

            int integerInput;
            CustomerMenuOptions menuOption;
            if (int.TryParse(Console.ReadLine(), out integerInput))
            {
                menuOption = (CustomerMenuOptions)integerInput;

                switch (menuOption)
                {
                    case CustomerMenuOptions.Deposit:
                        Console.Clear();
                        Deposit(customer, bankServices);
                        CustomerMenu(customer,bankServices);
                        break;

                    case CustomerMenuOptions.Withdraw:
                        Console.Clear();
                        Withdraw(customer, bankServices);
                        CustomerMenu(customer, bankServices);
                        break;

                    default:
                        Console.WriteLine("Invalid Selection");
                        CustomerMenu(customer,bankServices);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid Selection");
                CustomerMenu(customer, bankServices);

            }


        }


        public static void CreateNewCustomerAccount(BankServices bankServices)
        {
            string name, username;
            Console.WriteLine("Enter Name :");
            name = Console.ReadLine();
            Console.WriteLine("Enter Username :");
            while (bankServices.IFUserExists(username = Console.ReadLine()))
            {
                Console.WriteLine("User already exist enter a new Username : ");
            }

            Console.WriteLine("Enter Password : ");
            string password = Console.ReadLine();
            bankServices.AddUser(new Customer(name, username, password));
            Console.WriteLine("Customer Successfull added\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void UpdateCustomerAccount(BankServices bankServices)
        {
            string username;
            Console.WriteLine("Enter Username :");
            while ( !bankServices.IFUserExists(username = Console.ReadLine()))
            {
                Console.WriteLine("User does not exists, Enter 0 to Exit or Enter a valid Username : ");
                if (username.Equals("0"))
                {
                    Console.Clear();
                    return;
                }
            }

            
            Console.WriteLine("Enter New Password : ");
            bankServices.GetCustomer(username).Password = Console.ReadLine();
            Console.WriteLine("Password Updated Successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void DeleteCustomerAccount(BankServices bankServices)
        {
            string username;
            Console.WriteLine("Enter Username :");
           while ( !bankServices.IFUserExists(username = Console.ReadLine()))
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
            while (!(confirmatiom = Console.ReadLine()).Equals("Y"))
            {
                Console.WriteLine("Enter Y or N");
                if(confirmatiom.Equals("N"))
                {
                    Console.Clear();
                    return;
                }

            }

            bankServices.RemoveUser(username);
            Console.WriteLine("Customer Successfully Removed\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void AddNewCurrency(BankServices bankServices)
        {
            string currencyCode, currencyName;
            double exchangeRate;
            Console.WriteLine("Enter Currency Code :");
            while ( bankServices.IFCurrencyExists(currencyCode = Console.ReadLine()) )
            {
                Console.WriteLine("Currency already Exists, Enter a new Currency Code :");
            }

            Console.WriteLine("Enter Currency Name : ");
            currencyName = Console.ReadLine();
            Console.WriteLine("Enter Exchange Rate : ");
            while(!double.TryParse(Console.ReadLine(), out exchangeRate))
            {
                Console.WriteLine("Only numbers are acccepted, Enter valid Rate again :");
            }

            bankServices.AddCurrency(new Currency(currencyName, currencyCode, exchangeRate));
            Console.Write("Currency succesfully added\n" +
                "Press any button to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void ModifyServiceChargeSameBank(BankServices bankServices)
        {
            double newRate;
            Console.WriteLine("Enter Rate for Same Bank RTGS :");
            while(!double.TryParse(Console.ReadLine(), out newRate))
            {
                Console.WriteLine("Enter valid Rate :");
            }
            bankServices.SetSameBankRate(newRate, ServiceCharges.RTGS);

            Console.WriteLine("Enter Rate for Same Bank IMPS :");
            while (!double.TryParse(Console.ReadLine(), out newRate))
            {
                Console.WriteLine("Enter valid Rate :");
            }
            bankServices.SetSameBankRate(newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for same bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void ModifyServiceChargeOtherBank(BankServices bankServices)
        {
            double newRate;
            Console.WriteLine("Enter Rate for Other Bank RTGS :");
            while (!double.TryParse(Console.ReadLine(), out newRate))
            {
                Console.WriteLine("Enter valid Rate :");
            }
            bankServices.SetOtherBankRate(newRate, ServiceCharges.RTGS);

            Console.WriteLine("Enter Rate for Other Bank IMPS :");
            while (!double.TryParse(Console.ReadLine(), out newRate))
            {
                Console.WriteLine("Enter valid Rate :");
            }
            bankServices.SetOtherBankRate(newRate, ServiceCharges.IMPS);

            Console.WriteLine("Rates for other bank transfers updated successfully\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ViewTransactionHistory(BankServices bankServices)
        {
            string username;
            Console.WriteLine("Enter Customer Username :");
            while( !bankServices.IFUserExists(username = Console.ReadLine()))
            {
                Console.WriteLine("User does not exists, Enter valid Username :");
            }

        }

        public static void Deposit(Customer customer, BankServices bankServices)
        {
            double amount;
            Console.WriteLine("Enter Amount to Deposit :");
            while ( !double.TryParse(Console.ReadLine(), out amount) )
            {
                Console.WriteLine("Only Numbers Accepted, Enter Amount again :");
            }

            bankServices.DepositAmount(customer,amount);
            Console.WriteLine("Amount Deposited Successfully\n" +
                "Total Closing Amount : " + bankServices.GetTotalAmount(customer) + " \n" +
                "\n Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Withdraw(Customer customer, BankServices bankServices)
        {
            double amount;
            Console.WriteLine("Enter Amount to Withdraw : ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Only Numbers Accepted, Enter Amount again :");
            }

            if( bankServices.IfSufficientFundsAvailable(customer,amount) )
            {
                bankServices.WithdrawAmount(customer, amount);
                Console.WriteLine("Amount Withdrawed Successfully\n" +
                "Total Closing Amount : " + bankServices.GetTotalAmount(customer) + " \n" +
                "\n Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Sufficent Funds not available\n" +
                    "Total Closing Amount : " + bankServices.GetTotalAmount(customer) + " \n" +
                    "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
