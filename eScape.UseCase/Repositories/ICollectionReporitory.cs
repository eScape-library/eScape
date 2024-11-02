using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface ICollectionReporitory
    {
        Task<GridReturn<CollectionDTO>> GetProductsAsync(int subCategoryId, PagingOption paging);
    }
}
