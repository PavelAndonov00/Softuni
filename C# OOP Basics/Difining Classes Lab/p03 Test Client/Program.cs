using System;
using System.Collections.Generic;
using System.Linq;

namespace TestClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var accounts = new Dictionary<int, BankAccount>();

            var input = Console.ReadLine();
            while (input != "End")
            {
                var tokens = input
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var type = tokens[0];
                var id = int.Parse(tokens[1]);
                switch (type)
                {
                    case "Create":
                        Create(id, accounts);
                        break;
                    case "Deposit":
                        var depositAmount = decimal.Parse(tokens[2]);
                        Deposit(id, depositAmount, accounts);
                        break;
                    case "Withdraw":
                        var withdrawAmount = decimal.Parse(tokens[2]);
                        Withdraw(id, withdrawAmount, accounts);
                        break;
                    case "Print":
                        Print(id, accounts);
                        break;
                }

                input = Console.ReadLine();
            }
        }

        private static void Print(int id, Dictionary<int, BankAccount> accounts)
        {
            if (accounts.ContainsKey(id))
            {
                accounts[id].Print();
            }
            else
            {
                Console.WriteLine("Account does not exist");
            }
        }

        private static void Withdraw(int id, decimal withdrawAmount, Dictionary<int, BankAccount> accounts)
        {
            if (accounts.ContainsKey(id))
            {
                accounts[id].Withdraw(withdrawAmount);
            }
            else
            {
                Console.WriteLine("Account does not exist");
            }
        }

        private static void Deposit(int id, decimal depositAmount, Dictionary<int, BankAccount> accounts)
        {
            if (accounts.ContainsKey(id))
            {
                accounts[id].Deposit(depositAmount);
            }
            else
            {
                Console.WriteLine("Account does not exist");
            }
        }

        private static void Create(int id, Dictionary<int, BankAccount> accounts)
        {
            if (accounts.ContainsKey(id))
            {
                Console.WriteLine("Account already exist!");
            }
            else
            {
                accounts[id] = new BankAccount();
                accounts[id].Id = id;
            }
        }
    }
}
