using Dapper;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Entities;
using eScape.Infrastructure.SqlServer.DataAccess;
using eScape.UseCase.DTOs;
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
    public class UserRepository : DapperBase, IUserRepository
    {
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<UserDTO>(StoredProcedureName.UspGetUser, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<UserDTO?> GetAUserAsync(string userName)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add(Parameters.UserName, userName, DbType.String, ParameterDirection.Input);
                var user = await connection.QueryFirstOrDefaultAsync<UserDTO>(StoredProcedureName.UspGetUser, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            });
        }

        public async Task<bool> UpdateUserAsync(User user, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(user);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdUser, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }
    }
}
