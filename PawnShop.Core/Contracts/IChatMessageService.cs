using PawnShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Core.Contracts
{
    public interface IChatMessageService
    {
        Task AddAsync(ChatMessageViewModel model);

        Task DeleteAsync(int id);

        Task EditAsync(ChatMessageViewModel model);

        Task<ChatMessageViewModel> GetByIdAsync(int id);
    }
}
