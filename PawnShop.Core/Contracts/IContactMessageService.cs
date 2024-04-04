using PawnShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Core.Contracts
{
    public interface IContactMessageService
    {
        Task AddAsync(ContactMessageViewModel model);

        Task EditAsync(ContactMessageViewModel model);

        Task DeleteAsync(int id);

        Task<ContactMessageViewModel> GetByIdAsync(int id);
    }
}
