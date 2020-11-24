using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly PandaDbContext context;

        public ReceiptsService(PandaDbContext context)
        {
            this.context = context;
        }

        public Receipt CreateReceipt(decimal Fee, string recipientId, string packageId)
        {
            var receipt = new Receipt
            {
                Fee = Fee,
                RecipientId = recipientId,
                PackageId = packageId
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();

            return receipt;
        }

        public IQueryable<Receipt> GetReceiptsByUserId(string userId)
        {
            var receipts = context.Receipts
                .Include(r => r.Recipient)
                .Where(r => r.RecipientId == userId).ToList();

            return receipts.AsQueryable();
        }
    }
}
