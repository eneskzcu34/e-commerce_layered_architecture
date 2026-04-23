using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Application.DTOs.CategoryDTos;

namespace E_Shopping.Application.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryCreateDto model);
        Task<List<CategoryListDto>> GetAllCategoriesAsync();
        Task<CategoryUpdateDtos> GetCategoryUpdate(int id);
        Task CategoryUpdate(int id, CategoryUpdateDtos model);
    }
}