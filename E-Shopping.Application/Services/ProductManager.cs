using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Shopping.Application.DTOs.ProductDTos;
using E_Shopping.Application.Interfaces;
using E_Shopping.Domain.Entities;
using E_Shopping.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace E_Shopping.Application.Services
{
    public class ProductManager : IProductService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IRepository<Product> productRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CreateProductAsync(ProductCreateDto model)
        {
            var product = _mapper.Map<Product>(model);
            if (model.Images != null && model.Images.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                string fileExtension = Path.GetExtension(model.Images.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Images.CopyToAsync(fileStream);
                }
                product.ImageUrl = "/images/products/" + uniqueFileName;
            }
            await _productRepository.AddAsync(product);
        }

        public async Task<List<ProductListDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllWithIncludesAsync(c => c.Category);
            return _mapper.Map<List<ProductListDto>>(products).ToList();
        }

        public async Task<ProductGetByIdDto> GetByIdProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductGetByIdDto>(product);
        }

        public async Task<ProductUpdateDto> GetUpdateProductAsync(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id, p => p.Category);
            return _mapper.Map<ProductUpdateDto>(product);
        }

        public async Task UpdateProductAsync(int id, ProductUpdateDto model)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id, p => p.Category);
            if (product == null) return;
            _mapper.Map(model, product);
            if (model.NewImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));

                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }
                string extension = Path.GetExtension(model.NewImage.FileName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewImage.CopyToAsync(stream);
                }

                product.ImageUrl = "/images/products/" + uniqueName;
            }

            _productRepository.Update(product);
        }
    }
}