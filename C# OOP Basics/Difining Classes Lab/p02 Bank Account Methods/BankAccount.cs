using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    class BankAccount
    {
        public int id { get; set; }
        public decimal balance { get; set; }

        public void Deposit (decimal amount)
        {
            this.balance += amount;
        }

        public void Withdraw (decimal amount)
        {
            this.balance -= amount;
        }

        public override string ToString()
        {
            return $"Account {this.id}, balance {this.balance}";
        }
    }
}
