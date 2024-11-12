using Dapper;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Entities;
using eScape.Infrastructure.SqlServer.DataAccess;
using eScape.UseCase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace eScape.Infrastructure.SqlServer.Repositories
{
    public class RefreshTokenRepository : DapperBase, IRefreshTokenRepository
    {
        public async Task<string?> RetrieveUserNameByRefreshToken(string refreshToken)
        {
            return await WithConnection(async connection =>
            {
                string query = "SELECT UserName FROM RefreshToken WHERE Token = " + refreshToken;
                var userName = await connection.QueryFirstOrDefaultAsync(query);
                return userName;
            });
        }

        public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(refreshToken);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdRefreshToken, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }

    }
}
