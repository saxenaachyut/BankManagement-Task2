using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public static class Utilities
    {
        public static int GetIntInput()
        {
            int integerInput;
            while (!int.TryParse(Console.ReadLine(), out integerInput))
            {
                Console.WriteLine("Enter Valid option :");
            }

            return integerInput;

        }

        public static double GetDoubleInput(string text, string textIfFail)
        {
            double exchangeRate;
            Console.WriteLine(text);
            while (!double.TryParse(Console.ReadLine(), out exchangeRate))
            {
                Console.WriteLine(textIfFail);
            }

            return exchangeRate;
        }

        public static string GetStringInput(string text)
        {
            Console.WriteLine(text);
            string userInput = Console.ReadLine();

            return userInput;
        }

        public static void DisplayMessage(string text)
        {
            Console.WriteLine(text + "\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void DisplayBankList(List<Bank> list)
        {
            int i = 1;
            foreach(Bank bank in list)
            {
                Console.WriteLine((i++) + ". " + bank.Name + "\n");
            }
        }

    }
}

