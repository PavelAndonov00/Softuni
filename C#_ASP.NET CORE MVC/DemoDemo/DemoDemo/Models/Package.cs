using DemoDemo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDemo.Models
{
    public class Package
    {
        public Package()
        {
            this.Receipts = new HashSet<Receipt>();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
