using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankStaff : User
    {
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        
        public string EmployeeID { get; set; }

    }
}
