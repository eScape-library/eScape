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
    public class PromotionRepository : DapperBase, IPromotionRepository
    {
        public async Task<IEnumerable<PromotionDTO>> GetPromotionsAsync()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<PromotionDTO>(StoredProcedureName.UspGetPromotion, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<bool> UpdatePromotionAsync(Promotion promotion, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(promotion);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdPromotion, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }
    }
}
