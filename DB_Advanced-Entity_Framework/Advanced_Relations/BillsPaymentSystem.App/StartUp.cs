using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace BillsPaymentSystem.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();

                DbInitializer.Seed(context);

                Console.WriteLine("Enter user id");
                int userId = int.Parse(Console.ReadLine());
                UserDetails.PrintDetails(context, userId);

                var userService = new UserService(context);

                Console.WriteLine("Enter id:");
                userId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter amount:");
                decimal amount = decimal.Parse(Console.ReadLine());

                try
                {
                    userService.PayBills(userId, amount);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        
    }
}
