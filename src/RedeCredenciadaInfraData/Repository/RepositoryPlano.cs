using Dapper;
using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities.Plano;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDown.Core.Data;

namespace RedeCredenciadaInfraData.Repository
{
    public class RepositoryPlano : RepositoryBase<PlanoEntity>, IRepositoryPlano
    {
        public RepositoryPlano(BaseData baseData,
               ILogger<RepositoryPlano> logger)
                     : base(baseData, logger) { }

        public async Task<IEnumerable<PlanoEntity>> GetPlanosAsync(BaseFilterConsultaDTO request)
        {
            const string query = @"SELECT pm.cod_plano AS Codigo
                                         ,pm.nome_plano AS Nome
                                         ,pm.cod_registro_ans AS NumeroRegistroAns
                                         ,(CASE pm.cod_produto
                                             WHEN '1' THEN
                                              'Individual'
                                             WHEN '2' THEN
                                              'Coletivo empresarial'
                                             ELSE
                                              'Coletivo por Adesão'
                                          END) AS Classificacao
                                         ,(CASE pm.ind_situacao
                                             WHEN 'A' THEN
                                              'Ativo'
                                             ELSE
                                              'Encerrado'
                                          END) AS Situacao
                                         ,pm.cod_plano AS CodigoTopSaude
                                     FROM plano_medico pm
                                    WHERE pm.cod_produto_ans = :codigoProduto";

            return await _baseData.DbConnection.QueryAsync<PlanoEntity>(query, new { codigoProduto = request.CodigoProduto });
        }
    }
}
