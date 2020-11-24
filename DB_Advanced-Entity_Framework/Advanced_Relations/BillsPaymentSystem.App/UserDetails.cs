namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UserDetails
    {
        public static void PrintDetails(BillsPaymentSystemContext context, int userId)
        {
            if (context.Users.Any(x => x.UserId == userId))
            {
                var user = context
                    .Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = u.FirstName + " " + u.LastName,
                        BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentType.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .ToList(),
                        CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentType.CreditCard)
                        .Select(pm => pm.CreditCard)
                        .ToList()
                    })
                    .SingleOrDefault();

                Console.WriteLine($"User: {user.Name}");

                if (user.BankAccounts.Count > 0)
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (var bankAcc in user.BankAccounts)
                    {
                        Console.WriteLine($"-- ID: {bankAcc.BankAccountId}");
                        Console.WriteLine($"--- Balance: {bankAcc.Balance:f2}");
                        Console.WriteLine($"--- Bank: {bankAcc.BankName}");
                        Console.WriteLine($"--- SWIFT: {bankAcc.SWIFT}");
                    }
                }
                else
                {
                    Console.WriteLine("User doesn't have any bank accounts!");
                }

                if (user.CreditCards.Count > 0)
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (var creditCard in user.CreditCards)
                    {
                        Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                        Console.WriteLine($"--- Limit: {creditCard.Limit:f2}");
                        Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                        Console.WriteLine($"--- Limit Left: {creditCard.LimitLeft:f2}");
                        Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate}");
                    }
                }
                else
                {
                    Console.WriteLine("User doesn't have any credit cards!");
                }
            }
            else
            {
                Console.WriteLine($"User with id {userId} not found!");
            }
        }
    }
}
