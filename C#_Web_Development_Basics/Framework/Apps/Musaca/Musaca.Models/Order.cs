using Musaca.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Musaca.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrdersProducts = new HashSet<OrderProduct>();
        }

        [Key]
        public string Id { get;  set; }

        public OrderStatus Status { get; set; } = OrderStatus.Active;

        public DateTime IssuedOn { get; set; }


        [Required]
        public string CashierId { get; set; }

        public User Cashier { get; set; }

        public ICollection<OrderProduct> OrdersProducts { get; set; }
    }
}
