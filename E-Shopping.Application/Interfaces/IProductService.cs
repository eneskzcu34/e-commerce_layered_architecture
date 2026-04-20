using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Application.DTOs.ProductDTos;

namespace E_Shopping.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllProductsAsync();
        Task<ProductGetByIdDto> GetByIdProductAsync(int id);
        Task CreateProductAsync(ProductCreateDto model);
        Task<ProductUpdateDto> GetUpdateProductAsync(int id);
        Task UpdateProductAsync(int id, ProductUpdateDto model);
    }
}