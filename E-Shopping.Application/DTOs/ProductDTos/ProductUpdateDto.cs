using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shopping.Application.DTOs.ProductDTos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stok zorunludur.")]
        public int Stock { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Kategori zorunludur.")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
        public List<ProductImagesDto> Images { get; set; } = new();
        public List<IFormFile>? NewImages { get; set; }
        public int? SelectedMainImageId { get; set; }
    }
}