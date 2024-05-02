using Dapper;
using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities;
using RedeCredenciadaDomain.Entities.NovoRecurso;
using RedeCredenciadaDomain.Entities.Recurso;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopDown.Core.Data;

namespace RedeCredenciadaInfraData.Repository
{
    public class RepositoryRecursos : RepositoryBase<RecursosEntity>, IRepositoryRecursos
    {
        public RepositoryRecursos(BaseData baseData,
                  ILogger<RepositoryProduto> logger)
                         : base(baseData, logger) { }

        public async Task<ResultadoEntity> GetRecursosAsync(BaseFilterConsultaDTO request)
        {
            var (query, parameters) = MontaQuery(nameof(GetRecursosAsync), request);
            var planoDictionary = new Dictionary<string, ConsultaSubstituicaoDTO>();
            var resultado = new ResultadoEntity { Recursos = new List<RecursosEntity>() };

            var retorno = await _baseData.DbConnection.QueryAsync<ConsultaSubstituicaoDTO, ExclusaoDTO, ConsultaSubstituicaoDTO>(
                    query,
                    map: (substituicao, exclusao) =>
                    {
                        if (!planoDictionary.TryGetValue(substituicao.CodigoNovoRecurso, out ConsultaSubstituicaoDTO _substituicao))
                        {
                            _substituicao = substituicao;
                            var recursos = new RecursosEntity
                            {
                                DataExclusao = substituicao.DataExclusao,
                                DataInclusaoNovo = substituicao.DataInclusaoNovo,
                                NovoRecurso = new NovoRecursoEntity
                                {
                                    CnpjNovoRecurso = FormatCNPJ(substituicao.CnpjNovoRecurso),
                                    CodigoNovoRecurso = substituicao.CodigoNovoRecurso,
                                    CrmNovoRecurso = substituicao.CrmNovoRecurso,
                                    NomeNovoRecurso = substituicao.NomeNovoRecurso,
                                    RazaoSocialNovoRecurso = substituicao.RazaoSocialNovoRecurso,
                                    TipoAtendimentoNovoRecurso = substituicao.TipoAtendimentoNovoRecurso,
                                    UnimedNovoRecurso = substituicao.UnimedNovoRecurso,
                                    EnderecoNovoRecurso = new EnderecoNovoRecursoEntity
                                    {
                                        BairroNovoRecurso = substituicao.BairroNovoRecurso,
                                        CepNovoRecurso = substituicao.CepNovoRecurso,
                                        ComplementoNovoRecurso = substituicao.ComplementoNovoRecurso,
                                        LogradouroNovoRecurso = substituicao.LogradouroNovoRecurso,
                                        NumeroNovoRecurso = substituicao.NumeroNovoRecurso,
                                        Cidade = new CidadeNovoRecursoEntity { CidadeNovoRecurso = substituicao.CidadeNovoRecurso },
                                        Estado = new EstadoNovoRecursoEntity { EstadoNovoRecurso = substituicao.EstadoNovoRecurso }
                                    },
                                    TelefoneNovoRecurso = new TelefoneNovoRecursoEntity
                                    {
                                        NumeroTelefoneNovoRecurso = substituicao.NumeroTelefoneNovoRecurso,
                                        TipoTelefoneNovoRecurso = substituicao.TipoTelefoneNovoRecurso
                                    },
                                    EspecialidadeNovoRecurso = EspecialidadesSubstituicao(substituicao.EspecialidadesNovoRecurso),
                                    QualificacaoNovoRecurso = new QualificacoesNovoRecursoEntity
                                    {
                                        Qualificacao = QualificacoesSubstituicao(substituicao.QualificacoesNovoRecurso)
                                    }
                                },
                                Recurso = new RecursoEntity
                                {
                                    CnpjRecurso = FormatCNPJ(exclusao.CnpjRecurso),
                                    CodigoRecurso = exclusao.CodigoRecurso,
                                    NomeRecurso = exclusao.NomeRecurso,
                                    RazaoSocialRecurso = exclusao.RazaoSocialRecurso,
                                    TipoAtendimentoRecurso = exclusao.TipoAtendimentoRecurso,
                                    UnimedRecurso = exclusao.UnimedRecurso,
                                    DataExclusaoSolicitada = exclusao.DataExclusaoSolicitada,
                                    EnderecoRecurso = new EnderecoRecursoEntity
                                    {
                                        BairroRecurso = exclusao.BairroRecurso,
                                        CepRecurso = exclusao.CepRecurso,
                                        ComplementoRecurso = exclusao.ComplementoRecurso,
                                        LogradouroRecurso = exclusao.LogradouroRecurso,
                                        NumeroRecurso = exclusao.NumeroRecurso,
                                        Cidade = new CidadeRecursoEntity { CidadeRecurso = exclusao.CidadeRecurso },
                                        Estado = new EstadoRecursoEntity { EstadoRecurso = exclusao.EstadoRecurso }
                                    },
                                    TelefoneRecurso = new TelefoneRecursoEntity
                                    {
                                        NumeroTelefoneRecurso = exclusao.NumeroTelefoneRecurso,
                                        TipoTelefoneRecurso = exclusao.TipoTelefoneRecurso
                                    },
                                    EspecialidadeRecurso = EspecialidadesExclusao(exclusao.EspecialidadesRecurso),
                                    QualificacaoRecurso = new QualificacoesRecursoEntity
                                    {
                                        Qualificacao = QualificacoesExclusao(exclusao.QualificacoesRecurso)
                                    }
                                }
                            };

                            var taskNovoRecurso = new List<Task>
                            {
                                Task.Run(async () =>
                                {
                                    recursos.NovoRecurso.PlanoNovoRecurso = new PlanosNovoRecursoEntity();
                                    recursos.NovoRecurso.PlanoNovoRecurso.Plano = await GetPlanosNovoRecursoAsync(substituicao.CodigoNovoRecurso);
                                    return Task.CompletedTask;
                                })
                            }.ToArray();

                            Task.WaitAll(taskNovoRecurso);

                            var tasksRecurso = new List<Task>
                            {                                
                                Task.Run(async () =>
                                {
                                    recursos.Recurso.PlanoRecurso = new PlanosRecursoEntity();
                                    recursos.Recurso.PlanoRecurso.Plano = await GetPlanosRecursoAsync(exclusao.CodigoRecurso);
                                    return Task.CompletedTask;
                                })
                            }.ToArray();

                            Task.WaitAll(tasksRecurso);

                            resultado.Recursos.Add(recursos);
                            planoDictionary.Add(_substituicao.CodigoNovoRecurso, _substituicao);
                        }
                        return _substituicao;
                    },
                    splitOn: "CodigoRecurso",
                    param: parameters
                );
            return resultado;
        }

        public async Task<ResultadoEntity> GetExclusoesAsync(BaseFilterConsultaDTO request)
        {
            var (query, parameters) = MontaQuery(nameof(GetExclusoesAsync), request);
            var resultado = new ResultadoEntity { Recursos = new List<RecursosEntity>() };
            var planoDictionary = new Dictionary<string, ConsultaSubstituicaoDTO>();

            var retorno = await _baseData.DbConnection.QueryAsync<ConsultaSubstituicaoDTO, ExclusaoDTO, ConsultaSubstituicaoDTO>(
                    query,
                    map: (substituicao, exclusao) =>
                    {
                        if (!planoDictionary.TryGetValue(exclusao.CodigoRecurso, out ConsultaSubstituicaoDTO _substituicao))
                        {
                            _substituicao = substituicao;
                            var recursos = new RecursosEntity
                            {
                                DataExclusao = exclusao.DataExclusao,
                                Recurso = new RecursoEntity
                                {
                                    CnpjRecurso = FormatCNPJ(exclusao.CnpjRecurso),
                                    CodigoRecurso = exclusao.CodigoRecurso,
                                    NomeRecurso = exclusao.NomeRecurso,
                                    RazaoSocialRecurso = exclusao.RazaoSocialRecurso,
                                    TipoAtendimentoRecurso = exclusao.TipoAtendimentoRecurso,
                                    UnimedRecurso = exclusao.UnimedRecurso,
                                    DataExclusaoSolicitada = exclusao.DataExclusaoSolicitada,
                                    EnderecoRecurso = new EnderecoRecursoEntity
                                    {
                                        BairroRecurso = exclusao.BairroRecurso,
                                        CepRecurso = exclusao.CepRecurso,
                                        ComplementoRecurso = exclusao.ComplementoRecurso,
                                        LogradouroRecurso = exclusao.LogradouroRecurso,
                                        NumeroRecurso = exclusao.NumeroRecurso,
                                        Cidade = new CidadeRecursoEntity { CidadeRecurso = exclusao.CidadeRecurso },
                                        Estado = new EstadoRecursoEntity { EstadoRecurso = exclusao.EstadoRecurso }
                                    },
                                    TelefoneRecurso = new TelefoneRecursoEntity
                                    {
                                        NumeroTelefoneRecurso = exclusao.NumeroTelefoneRecurso,
                                        TipoTelefoneRecurso = exclusao.TipoTelefoneRecurso
                                    },
                                    EspecialidadeRecurso = EspecialidadesExclusao(exclusao.EspecialidadesRecurso),
                                    QualificacaoRecurso = new QualificacoesRecursoEntity
                                    {
                                        Qualificacao = QualificacoesExclusao(exclusao.QualificacoesRecurso)
                                    }

                                }
                            };

                            var tasks = new List<Task>
                            {
                                Task.Run(async () =>
                                {
                                    recursos.Recurso.PlanoRecurso = new PlanosRecursoEntity();
                                    recursos.Recurso.PlanoRecurso.Plano = await GetPlanosRecursoAsync(exclusao.CodigoRecurso);
                                    return Task.CompletedTask;
                                })
                            }.ToArray();

                            Task.WaitAll(tasks);

                            resultado.Recursos.Add(recursos);
                            planoDictionary.Add(exclusao.CodigoRecurso, _substituicao);
                        }
                        return _substituicao;
                    },
                    splitOn: "DataExclusao",
                    param: parameters
                );

            return resultado;
        }

        public static IEnumerable<EspecialidadesNovoRecursoEntity> EspecialidadesSubstituicao(string especialidades)
        {
            if (string.IsNullOrEmpty(especialidades))
                return null;

            IList<EspecialidadesNovoRecursoEntity> especialidadesRetorno = new List<EspecialidadesNovoRecursoEntity>();
            EspecialidadesNovoRecursoEntity especialidade;
            if (!string.IsNullOrEmpty(especialidades))
            {
                string[] especialidadesConsulta = especialidades.Split('#');
                foreach (var espec in especialidadesConsulta)
                {
                    especialidade = new EspecialidadesNovoRecursoEntity
                    {
                        Nome = espec
                    };
                    especialidadesRetorno.Add(especialidade);
                }
            }

            return especialidadesRetorno;
        }

        public static IEnumerable<QualificacaoNovoRecursoEntity> QualificacoesSubstituicao(string qualificacoes)
        {
            if (string.IsNullOrEmpty(qualificacoes))
                return null;

            IList<QualificacaoNovoRecursoEntity> qualificacoesRetorno = new List<QualificacaoNovoRecursoEntity>();
            QualificacaoNovoRecursoEntity qualificacao;
            if (!string.IsNullOrEmpty(qualificacoes))
            {
                string[] qualificacoesConsulta = qualificacoes.Split('#');
                foreach (var qualif in qualificacoesConsulta)
                {
                    string[] qualificacaoConsulta = qualif.Split('@');
                    qualificacao = new QualificacaoNovoRecursoEntity();

                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                            qualificacao.Codigo = qualificacaoConsulta[0];
                        else if (i == 1)
                            qualificacao.Nome = qualificacaoConsulta[1];
                        else
                        {
                            if (string.IsNullOrEmpty(qualificacaoConsulta[2]))
                                qualificacao.Habilitado = "true";
                            else
                                qualificacao.Habilitado = "false";
                        }
                    }
                    qualificacoesRetorno.Add(qualificacao);
                }
            }

            return qualificacoesRetorno;
        }

        public static IEnumerable<EspecialidadesRecursoEntity> EspecialidadesExclusao(string especialidades)
        {
            if (string.IsNullOrEmpty(especialidades))
                return null;

            IList<EspecialidadesRecursoEntity> especialidadesRetorno = new List<EspecialidadesRecursoEntity>();
            EspecialidadesRecursoEntity especialidade;
            if (!string.IsNullOrEmpty(especialidades))
            {
                string[] especialidadesConsulta = especialidades.Split('#');
                foreach (var espec in especialidadesConsulta)
                {
                    especialidade = new EspecialidadesRecursoEntity
                    {
                        Nome = espec
                    };
                    especialidadesRetorno.Add(especialidade);
                }
            }

            return especialidadesRetorno;
        }

        public static IEnumerable<QualificacaoRecursoEntity> QualificacoesExclusao(string qualificacoes)
        {
            if (string.IsNullOrEmpty(qualificacoes))
                return null;

            IList<QualificacaoRecursoEntity> qualificacoesRetorno = new List<QualificacaoRecursoEntity>();
            QualificacaoRecursoEntity qualificacao;
            if (!string.IsNullOrEmpty(qualificacoes))
            {
                string[] qualificacoesConsulta = qualificacoes.Split('#');
                foreach (var qualif in qualificacoesConsulta)
                {
                    string[] qualificacaoConsulta = qualif.Split('@');
                    qualificacao = new QualificacaoRecursoEntity();

                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                            qualificacao.Codigo = qualificacaoConsulta[0];
                        else if (i == 1)
                            qualificacao.Nome = qualificacaoConsulta[1];
                        else
                        {
                            if (string.IsNullOrEmpty(qualificacaoConsulta[2]))
                                qualificacao.Habilitado = "true";
                            else
                                qualificacao.Habilitado = "false";
                        }
                    }
                    qualificacoesRetorno.Add(qualificacao);
                }
            }

            return qualificacoesRetorno;
        }

        public async Task<IEnumerable<PlanoNovoRecursoEntity>> GetPlanosNovoRecursoAsync(string codigoNovoRecurso)
        {
            const string query = @"SELECT distinct
                                          pm.cod_plano AS codigo
                                         ,pm.nome_plano AS nome
                                         ,pm.cod_registro_ans AS numeroregistroans
                                         ,(CASE pm.cod_produto
                                             WHEN '1' THEN
                                              'Individual'
                                             WHEN '2' THEN
                                              'Coletivo empresarial'
                                             ELSE
                                              'Coletivo por Adesão'
                                          END) AS classificacao
                                         ,(CASE pm.ind_situacao
                                             WHEN 'A' THEN
                                              'Ativo'
                                             ELSE
                                              'Encerrado'
                                          END) AS situacao
                                         ,pm.cod_plano AS codigotopsaude
                                     FROM prestador_subst_plano_hist plano_hist
                                         ,plano_medico               pm
                                    WHERE to_char(plano_hist.cod_plano) = pm.cod_plano
                                      AND plano_hist.cod_sequencia_substituicao IN
                                          (SELECT hist.cod_sequencia_substituicao
                                             FROM prestador_substituicao_hist hist
                                                 ,prestador_servico           prest_exc
                                                 ,prestador_servico           prest_subs
                                            WHERE hist.cod_prestador_ts_excluido = prest_exc.cod_prestador_ts
                                              AND hist.cod_prestador_ts_substituto =
                                                  prest_subs.cod_prestador_ts
                                              AND prest_subs.cod_prestador = :codigoNovoRecurso)";

            return await _baseData.DbConnection.QueryAsync<PlanoNovoRecursoEntity>(query, new { codigoNovoRecurso });
        }

        public async Task<IEnumerable<PlanoRecursoEntity>> GetPlanosRecursoAsync(string codigoRecurso)
        {
            const string query = @"SELECT pm.cod_plano AS codigo
                                         ,pm.nome_plano AS nome
                                         ,pm.cod_registro_ans AS numeroregistroans
                                         ,(CASE pm.cod_produto
                                             WHEN '1' THEN
                                              'Individual'
                                             WHEN '2' THEN
                                              'Coletivo empresarial'
                                             ELSE
                                              'Coletivo por Adesão'
                                          END) AS classificacao
                                         ,(CASE pm.ind_situacao
                                             WHEN 'A' THEN
                                              'Ativo'
                                             ELSE
                                              'Encerrado'
                                          END) AS situacao
                                         ,pm.cod_plano AS codigotopsaude
                                     FROM prestador_servico      ps
                                         ,prestador_operadora    po
                                         ,rede_prestador         rp
                                         ,plano_rede_atendimento pra
                                         ,plano_medico           pm
                                    WHERE 1 = 1
                                      AND ps.cod_prestador = :codigoRecurso
                                      AND rp.ind_divulgacao_rede = 'S'
                                      AND po.cod_prestador_ts = ps.cod_prestador_ts
                                      AND rp.cod_prestador_ts = ps.cod_prestador_ts
                                      AND pra.cod_rede = rp.cod_rede
                                      AND pra.cod_plano = pm.cod_plano";

            return await _baseData.DbConnection.QueryAsync<PlanoRecursoEntity>(query, new { codigoRecurso });
        }

        public static string FormatCNPJ(string CNPJ)
        {
            if (string.IsNullOrEmpty(CNPJ) || CNPJ.Length > 14)
                return CNPJ;

            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

        protected (string, DynamicParameters) MontaQuery(string metodo, BaseFilterConsultaDTO request)
        {
            var query = new StringBuilder();
            var parameters = new DynamicParameters();

            query.Append(@"select  distinct  ");
            if (metodo == "GetRecursosAsync")
                query.Append(@"              psn.cod_prestador
                                            ,to_char(pe.dt_exclusao,'YYYY-MM-DD')         DataExclusao
                                            --,to_char(psh.dt_inicio_servico,'YYYY-MM-DD')  DataInclusaoNovo
                                            , to_char(PON.DT_CONTRATUALIZACAO,'YYYY-MM-DD')  DataInclusaoNovo
                                            , pm.cod_registro_ans                         CodRegistroAns
                                             -- novo recurso                              
                                            ,psn.cod_prestador                            CodigoNovoRecurso
                                            ,'SEGUROS UNIMED'                             UnimedNovoRecurso
                                            ,psn.num_crm                                  CrmNovoRecurso
                                            ,esn.num_cgc                                  CnpjNovoRecurso
                                            ,esn.nome_razao_social                        RazaoSocialNovoRecurso
                                            ,tpn.nome_tipo_prestador                      TipoAtendimentoNovoRecurso
                                            ,psn.nome_prestador                           NomeNovoRecurso
                                            ,epn.num_cep                                  CepNovoRecurso
                                            ,epn.end_prestador                            LogradouroNovoRecurso
                                            ,epn.num_endereco                             NumeroNovoRecurso
                                            ,epn.txt_complemento                          ComplementoNovoRecurso
                                            ,epn.nome_bairro                              BairroNovoRecurso 
                                            ,mn.nom_municipio                             CidadeNovoRecurso
                                            ,mn.sgl_uf                                    EstadoNovoRecurso
                                                                                          
                                            ,'FIXO'                                       TipoTelefoneNovoRecurso
                                            ,(SELECT CASE nvl(end_pres.num_telefone_1, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   end_pres.num_ddd_telefone_1 || ' ' || end_pres.num_telefone_1
                                               END || CASE nvl(end_pres.num_telefone_2, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_2
                                               END || CASE nvl(end_pres.num_telefone_3, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_3
                                               END
                                          FROM endereco_prestador end_pres
                                         WHERE end_pres.cod_prestador_ts = epn.cod_prestador_ts
                                           AND ROWNUM  < 2) NumeroTelefoneNovoRecurso
                                            ,(select distinct listagg(iq.cod_item || '@' || q.nom_legenda || '@' || iq.dt_qualifica_fim, '#')
                                                     within group (order by null) over (partition by null) qualifica
                                              from   item_qualifica_prs iq
                                                    ,item_qualifica     q
                                              where  iq.cod_item        = q.cod_item
                                              and    iq.cod_prestador_ts = psn.cod_prestador_ts
                                              ) as QualificacoesNovoRecurso
                                            ,(SELECT distinct listagg(esp.nome_especialidade, '#')
                                                     within group (order by null) over (partition by null) especialidade
                                              from   especialidade          esp 
                                                    ,habilitacao_rede       hab  
                                                    ,habilitacao_prestador  habp
                                              where  habp.cod_prestador_ts  = psn.cod_prestador_ts
                                              and    hab.cod_prestador_ts   = habp.cod_prestador_ts
                                              and    hab.ind_divulgacao_hab = 'S'
                                              and    habp.ind_divulgacao_hab = 'S'
                                              and    esp.cod_especialidade  = hab.cod_especialidade
                                              and    hab.cod_especialidade  = hab.item_medico
                                              and    habp.cod_especialidade = habp.item_medico
                                              group by esp.nome_especialidade
                                              ) as EspecialidadesNovoRecurso
                                            -- recurso
                                            , ps.cod_prestador                         CodigoRecurso                                          
                                            , es.num_cgc                               CnpjRecurso
                                            , es.nome_razao_social                     RazaoSocialRecurso
                                            , 'SEGUROS UNIMED'                         UnimedRecurso
                                            , tp.nome_tipo_prestador                   TipoAtendimentoRecurso
                                            , ps.nome_prestador                        NomeRecurso
                                            , ep.num_cep                               CepRecurso
                                            , ep.end_prestador                         LogradouroRecurso
                                            , ep.num_endereco                          NumeroRecurso
                                            , ep.txt_complemento                       ComplementoRecurso
                                            , ep.nome_bairro                           BairroRecurso
                                            , m.nom_municipio                          CidadeRecurso
                                            , m.sgl_uf                                 EstadoRecurso
                                            , 'FIXO'                                   TipoTelefoneRecurso
                                            , to_char(po.dt_exclusao_solicitada,'YYYY-MM-DD') DataExclusaoSolicitada
                                            , (SELECT CASE nvl(end_pres.num_telefone_1, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   end_pres.num_ddd_telefone_1 || ' ' || end_pres.num_telefone_1
                                               END || CASE nvl(end_pres.num_telefone_2, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_2
                                               END || CASE nvl(end_pres.num_telefone_3, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_3
                                               END
                                          FROM endereco_prestador end_pres
                                         WHERE end_pres.cod_prestador_ts = po.cod_prestador_ts
                                           AND ROWNUM < 2) NumeroTelefoneRecurso
                                            ,(select distinct listagg(iq.cod_item || '@' || q.nom_legenda || '@' || iq.dt_qualifica_fim, '#')
                                                     within group (order by null) over (partition by null) qualifica
                                              from   item_qualifica_prs iq
                                                    ,item_qualifica     q
                                              where  iq.cod_item        = q.cod_item
                                              and    iq.cod_prestador_ts = po.cod_prestador_ts
                                              ) as QualificacoesRecurso
                                            ,(SELECT distinct listagg(esp.nome_especialidade, '#')
                                                     within group (order by null) over (partition by null) especialidade
                                              from   especialidade          esp 
                                                    ,habilitacao_rede       hab  
                                                    ,habilitacao_prestador  habp
                                              where  habp.cod_prestador_ts  = po.cod_prestador_ts
                                              and    hab.cod_prestador_ts   = habp.cod_prestador_ts
                                              and    hab.ind_divulgacao_hab = 'S'
                                              and    habp.ind_divulgacao_hab = 'S'
                                              and    esp.cod_especialidade  = hab.cod_especialidade
                                              and    hab.cod_especialidade  = hab.item_medico
                                              and    habp.cod_especialidade = habp.item_medico
                                              group by esp.nome_especialidade
                                              ) as EspecialidadesRecurso
                                    from    prestador_servico   ps
                                           ,prestador_operadora po
                                           ,prestador_operadora PON
                                           ,prestador_exclusao  pe
                                           ,prestador_substituicao_hist psh
                                           ,endereco_prestador          ep
                                           ,municipio                   m
                                           ,entidade_sistema            es
                                           ,prestador_tipo              pt
                                           ,tipo_prestador              tp
                                           ,prestador_servico           psn
                                           ,endereco_prestador          epn
                                           ,municipio                   mn
                                           ,entidade_sistema            esn
                                           ,prestador_tipo              ptn
                                           ,tipo_prestador              tpn
                                           ,plano_rede_atendimento      pra
                                           ,plano_medico                pm
                                    where   po.cod_prestador_ts           = ps.cod_prestador_ts
                                    and     pe.cod_prestador_ts           = po.cod_prestador_ts
                                    and     PON.cod_prestador_ts          = psn.cod_prestador_ts
                                    and     psh.cod_prestador_ts_excluido = po.cod_prestador_ts
                                    --and     po.ind_situacao               = 'E'
                                    and     es.cod_entidade_ts            = ps.cod_entidade_ts
                                    and     ep.cod_prestador_ts           = po.cod_prestador_ts
                                    and     m.cod_municipio               = ep.cod_municipio
                                    and     pt.cod_prestador_ts           = po.cod_prestador_ts
                                    and     pt.ind_principal              = 'S'
                                    and     tp.cod_tipo_prestador         = pt.cod_tipo_prestador
                                    and     pra.cod_rede                  in (select cod_rede
                                                                              from   rede_prestador rp
                                                                              where  rp.cod_prestador_ts = ps.cod_prestador_ts)
                                    and     pra.cod_plano                 = pm.cod_plano                                   
                                    --novo recurso
                                    and     psn.cod_prestador_ts          = psh.cod_prestador_ts_substituto
                                    and     epn.cod_prestador_ts          = psh.cod_prestador_ts_substituto
                                    and     epn.cod_municipio             = ep.cod_municipio
                                    and     mn.cod_municipio              = epn.cod_municipio
                                    and     esn.cod_entidade_ts           = psn.cod_entidade_ts
                                    and     ptn.cod_prestador_ts          = psn.cod_prestador_ts
                                    and     ptn.ind_principal             = 'S'
                                    and     tpn.cod_tipo_prestador        = ptn.cod_tipo_prestador 
                                    and     pe.dt_exclusao >= add_months(sysdate, - 6)
                                    and     po.ind_vinculacao in ('1','13')");
            if (metodo == "GetExclusoesAsync")
                query.Append(@"               ps.cod_prestador
                                            , to_char(pe.dt_exclusao,'YYYY-MM-DD')     DataExclusao 
                                            , ps.cod_prestador                         CodigoRecurso
                                            , es.num_cgc                               CnpjRecurso
                                            , es.nome_razao_social                     RazaoSocialRecurso
                                            , 'SEGUROS UNIMED'                         UnimedRecurso
                                            , tp.nome_tipo_prestador                   TipoAtendimentoRecurso
                                            , ps.nome_prestador                        NomeRecurso
                                            , ep.num_cep                               CepRecurso
                                            , ep.end_prestador                         LogradouroRecurso
                                            , ep.num_endereco                          NumeroRecurso
                                            , ep.txt_complemento                       ComplementoRecurso
                                            , ep.nome_bairro                           BairroRecurso
                                            , m.nom_municipio                          CidadeRecurso
                                            , m.sgl_uf                                 EstadoRecurso
                                            , 'FIXO'                                   TipoTelefoneRecurso
                                            , to_char(po.dt_exclusao_solicitada,'YYYY-MM-DD') DataExclusaoSolicitada
                                            , (SELECT CASE nvl(end_pres.num_telefone_1, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   end_pres.num_ddd_telefone_1 || ' ' || end_pres.num_telefone_1
                                               END || CASE nvl(end_pres.num_telefone_2, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_2
                                               END || CASE nvl(end_pres.num_telefone_3, 'X')
                                                  WHEN 'X' THEN
                                                   NULL
                                                  ELSE
                                                   ' / ' || end_pres.num_telefone_3
                                               END
                                          FROM endereco_prestador end_pres
                                         WHERE end_pres.cod_prestador_ts = po.cod_prestador_ts
                                           AND ROWNUM < 2) NumeroTelefoneRecurso
                                            ,(select distinct listagg(iq.cod_item || '@' || q.nom_legenda || '@' || iq.dt_qualifica_fim, '#')
                                                     within group (order by null) over (partition by null) qualifica
                                              from   item_qualifica_prs iq
                                                    ,item_qualifica     q
                                              where  iq.cod_item        = q.cod_item
                                              and    iq.cod_prestador_ts = po.cod_prestador_ts
                                              ) as QualificacoesRecurso
                                            ,(SELECT distinct listagg(esp.nome_especialidade, '#')
                                                     within group (order by null) over (partition by null) especialidade
                                              from   especialidade          esp 
                                                    ,habilitacao_rede       hab  
                                                    ,habilitacao_prestador  habp
                                              where  habp.cod_prestador_ts  = po.cod_prestador_ts
                                              and    hab.cod_prestador_ts   = habp.cod_prestador_ts
                                              and    hab.ind_divulgacao_hab = 'S'
                                              and    habp.ind_divulgacao_hab = 'S'
                                              and    esp.cod_especialidade  = hab.cod_especialidade
                                              and    hab.cod_especialidade  = hab.item_medico
                                              and    habp.cod_especialidade = habp.item_medico
                                              group by esp.nome_especialidade
                                              ) as EspecialidadesRecurso
                                          FROM prestador_servico           ps
                                              ,prestador_operadora         po
                                              ,prestador_exclusao          pe
                                              ,endereco_prestador          ep
                                              ,municipio                   m
                                              ,entidade_sistema            es
                                              ,prestador_tipo              pt
                                              ,tipo_prestador              tp
                                              ,plano_rede_atendimento      pra
                                              ,plano_medico                pm
                                         WHERE po.cod_prestador_ts = ps.cod_prestador_ts
                                           AND pe.cod_prestador_ts = po.cod_prestador_ts
                                           --AND po.ind_situacao = 'E'
                                           AND es.cod_entidade_ts = ps.cod_entidade_ts
                                           AND ep.cod_prestador_ts = po.cod_prestador_ts
                                           AND m.cod_municipio = ep.cod_municipio
                                           AND pt.cod_prestador_ts = po.cod_prestador_ts
                                           AND pt.ind_principal = 'S'
                                           AND tp.cod_tipo_prestador = pt.cod_tipo_prestador
                                           AND pra.cod_rede IN
                                               (SELECT cod_rede
                                                  FROM rede_prestador rp
                                                 WHERE rp.cod_prestador_ts = ps.cod_prestador_ts)
                                           AND pra.cod_plano = pm.cod_plano
                                           AND pe.dt_exclusao >= add_months(sysdate, - 6)
                                           AND po.ind_vinculacao in ('1','13')
                                ");

            if (!string.IsNullOrEmpty(request.CodigoPlano))
            {
                query.AppendLine(" and pm.cod_plano = :cod_plano");
                parameters.Add("cod_plano", request.CodigoPlano);
            }
            if (!string.IsNullOrEmpty(request.RegistroANS))
            {
                query.AppendLine(" and pm.cod_registro_ans = :registro_ans");
                parameters.Add("registro_ans", request.RegistroANS);
            }
            if (!string.IsNullOrEmpty(request.Estado))
            {
                query.AppendLine(" and m.sgl_uf = :estado");
                parameters.Add("estado", request.Estado);
            }
            if (!string.IsNullOrEmpty(request.Cidade))
            {
                query.AppendLine(" and ep.cod_municipio = (select cod_municipio from municipio where upper(nom_municipio) = upper(:cidade) and upper(sgl_uf) = upper(:estadoCidade))");
                parameters.Add("cidade", request.Cidade);
                parameters.Add("estadoCidade", request.Estado);
            }
            if (!string.IsNullOrEmpty(request.Filtro))
            {
                query.AppendLine(" and upper(ps.nome_prestador) like :filtro");
                parameters.Add("filtro", $"%{request.Filtro.ToUpper()}%");
            }

            query.AppendLine(" order by DataExclusao desc, NomeRecurso asc");            

            return (query.ToString(), parameters);
        }

    }
}
