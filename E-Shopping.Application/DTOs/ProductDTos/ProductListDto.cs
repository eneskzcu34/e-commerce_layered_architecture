using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Application.DTOs.ProductDTos
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
    }
}