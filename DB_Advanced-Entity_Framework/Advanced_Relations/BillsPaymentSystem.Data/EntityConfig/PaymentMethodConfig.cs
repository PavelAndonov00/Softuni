namespace BillsPaymentSystem.Data.EntityConfig
{
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder
                .HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods);

            builder
                .HasOne(pm => pm.BankAccount)
                .WithOne(ba => ba.PaymentMethod);

            builder
                .HasOne(pm => pm.CreditCard)
                .WithOne(ba => ba.PaymentMethod);

            builder
                .Property(pm => pm.Type)
                .IsRequired();

            builder
                .Property(pm => pm.UserId)
                .IsRequired();
        }
    }
}
