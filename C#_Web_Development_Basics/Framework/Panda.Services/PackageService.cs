using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using Panda.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class PackageService : IPackageService
    {
        private readonly PandaDbContext context;

        public PackageService(PandaDbContext context)
        {
            this.context = context;
        }

        public void CreatePackage(string description, decimal weight, string shippingAddress, string recipientName)
        {
            var recipient = this.context.Users.FirstOrDefault(u => u.Username == recipientName);

            var package = new Package
            {
                Description = description,
                Weight = weight,
                ShippingAddress = shippingAddress,
                RecipientId = recipient.Id
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();
        }

        public IEnumerable<Package> GetAllPackageByCurrentLoggedUserIdAndStatus(string currentLoggedId, string status)
        {
            var packages = this.context.Packages
                .Include(p => p.Recipient)
                .Where(p => p.RecipientId == currentLoggedId && p.Status.ToString() == status).ToList();

            return packages;
        }

        public Package ChangePackageToDeliveredStatusById(string id)
        {
            var package = this.context.Packages
                .Include(p => p.Recipient)
                .FirstOrDefault(p => p.Id == id);
            package.Status = PackageStatus.Delivered;
            this.context.SaveChanges();

            return package;
        }
    }
}
