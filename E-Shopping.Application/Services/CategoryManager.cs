using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Shopping.Application.DTOs.CategoryDTos;
using E_Shopping.Application.Interfaces;
using E_Shopping.Domain.Entities;
using E_Shopping.Domain.Interfaces.Repositories;

namespace E_Shopping.Application.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryManager(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task CreateCategoryAsync(CategoryCreateDto model)
        {
            var category = _mapper.Map<Category>(model);
            await _categoryRepository.AddAsync(category);
        }

        public async Task<List<CategoryListDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllWithIncludesAsync(x => x.Products);
            var categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);
            return categoryListDtos;
        }
    }
}