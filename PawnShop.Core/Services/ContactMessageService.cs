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
    public class ContactMessageService : IContactMessageService
    {
        private readonly ApplicationDbContext _context;

        public ContactMessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ContactMessageViewModel model)
        {
            var entity = new ContactMessage()
            {
                Id = model.Id,
                Message = model.Message,
                Sender = model.Sender
            };

            await _context.ContactMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ContactMessages.FindAsync(id);

            _context.ContactMessages.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ContactMessageViewModel model)
        {
            var entity = new ContactMessage()
            {
                Id = model.Id,
                Message = model.Message,
                Sender = model.Sender
            };

            _context.ContactMessages.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ContactMessageViewModel> GetByIdAsync(int id)
        {
            return await _context.ContactMessages
                .Where(cm => cm.Id == id)
                .Select(cm => new ContactMessageViewModel()
            {
                Id = cm.Id,
                Message = cm.Message,
                Sender = cm.Sender
            }).FirstAsync();
        }
    }
}
