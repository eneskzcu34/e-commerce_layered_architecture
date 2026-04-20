using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shopping.Application.DTOs.ProductDTos
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stok zorunludur.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Kategori zorunludur.")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
        public IFormFile Images { get; set; }

    }
}