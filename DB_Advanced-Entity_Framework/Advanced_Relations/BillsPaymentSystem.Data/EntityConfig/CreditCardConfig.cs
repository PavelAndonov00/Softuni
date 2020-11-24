namespace BillsPaymentSystem.Data.EntityConfig
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(cc => cc.CreditCardId);

            builder
                .Property(cc => cc.Limit)
                .IsRequired();

            builder
                .Property(cc => cc.MoneyOwed)
                .IsRequired();

            builder.Ignore(cc => cc.LimitLeft);

            builder
                .Property(cc => cc.ExpirationDate)
                .IsRequired();      
        }
    }
}
