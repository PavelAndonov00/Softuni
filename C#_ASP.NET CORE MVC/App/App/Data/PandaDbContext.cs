﻿using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class PandaDbConext : IdentityDbContext
    {
        public PandaDbConext(DbContextOptions<PandaDbConext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity
                .HasMany(user => user.Packages)
                .WithOne(package => package.Recipient)
                .HasForeignKey(package => package.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(user => user.Receipts)
                .WithOne(receipt => receipt.Recipient)
                .HasForeignKey(receipt => receipt.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Package>(entity =>
            {
                entity
                .HasMany(package => package.Receipts)
                .WithOne(receipt => receipt.Package)
                .HasForeignKey(receipt => receipt.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

    }
}
