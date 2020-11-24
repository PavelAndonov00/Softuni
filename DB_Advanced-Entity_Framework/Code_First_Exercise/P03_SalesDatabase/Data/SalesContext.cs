using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>(entity =>
                {
                    entity
                    .HasKey(p => p.ProductId);

                    entity
                    .HasMany(p => p.Sales)
                    .WithOne(s => s.Product);

                    entity
                    .Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsUnicode();

                    entity
                    .Property(p => p.Description)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");
                });

            modelBuilder
                .Entity<Customer>(entity =>
                {
                    entity
                    .HasKey(c => c.CustomerId);

                    entity
                    .HasMany(c => c.Sales)
                    .WithOne(s => s.Customer);

                    entity
                    .Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsUnicode();

                    entity
                    .Property(p => p.Email)
                    .HasMaxLength(80);
                });

            modelBuilder
                .Entity<Store>(entity =>
                {
                    entity
                    .HasKey(s => s.StoreId);

                    entity
                    .HasMany(st => st.Sales)
                    .WithOne(s => s.Store);

                    entity
                    .Property(p => p.Name)
                    .HasMaxLength(80)
                    .IsUnicode();
                });

            modelBuilder
                .Entity<Sale>(entity =>
                {
                    entity
                    .HasKey(s => s.SaleId);

                    entity
                    .Property(p => p.Date)
                    .HasDefaultValueSql("GETDATE()");
                });
        }
    }
}
