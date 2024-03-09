using PawnShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Core.Contracts
{
    public interface IPossessionService
    {
        Task AddAsync(PossessionViewModel possessionViewModel);

        Task DeleteAsync(int id);

        Task EditAsync(PossessionViewModel possessionViewModel);

        Task<PossessionViewModel> GetByIdAsync(int id);
    }
}
