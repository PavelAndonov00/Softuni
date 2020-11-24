﻿using Microsoft.EntityFrameworkCore;
using Suls.Models;
using System;

namespace Suls.Data
{
    public class SulsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseSettings.connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity
                .HasMany(u => u.Submissions)
                .WithOne(s => s.User);
            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity
                .HasMany(p => p.Submissions)
                .WithOne(s => s.Problem);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
