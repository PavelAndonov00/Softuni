using System;
using System.Collections.Generic;
using System.Text;
using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.BankAccountId);

            builder
                .Property(ba => ba.Balance)
                .IsRequired();

            builder
                .Property(ba => ba.BankName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
               .Property(ba => ba.SWIFT)
               .IsRequired()
               .IsUnicode(false)
               .HasMaxLength(20);
        }
    }
}
