using Dapper;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Infrastructure.SqlServer.DataAccess;
using eScape.UseCase;
using eScape.UseCase.DTOs;
using eScape.UseCase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Infrastructure.SqlServer.Repositories
{
    public class CollectionReporitory : DapperBase, ICollectionReporitory
    {
        public async Task<GridReturn<CollectionDTO>> GetProductsAsync(int subCategoryId, PagingOption paging)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jsonPaging = JsonDeserializeHelper.SerializeObjectForDb(paging);
                parameters.Add(Parameters.PagingOption, jsonPaging);
                parameters.Add(Parameters.SubCategoryId, subCategoryId, DbType.Int32, ParameterDirection.Input);
                parameters.Add(Parameters.TotalRecord, dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryAsync<CollectionDTO>(StoredProcedureName.UspGetProductsByCategory, param: parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                var total = parameters.Get<int>(Parameters.TotalRecord);
                return new GridReturn<CollectionDTO>
                {
                    Items = result.ToList(),
                    Total = total
                };
            });
        }
    }
}
