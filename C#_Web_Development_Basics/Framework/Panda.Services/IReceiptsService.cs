using Panda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public interface IReceiptsService
    {
        IQueryable<Receipt> GetReceiptsByUserId(string userId);

        Receipt CreateReceipt(decimal Fee, string recipientId, string packageId);
    }
}
