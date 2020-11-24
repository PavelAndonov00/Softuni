using System;

namespace BankAccount
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var acc = new BankAccount();

            acc.id = 1;
            acc.balance = 15;

            Console.WriteLine($"Account {acc.id}, balance {acc.balance}");
        }
    }
}
