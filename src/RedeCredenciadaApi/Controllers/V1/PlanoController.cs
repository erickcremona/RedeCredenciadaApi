using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeCredenciadaApi.ViewModels.Plano;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RedeCredenciadaApi.Controllers.V1
{
    [Produces("application/json", "application/xml")]
    [Route("api/v{version:apiVersion}/movimentacao-rede-credenciada/planos")]
    [ApiVersion("1.0")]
    public class PlanoController : MainController
    {
        private readonly ILogger<PlanoController> _logger;
        private readonly IRepositoryPlano _repositoryPlano;
        private readonly IMapper _mapper;

        public PlanoController(ILogger<PlanoController> logger,
                                    INotification notification,
                                                IMapper mapper,
                              IRepositoryPlano repositoryPlano)
                                            : base(notification)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryPlano = repositoryPlano;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Get(string codigoProduto)
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                _logger.LogDebug($"[{nameof(PlanoController)}] inicializando método {nameof(Get)} - Data/Hora -> {DateTime.Now}");

                if (codigoProduto == null)
                {
                    NotifyError("Código do Produto não informado.");
                    _logger.LogError($"[{nameof(PlanoController)}] erro de parâmetro método {nameof(Get)} - Código do Produto não informado.");
                    return CustomResponse();
                }

                var consulta = new BaseFilterConsultaDTO
                {
                    CodigoProduto = codigoProduto
                };

                var resultado = new Resultado();
                resultado.Planos.Plano = _mapper.Map<IEnumerable<PlanoViewModelResponse>>(await _repositoryPlano.GetPlanosAsync(consulta));

                return CustomResponse(resultado);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                _logger.LogError(ex, $"[{nameof(PlanoController)}] Error - {ex.GetBaseException().Message}");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug($"[{nameof(PlanoController)}] finalizando método {nameof(Get)} - Tempo total -> {stopwatch.ElapsedMilliseconds} ms");
            }
            return CustomResponse();
        }
    }
}
