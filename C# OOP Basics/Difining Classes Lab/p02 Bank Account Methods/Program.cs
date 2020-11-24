using System;

namespace BankAccount
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var acc = new BankAccount();

            acc.id = 1;
            acc.Deposit(22);
            acc.Withdraw(12);

            Console.WriteLine(acc.ToString());
        }
    }
}
