namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUsers(context);
            SeedCreditCards(context);
            SeedBankAccounts(context);
            SeedPaymentMethods(context);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            var users = context.Users.ToArray();
            var bankAccounts = context.BankAccounts.ToArray();
            var creditCards = context.CreditCards.ToArray();

            var paymentMethods = new List<PaymentMethod>();
            for (int k = 0; k < 7; k++)
            {
                paymentMethods.Add(new PaymentMethod()
                {
                    User = users[k],
                    BankAccount = bankAccounts[k],
                    Type = PaymentType.BankAccount
                });

                paymentMethods.Add(new PaymentMethod()
                {
                    User = users[k],
                    CreditCard = creditCards[k],
                    Type = PaymentType.CreditCard
                });
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            var accounts = new List<BankAccount>();
            for (int i = 1; i < 8; i++)
            {
                accounts.Add(new BankAccount()
                {
                    Balance = 500 * i,
                    BankName = "Bank " + i,
                    SWIFT = "Swift " + i
                });
            }

            context.BankAccounts.AddRange(accounts);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = new List<CreditCard>();
            decimal limit = 500; decimal moneyOwed = 10;
            for (int i = 0; i < 7; i++)
            {
                creditCards.Add(new CreditCard()
                {
                    Limit = limit,
                    MoneyOwed = moneyOwed,
                    ExpirationDate = DateTime.Now.AddDays((int)(limit + moneyOwed))
                });

                limit += 50;
                moneyOwed += 10;
            }

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            var firstNames = new string[]
                            {
                    "Gosho", "Pesho", "Ivan", "Georgi", "Sasho", "Kiro", "Kristian"
                            };

            var lastNames = new string[]
            {
                    "Goshov", "Peshov", "Ivanov", "Georgiev", "Sashov", "Kirov", "Gucalev"
            };

            var emails = new string[]
            {
                    "Gosho@abv.bg", "Pesho@gmail.com", "Ivan@abv.bg", "Georgi@abv.bg", "Sasho@gmail.com", "Kiro@gmail.com", "Kristian@abv.bg"
            };

            var passwords = new string[]
            {
                    "Gosho123", "Pesho423", "23Ivan23", "1241Geo23rgi3", "55Sasho55", "65Kiro_sd", "Kris3tia123n"
            };

            var users = new List<User>();
            for (int i = 0; i < 7; i++)
            {
                users.Add(new User()
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i],
                });
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
