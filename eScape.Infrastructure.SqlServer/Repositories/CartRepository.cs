using Dapper;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Entities;
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
    public class CartRepository : DapperBase, ICartRepository
    {
        public async Task<IEnumerable<CartDTO>> GetCartAsync(int userId)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add(Parameters.UserId, userId, DbType.Int32, ParameterDirection.Input);
                var result = await connection.QueryAsync<CartDTO>(StoredProcedureName.UspGetCart, param: parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<bool> UpdateCartAsync(Cart cart, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(cart);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdCart, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }
    }
}
