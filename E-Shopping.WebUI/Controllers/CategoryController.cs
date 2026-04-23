using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Application.DTOs.CategoryDTos;
using E_Shopping.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Shopping.WebUI.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/[controller]/List")]
        [HttpGet]
        public async Task<IActionResult> CAIndex()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [Route("/Admin/[controller]/Create")]
        [HttpGet]
        public IActionResult CACreate()
        {
            return View();
        }

        [Route("/Admin/[controller]/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CACreate(CategoryCreateDto model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(model);
                return RedirectToAction("CAIndex");
            }
            else
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurun.");
            }
            return View(model);
        }
        [Route("/Admin/[controller]/Update/{id}")]
        [HttpGet]
        public async Task<IActionResult> CAUpdate(int id)
        {
            var category = await _categoryService.GetCategoryUpdate(id);
            return View(category);
        }

        [Route("/Admin/[controller]/Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> CAUpdate(int id, CategoryUpdateDtos model)
        {
            if (id == model.Id)
                if (ModelState.IsValid)
                {
                    await _categoryService.CategoryUpdate(id, model);
                    return RedirectToAction("CAIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Böyle Bir Kategori Yok");
                }
            return View(model);
        }
    }
}