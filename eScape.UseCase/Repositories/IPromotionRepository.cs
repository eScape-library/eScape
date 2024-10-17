using eScape.Entities;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<PromotionDTO>> GetPromotionsAsync();
        Task<bool> UpdatePromotionAsync(Promotion promotion, string action);
    }
}
