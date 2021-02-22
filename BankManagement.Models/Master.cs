using System;
using System.Collections.Generic;
using System.Text;

namespace InternshipTaskBankManagement
{
    public class Master
    {
        public List<Bank> Banks;

        public Master()
        {
            this.Banks = new List<Bank>();
        }

        public void AddBank(string bankName)
        {
            Bank bank = new Bank(bankName);
            this.Banks.Add(bank);

        }

        public Boolean IfBankExists(string bankName)
        {
            foreach (Bank bank in Banks)
            {
                if (bank.Name == bankName)
                {
                    return true;
                }
            }

            return false;
        }

        public Bank GetBank(string bankName)
        {
            foreach (Bank bank in Banks)
            {
                if (bank.Name == bankName)
                {
                    return bank;
                }
            }

            return null;

        }

    }
}