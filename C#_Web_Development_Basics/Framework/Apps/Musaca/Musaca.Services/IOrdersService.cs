using Musaca.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Musaca.Services
{
    public interface IOrdersService
    {
        Order CreateOrder(string CashierId);

        IEnumerable<OrderProduct> GetAllOrderProductsByCashierId(string CashierId);
    }
}
