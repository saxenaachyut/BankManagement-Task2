using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }  
        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BankStaff> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ServiceChargeRates> ServiceCharges { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
