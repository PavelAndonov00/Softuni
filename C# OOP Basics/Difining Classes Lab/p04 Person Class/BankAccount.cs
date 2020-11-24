using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    class BankAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if(amount <= this.Balance)
            {
                this.Balance -= amount;
            }

            Console.WriteLine("Insufficient balance");
        }

        public void Print()
        {
            Console.WriteLine($"AccountID: {this.Id}, balance: {this.Balance}");
        }
    }
}
