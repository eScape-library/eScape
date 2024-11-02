using AutoMapper;
using Dapper;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Entities;
using eScape.UseCase.DTOs;
using eScape.Infrastructure.SqlServer.DataAccess;
using eScape.UseCase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Infrastructure.SqlServer.Repositories
{
    public class SubCategoryRepository : DapperBase, ISubCategoryRepository
    {
        public async Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesAsync()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<SubCategoryDTO>(StoredProcedureName.UspGetSubCategory, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<bool> UpdateSubCategoryAsync(SubCategory subCategory, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(subCategory);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdSubCategory, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }
    }
}
