using eScape.Entities;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartDTO>> GetCartAsync(int userId);
        Task<bool> UpdateCartAsync(Cart cart, string action);
    }
}
