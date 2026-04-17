using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Domain.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } // Örn: Renk
        public string Value { get; set; } // Kırmızı, M vs.
        public int Stock { get; set; }
        public Product Product { get; set; }
    }
}