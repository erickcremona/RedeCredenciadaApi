using Dapper;
using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.Entities.Produto;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopDown.Core.Data;

namespace RedeCredenciadaInfraData.Repository
{
    public class RepositoryProduto : RepositoryBase<ProdutoEntity>, IRepositoryProduto
    {
        public RepositoryProduto(BaseData baseData,
                 ILogger<RepositoryProduto> logger)
                        : base(baseData, logger) { }

        public async Task<IEnumerable<ProdutoEntity>> GetProdutosAsync()
        {
            const string query = @"SELECT a.cod_produto_ans AS codigo
                                         ,upper(a.nome_produto_ans) AS nome
                                         ,row_number() over(ORDER BY a.cod_produto_ans) AS ordem
                                    FROM produto_ans a";

            return await _baseData.DbConnection.QueryAsync<ProdutoEntity>(query);
        }
    }
}
