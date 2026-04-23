using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace E_Shopping.Application.DTOs.ProductDTos
{
    public class ProductImagesDto
    {
        public int Id { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}