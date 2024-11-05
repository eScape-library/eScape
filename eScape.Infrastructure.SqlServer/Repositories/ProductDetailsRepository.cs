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
using static System.Collections.Specialized.BitVector32;

namespace eScape.Infrastructure.SqlServer.Repositories
{
    public class ProductDetailsRepository : DapperBase, IProductDetailsRepository
    {
        public async Task<IEnumerable<ProductDetailsDTO>> GetProductDetailsAsync()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<ProductDetailsDTO>(StoredProcedureName.UspGetProductDetails, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<ProductDetailsWithFamiliar> GetProductDetailsByIdAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add(Parameters.ProductDetailsId, id, DbType.Int32, ParameterDirection.Input);
                var multi = await connection.QueryMultipleAsync(StoredProcedureName.UspGetProductDetailsById, param: parameters, commandType: CommandType.StoredProcedure);
                var product = await multi.ReadFirstAsync<ProductDetailsDTO>();
                var variantColor = await multi.ReadAsync<ProductDetailsDTO>();
                var familiar = await multi.ReadAsync<CollectionDTO>();

                var variants = new List<Variant>();
                foreach (var item in variantColor)
                {
                    variants.Add(new Variant() 
                    { 
                        VariantColor = item, 
                        VariantSize = await GetVariantSize(item.ProductId, item.ColorAttributeId) 
                    });
                }

                return new ProductDetailsWithFamiliar()
                {
                    Product = product,
                    Variants = variants,
                    Familiar = familiar
                };
            });
        }

        public async Task<IEnumerable<ProductDetailsDTO>> GetVariantSize(int productId, int colorId)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add(Parameters.ProductId, productId, DbType.Int32, ParameterDirection.Input);
                parameters.Add(Parameters.ColorAttributeId, colorId, DbType.Int32, ParameterDirection.Input);
                var variants = await connection.QueryAsync<ProductDetailsDTO>(StoredProcedureName.UspGetVariantsBySize, param: parameters, commandType: CommandType.StoredProcedure);

                return variants.ToList();
            });
        }

        public async Task<bool> UpdateProductDetailsAsync(ProductDetails productDetails, string action)
        {
            return await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(productDetails);
                parameters.Add(Parameters.JInput, jInput, DbType.String, ParameterDirection.Input);
                parameters.Add(Parameters.Action, action);
                parameters.Add(Parameters.ReturnValue, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await connection.QueryFirstOrDefaultAsync(StoredProcedureName.UspInsUpdProductDetails, param: parameters, commandType: CommandType.StoredProcedure);
                var isSuccess = parameters.Get<int>(Parameters.ReturnValue);
                return isSuccess > 0;
            });
        }
    }
}
