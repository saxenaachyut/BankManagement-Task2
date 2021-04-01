using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        public string BankId { get; set; }

        public Bank Bank { get; set; }
    }
}
