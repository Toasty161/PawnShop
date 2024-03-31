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
    public class PossessionService : IPossessionService
    {
        private readonly ApplicationDbContext _context;

        public PossessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PossessionViewModel possessionViewModel)
        {
            var entity = new Possession()
            {
                Id = possessionViewModel.Id,
                Area = possessionViewModel.Area,
                Location = possessionViewModel.Location,
                Name = possessionViewModel.Name,
                OwnerId = possessionViewModel.OwnerId
            };

            await _context.Possessions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Possessions.FindAsync(id);

            _context.Possessions.Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(PossessionViewModel possessionViewModel)
        {
            var entity = new Possession()
            {
                Id = possessionViewModel.Id,
                Area = possessionViewModel.Area,
                Location = possessionViewModel.Location,
                Name = possessionViewModel.Name,
                OwnerId= possessionViewModel.OwnerId
            };

            _context.Possessions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PossessionViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Possessions.FindAsync(id);

            return new PossessionViewModel()
            {
                Area = entity.Area,
                Location = entity.Location,
                Name = entity.Name,
                OwnerId = entity.OwnerId,
                Id = entity.Id
            };
        }
    }
}
