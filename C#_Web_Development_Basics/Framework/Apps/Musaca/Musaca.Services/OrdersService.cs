using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly MusacaDbContext context;

        public OrdersService(MusacaDbContext context)
        {
            this.context = context;
        }

        public Order CreateOrder(string CashierId)
        {
            var order = new Order
            {
                CashierId = CashierId
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            return order;
        }

        public IEnumerable<OrderProduct> GetAllOrderProductsByCashierId(string CashierId)
        {
            //
            var orderProducts = this.context.Orders
                .Include(o => o.OrdersProducts)
                .ThenInclude(o => o.Product)
                .FirstOrDefault(o => o.CashierId == CashierId).OrdersProducts;

            return orderProducts;
        }
    }
}
