using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public static class Utilities
    {
        public static MainMenuOptions GetStartMenuOption()
        {
            int integerInput;
            while ( !int.TryParse(Console.ReadLine(), out integerInput) )
            {
                Console.WriteLine("Enter Valid option :");
            }

            return (MainMenuOptions)integerInput;
        }

        public static BankMenuOptions GetBankMenuOption()
        {
            int integerInput;
            while (!int.TryParse(Console.ReadLine(), out integerInput))
            {
                Console.WriteLine("Enter Valid option :");
            }

            return (BankMenuOptions)integerInput;
        }

        public static StaffMenuOptions GetStaffMenuOption()
        {
            int integerInput;
            while (!int.TryParse(Console.ReadLine(), out integerInput))
            {
                Console.WriteLine("Enter Valid option :");
            }

            return (StaffMenuOptions)integerInput;
        }

        public static CustomerMenuOptions GetCustomerMenuOption()
        {
            int integerInput;
            while (!int.TryParse(Console.ReadLine(), out integerInput))
            {
                Console.WriteLine("Enter Valid option :");
            }

            return (CustomerMenuOptions)integerInput;
        }

        public static FundTransferOptions GetTransferFundsMenuOption()
        {
            int integerInput;
            while (!int.TryParse(Console.ReadLine(), out integerInput))
            {
                Console.WriteLine("Enter Valid option :");
            }

            return (FundTransferOptions )integerInput;
        }

        public static double GetExchangeRateFromUser()
        {
            double exchangeRate;
            while (!double.TryParse(Console.ReadLine(), out exchangeRate))
            {
                Console.WriteLine("Only numbers are acccepted, Enter valid Rate again :");
            }

            return exchangeRate;
        }

        public static double GetAmount()
        {
            double amount;
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Only Numbers Accepted, Enter Amount again :");
            }

            return amount;
        }

        public static string GetStringInput()
        {
            string userInput = Console.ReadLine();

            return userInput;
        }

    }
}

