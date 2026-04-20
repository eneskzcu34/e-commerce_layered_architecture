using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Application.DTOs.ProductDTos;
using E_Shopping.Application.Interfaces;
using E_Shopping.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult Detail()
        {
            return View();
        }

        [Route("/Admin/[controller]/List")]
        [HttpGet]
        public async Task<IActionResult> PAIndex()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [Route("/Admin/[controller]/Create")]
        [HttpGet]
        public async Task<IActionResult> PACreate()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var model = new ProductCreateDto
            {
                Categories = new SelectList(categories, "Id", "Name")
            };
            return View(model);
        }

        [Route("/Admin/[controller]/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PACreateAsync(ProductCreateDto model)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(model);
                return RedirectToAction("PAIndex");
            }
            else
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurun.");
                var categories = await _categoryService.GetAllCategoriesAsync();
                model = new ProductCreateDto
                {
                    Categories = new SelectList(categories, "Id", "Name")
                };
            }
            return View(model);
        }

        [Route("/Admin/[controller]/Update/{id}")]
        [HttpGet]
        public async Task<IActionResult> PAUpdate(int id)
        {
            var model = await _productService.GetUpdateProductAsync(id);
            var categories = await _categoryService.GetAllCategoriesAsync();
            model.Categories = new SelectList(categories, "Id", "Name");
            return View(model);
        }

        [Route("/Admin/[controller]/Update/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PAUpdate(int id, ProductUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(id, model);
                return RedirectToAction("PAIndex");
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            model.Categories = new SelectList(categories, "Id", "Name");
            return View(model);
        }

    }
}
