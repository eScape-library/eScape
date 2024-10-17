using eScape.Entities;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface IProductDetailsRepository
    {
        Task<IEnumerable<ProductDetailsDTO>> GetProductDetailsAsync();
        Task<bool> UpdateProductDetailsAsync(ProductDetails productDetails, string action);
    }
}
