using Fluffy_Duffy_Munchkin_Cats.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluffy_Duffy_Munchkin_Cats.Data
{
    public class FluffyDbContext : DbContext
    {
        public FluffyDbContext(DbContextOptions<FluffyDbContext> options)
            : base(options)
        { }  

        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
