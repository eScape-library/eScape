using eScape.Core.Helper;
using eScape.UseCase;
using eScape.UseCase.DTOs;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/collection")]
    [ApiController]
    public class CollectionController(ICollectionReporitory collectionReporitory) : ControllerBase
    {
        private readonly ICollectionReporitory _collectionReporitory = collectionReporitory;

        [HttpPost("{subCategoryId:int}")]
        public async Task<IActionResult> GetProductsAsync(int subCategoryId, [FromBody] PagingOption paging)
        {
            if(paging.WhereClause != null)
            {
                var filter = JsonConvert.DeserializeObject<CollectionFilterDTO>(paging.WhereClause);
                Criteria color = new();
                Criteria size = new();
                Criteria price = new();
                if(filter != null && filter.Color!.ToList().Count > 0)
                {
                    var colorArray = filter.Color!.ToArray();
                    color = new Criteria { Key = "Color", Value = string.Join(", ", Array.ConvertAll(colorArray, item => $"'{item}'")) };
                }
                if (filter != null && filter.Size!.ToList().Count > 0)
                {
                    var sizeArray = filter.Size!.ToArray();
                    size = new Criteria { Key = "Size", Value = string.Join(", ", Array.ConvertAll(sizeArray, item => $"'{item}'")) };
                }
                if (filter != null)
                {
                    price = new Criteria { Key = "Price", Value = GenerateWhereClauseHelper.GetPriceClause(filter.Price!) };
                }
                paging.WhereClause = GenerateWhereClauseHelper.Generate(color, size, price);
            }
            
            var collection = await _collectionReporitory.GetProductsAsync(subCategoryId, paging);
            return Ok(collection);
        }
       
    }
}
