using Microsoft.AspNetCore.Http;
namespace E_Shopping.Application.DTOs.ProductDTos
{
    public class ProductImagesDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}