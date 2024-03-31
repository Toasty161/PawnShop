using Microsoft.EntityFrameworkCore;
using PawnShop.Core.Contracts;
using PawnShop.Core.Models;
using PawnShop.Data;
using PawnShop.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductViewModel productViewModel)
        {
            var entity = new Product()
            {
                CategoryId = productViewModel.CategoryId,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Id = productViewModel.Id,
                OwnerId = productViewModel.OwnerId,
                Weight = productViewModel.Weight
            };

            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);

            _context.Products.Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ProductViewModel productViewModel)
        {
            var entity = new Product()
            {
                CategoryId = productViewModel.CategoryId,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Id = productViewModel.Id,
                Weight = productViewModel.Weight,
                OwnerId = productViewModel.OwnerId
            };

            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            return await _context.Products.Where(p => p.Id == id).Select(p => new ProductViewModel()
            {
                CategoryId = p.CategoryId,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                Weight = p.Weight
            }).FirstAsync();
        }
    }
}
