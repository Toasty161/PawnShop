using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;
using PawnShop.Infrastructure.Data.IdentityModels;
using PawnShop.Infrastructure.Data.Models;
using System;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PawnShop.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(ApplicationDbContext context, 
            ProductService productService, 
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _productService = productService;
            _roleManager = roleManager;
            _userManager = userManager;
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

            var entity = await _context.Products.FindAsync(id);

            if (GetUserId() != entity.OwnerId)
            {
                if (!User.IsInRole("Administrator"))
                {
					return RedirectToAction(nameof(All));
				}
            }

            TempData["EditId"] = id;
            TempData["EditOwnerId"] = entity.OwnerId;

            var model = await _productService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            model.OwnerId = TempData["EditOwnerId"].ToString();
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

            var entity = await _context.Products.FindAsync(id);

            if (GetUserId() != entity.OwnerId)
            {
                if (!User.IsInRole("Administrator"))
                {
					return RedirectToAction(nameof(All));
				}
            }

            TempData["DeleteId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            int deleteId = (int)TempData["DeleteId"];

            var entity = await _context.Products.FindAsync(deleteId);

            if (GetUserId() != entity.OwnerId)
            {
                if (!User.IsInRole("Administrator"))
                {
					return RedirectToAction(nameof(All));
				}
            }

            await _productService.DeleteAsync(deleteId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
            if (await _context.Products.FindAsync(id) == null)
            {
                return RedirectToAction(nameof(All));
            }

            if (await _context.ProductBuyers.FindAsync(GetUserId(), id) != null)
            {
                return RedirectToAction(nameof(All));
            }

            var product = await _context.Products.FindAsync(id);

			if (product.OwnerId == GetUserId())
            {
                return RedirectToAction(nameof(All));
            }

            TempData["BuyId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy()
        {
            int buyId = (int)TempData["BuyId"];

            if (await _context.ProductBuyers.FindAsync(GetUserId(), buyId) != null)
            {
                return RedirectToAction(nameof(All));
            }

			var product = await _context.Products.FindAsync(buyId);

			if (product.OwnerId == GetUserId())
			{
				return RedirectToAction(nameof(All));
			}

			var entity = new ProductBuyer()
            {
                BuyerId = GetUserId(),
                ProductId = buyId
            };

            await _context.ProductBuyers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> BoughtProducts()
        {
            var entities = await _context.ProductBuyers.Where(pb => pb.BuyerId == GetUserId()).ToListAsync();

            List<ProductViewModel> model = new List<ProductViewModel>();

            foreach (var productBuyer in entities)
            {
                model.Add(await _productService.GetByIdAsync(productBuyer.ProductId));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Sell(int id)
        {
            if (await _context.ProductBuyers.FindAsync(GetUserId(), id) == null)
            {
                return RedirectToAction(nameof(All));
            }

            TempData["SellId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Sell()
        {
            var entity = await _context.ProductBuyers.FindAsync(GetUserId(), (int)TempData["SellId"]);

            _context.ProductBuyers.Remove(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
