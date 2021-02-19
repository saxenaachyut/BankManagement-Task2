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
                        Console.WriteLine("Invalid Selection\n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
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
                        string userName = Console.ReadLine();
                        if (bankServices.IFUserExists(userName))
                        {
                            Console.WriteLine("Enter Password : ");
                            BankStaff bankStaff = bankServices.GetBankStaff(userName);
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
                        userName = Console.ReadLine();
                        if (bankServices.IFUserExists(userName))
                        {
                            Console.WriteLine("Enter Password : ");
                            Customer customer = bankServices.GetCustomer(userName);
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
                "1. Create new Customer Account\n" +
                "2. Update Customer Account\n" +
                "3. Delete Customer Account\n" +
                "4. Add New Currency\n" +
                "5. Modify Service Charges for same bank\n" +
                "6. Modify Service Charges for different bank\n" +
                "7. View Transaction History\n" +
                "8. Revert a Transaction" +
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
                        Console.Clear();
                        StaffMenu(bankStaff, bankServices);
                        break;
                    case BankStaffMenu.UpdateCustomerAccount:
                        break;
                    case BankStaffMenu.DeleteCustomerAccount:
                        break;
                    case BankStaffMenu.AddNewCurrency:
                        break;
                    case BankStaffMenu.ModifyServiceChargeSameBank:
                        break;
                    case BankStaffMenu.ModifyServiceChargeOtherBank:
                        break;
                    case BankStaffMenu.ViewTransactionHistory:
                        break;
                    case BankStaffMenu.RevertTransaction:
                        break;
                    case BankStaffMenu.Logout:
                        BankMenu(bankServices);
                        break;
                    default:
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

        }


        public static void CreateNewCustomerAccount(BankServices bankServices)
        {
            string name, userName;
            Console.WriteLine("Enter Name :");
            name = Console.ReadLine();
            Console.WriteLine("Enter Username :");
            while (bankServices.IFUserExists(userName = Console.ReadLine()))
            {
                Console.WriteLine("User already exist enter a new Username : ");
            }

            Console.WriteLine("Enter Password : ");
            string password = Console.ReadLine();
            bankServices.AddUser(new Customer(name, userName, password));
            Console.WriteLine("Customer Successfull added\n" +
                "Press any key to continue...");
            Console.ReadKey();

        }
    }
}
