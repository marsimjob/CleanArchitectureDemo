using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Layer.DTOs
{
    public record ToyDto
    {
        public int Id { get; init; }
        // Name
        public string Name { get; init; } = string.Empty;

        // Series (IP)
        public string Series { get; init; } = string.Empty;

        // PlaySet
        public string PlaySet { get; init; } = string.Empty;

        // Price
        public decimal Price { get; init; }

        // Brand / Maker
        public string Brand { get; init; } = string.Empty;
    }
}
