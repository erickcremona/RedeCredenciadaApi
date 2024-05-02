using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopDown.Core.Data;

namespace RedeCredenciadaInfraData.Common
{
    public abstract class BaseRepository
    {
        protected readonly BaseData _baseData;
        protected readonly ILogger<BaseRepository> _logger;

        public BaseRepository(BaseData baseData, ILogger<BaseRepository> logger)
        {
            _baseData = baseData;
            _logger = logger;
        }

        protected async Task<T> ExecuteQueryFirstAsync<T>(string query, object param = null)
            => await _baseData.DbConnection?.QueryFirstOrDefaultAsync<T>(query, param: param);

        protected async Task<T> ExecuteScalarAsync<T>(string query, object param = null)
            => await _baseData.DbConnection?.ExecuteScalarAsync<T>(query, param: param);

        protected async Task<object> ExecuteProcedureAsycn(string query, object param = null)
            => await _baseData.DbConnection?.QueryAsync(query, param: param, commandType: System.Data.CommandType.StoredProcedure);

        protected async Task<IEnumerable<T>> ExecuteProcedureAsycn<T>(string query, object param = null)
            => await _baseData.DbConnection?.QueryAsync<T>(query, param: param, commandType: System.Data.CommandType.StoredProcedure);

        protected async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, string param = null)
            => await _baseData.DbConnection?.QueryAsync<T>(query, param: param);

    }
}