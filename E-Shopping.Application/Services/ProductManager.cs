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
            if (model.Images == null || !model.Images.Any())
                return;
            var product = _mapper.Map<Product>(model);
            product.Images = new List<ProductImages>();

            foreach (var file in model.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images/products", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                product.Images.Add(new ProductImages
                {
                    ImageUrl = "/images/products/" + fileName,
                    IsMain = !product.Images.Any(x => x.IsMain)
                });
            }
            await _productRepository.AddAsync(product);
        }

        public async Task<List<ProductListDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllWithIncludesAsync(c => c.Category, p => p.Images);
            return _mapper.Map<List<ProductListDto>>(products).ToList();
        }

        public async Task<ProductGetByIdDto> GetByIdProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductGetByIdDto>(product);
        }

        public async Task<ProductUpdateDto> GetUpdateProductAsync(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id, p => p.Category, p => p.Images);
            var dto = _mapper.Map<ProductUpdateDto>(product);
            dto.Images ??= new List<ProductImagesDto>();
            return dto;
        }

        public async Task UpdateProductAsync(int id, ProductUpdateDto model)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id, p => p.Category, p => p.Images);
            if (product == null) return;
            _mapper.Map(model, product);

            if (model.SelectedMainImageId.HasValue)
            {
                foreach (var img in product.Images)
                {
                    img.IsMain = img.Id == model.SelectedMainImageId.Value;
                }
            }
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");

            for (int i = 0; i < model.Images.Count; i++)
            {
                var dtoImage = model.Images[i];

                if (dtoImage.NewImage != null && dtoImage.NewImage.Length > 0)
                {
                    var existingImage = product.Images.FirstOrDefault(x => x.Id == dtoImage.Id);

                    if (existingImage != null)
                    {
                        var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, existingImage.ImageUrl.TrimStart('/')
                        );

                        if (File.Exists(oldPath))
                            File.Delete(oldPath);

                        var fileName = Guid.NewGuid() + Path.GetExtension(dtoImage.NewImage.FileName);
                        var path = Path.Combine(folder, fileName);

                        using var stream = new FileStream(path, FileMode.Create);
                        await dtoImage.NewImage.CopyToAsync(stream);

                        existingImage.ImageUrl = "/images/products/" + fileName;
                    }
                }
            }
            _productRepository.Update(product);
        }
    }
}
