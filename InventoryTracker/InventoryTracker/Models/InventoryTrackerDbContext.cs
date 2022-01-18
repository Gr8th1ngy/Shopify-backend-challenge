using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Models
{
    public class InventoryTrackerDbContext : DbContext
    {
        public InventoryTrackerDbContext(DbContextOptions<InventoryTrackerDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");
        }
    }
}
