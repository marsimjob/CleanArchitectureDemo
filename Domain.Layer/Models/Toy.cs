using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Layer.Models
{
    public class Toy
    {
        public int Id { get; set; }

        // Brand / Maker
        public string Brand { get; set; } = string.Empty;

        // Release Date
        public DateTime ReleaseDate { get; set; }

        // Name
        public string Name { get; set; } = string.Empty;

        // Series (IP)
        public string Series { get; set; } = string.Empty;

        // PlaySet
        public string PlaySet { get; set; } = string.Empty;

        // Price
        public decimal Price { get; set; }

        // Add summary for Exercise 3
        public string Summary { get; set; } = string.Empty;

        // Exercise 4
        public bool IsDeleted { get; set; } = false;
    }
}
