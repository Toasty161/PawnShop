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
    public class ChatMessageService : IChatMessageService
    {
        private readonly ApplicationDbContext _context;

        public ChatMessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ChatMessageViewModel model)
        {
            var entity = new ChatMessage()
            {
                Id = model.Id,
                Message = model.Message,
                Sender = model.Sender
            };

            await _context.ChatMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ChatMessages.FindAsync(id);

            _context.ChatMessages.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ChatMessageViewModel model)
        {
            var entity = new ChatMessage()
            {
                Id = model.Id,
                Message = model.Message,
                Sender = model.Sender
            };

            _context.ChatMessages.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatMessageViewModel> GetByIdAsync(int id)
        {
            return await _context.ChatMessages
                .Where(cm => cm.Id == id)
                .Select(cm => new ChatMessageViewModel()
            {
                Id = cm.Id,
                Message = cm.Message,
                Sender = cm.Sender
            }).FirstAsync();
        }
    }
}
