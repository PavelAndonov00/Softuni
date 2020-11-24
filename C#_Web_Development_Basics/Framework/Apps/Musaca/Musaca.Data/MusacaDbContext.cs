using Microsoft.EntityFrameworkCore;
using Musaca.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musaca.Data
{
    public class MusacaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity
                .HasKey(op => new { op.OrderId, op.ProductId});

                entity
                .HasOne(op => op.Order)
                .WithMany(o => o.OrdersProducts)
                .HasForeignKey(op => op.OrderId);

                entity
                .HasOne(op => op.Product)
                .WithMany(p => p.ProductsOrders)
                .HasForeignKey(op => op.ProductId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
