using Panda.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Services
{
    public interface IPackageService
    {
        void CreatePackage(string description, decimal weight, string shippingAddress, string recipientName);

        IEnumerable<Package> GetAllPackageByCurrentLoggedUserIdAndStatus(string currentLoggedId, string status);
        Package ChangePackageToDeliveredStatusById(string id);
    }
}
