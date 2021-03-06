﻿namespace BillsPaymentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SWIFT { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
