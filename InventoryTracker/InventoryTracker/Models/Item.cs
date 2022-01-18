using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool Deleted { get; set; }
        [Display(Name = "Delete Comment")]
        public string DeleteComment { get; set; }
    }
}
