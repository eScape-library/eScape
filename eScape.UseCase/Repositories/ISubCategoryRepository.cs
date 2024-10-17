using eScape.Entities;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesAsync();
        Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, string action);
    }
}
