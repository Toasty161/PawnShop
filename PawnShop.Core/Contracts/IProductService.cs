using PawnShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Core.Contracts
{
    public interface IProductService
    {
        Task AddAsync(ProductViewModel productViewModel);

        Task DeleteAsync(int id);

        Task EditAsync(ProductViewModel productViewModel);

        Task<ProductViewModel> GetByIdAsync(int id);
    }
}
