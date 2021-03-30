﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionUId { get; set; }
       
        public string SrcAccountNumber { get; set; }

        public int AccountHolderId { get; set; }
        public AccountHolder SrcAccountHolder { get; set; }
        
        public string DestAccountNumber { get; set; }       
        
        public TransactionType Type { get; set; }
        public string SrcBankID { get; set; }

        public string DestBankID { get; set; }
        
        public double Amount { get; set; }
        
        public string CreatedOn { get; set; }
        
        public string CreatedBy { get; set; }

        public bool IsReverted { get; set; }
    }
}
