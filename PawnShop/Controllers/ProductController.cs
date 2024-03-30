﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;
using PawnShop.Infrastructure.Data.Models;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PawnShop.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;

        public ProductController(ApplicationDbContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> All()
        {
            var entities = await _context.Products.ToListAsync();

            var model = entities.Select(p => new ProductViewModel()
            {
                CategoryId = p.CategoryId,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                OwnerId = p.OwnerId,
                Weight = p.Weight,
                Category = _context.Categories.Find(p.CategoryId)!.Name
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            model.OwnerId = GetUserId();
            model.Category = _context.Categories.FindAsync(model.CategoryId).Result.Name;

            await _productService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await _context.Products.FindAsync(id) == null)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await _productService.GetByIdAsync(id);

            TempData["EditId"] = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            model.OwnerId = GetUserId();
            model.Category = _context.Categories.FindAsync(model.CategoryId).Result.Name;
            model.Id = (int)TempData["EditId"];

            await _productService.EditAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _context.Products.FindAsync(id) == null)
            {
                return RedirectToAction(nameof(All));
            }

            TempData["DeleteId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _productService.DeleteAsync((int)TempData["DeleteId"]);

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
