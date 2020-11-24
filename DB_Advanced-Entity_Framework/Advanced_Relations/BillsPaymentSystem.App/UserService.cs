namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UserService
    {
        private BillsPaymentSystemContext context;

        public UserService(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public void PayBills(int userId, decimal amount)
        {
            this.Withdraw(userId, amount);
        }

        public void Deposit(int userId, decimal amount)
        {
            if (context.Users.Any(u => u.UserId == userId))
            {
                context
                    .PaymentMethods
                    .Where(pm => pm.UserId == userId && pm.Type == PaymentType.BankAccount)
                    .Select(pm => pm.BankAccount)
                    .SingleOrDefault()
                    .Balance += amount;

                context.SaveChanges();
            }
        }

        public void Withdraw(int userId, decimal amount)
        {
            if (context.Users.Any(u => u.UserId == userId))
            {
                var accounts = context
                    .PaymentMethods
                    .Where(pm => pm.UserId == userId && pm.Type == PaymentType.BankAccount)
                    .Select(pm => pm.BankAccount)
                    .OrderBy(pm => pm.BankAccountId)
                    .ToList();

                var cards = context
                    .PaymentMethods
                    .Where(pm => pm.UserId == userId && pm.Type == PaymentType.CreditCard)
                    .Select(pm => pm.CreditCard)
                    .OrderBy(pm => pm.CreditCardId)
                    .ToList();

                if (accounts.Sum(a => a.Balance) + cards.Sum(c => c.LimitLeft) < amount)
                {
                    throw new Exception("Insufficient funds!");
                }

                foreach (var account in accounts)
                {
                    if (account.Balance <= amount)
                    {
                        amount -= account.Balance;
                        account.Balance = 0;
                    }
                    else
                    {
                        account.Balance -= amount;
                        amount = 0;
                        context.SaveChanges();
                        return;
                    }
                }

                foreach (var card in cards)
                {
                    if (card.LimitLeft <= amount)
                    {
                        amount -= card.LimitLeft;
                        card.MoneyOwed += card.LimitLeft;
                    }
                    else
                    {
                        card.MoneyOwed += amount;
                        amount = 0;
                        break;
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
