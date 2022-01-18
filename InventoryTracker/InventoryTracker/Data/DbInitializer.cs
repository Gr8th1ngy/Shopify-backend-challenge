using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventoryTrackerDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if DB is created
            if (context.Items.Any())
            {
                return;
            }

            var items = new Item[]
            {
                new Item {
                    Name = "Fish oil",
                    Description = "Omega-3 supplement",
                    Count = 10,
                    Deleted = false
                },
                new Item {
                    Name = "Laptop",
                    Description = "Asus laptop model 3",
                    Count = 5,
                    Deleted = false
                },
                new Item {
                    Name = "Plate",
                    Description = "White ceramic plate",
                    Count = 150,
                    Deleted = false
                },
            };

            foreach (Item i in items)
            {
                context.Items.Add(i);
            }

            context.SaveChanges();
        }
    }
}
