using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeCredenciadaApi.ViewModels.Substituicao;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Interfaces.Service;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RedeCredenciadaApi.Controllers.V1
{
    [Produces("application/json", "application/xml")]
    [Route("api/v{version:apiVersion}/movimentacao-rede-credenciada/recursos-exclusao-rede/1")]
    [ApiVersion("1.0")]
    public class ExclusaoController : MainController
    {
        private readonly IServiceRecursos _serviceRecursos;
        private readonly IMapper _mapper;
        private readonly ILogger<ExclusaoController> _logger;
        public ExclusaoController(INotification notification,
                            IServiceRecursos serviceRecursos,
                                              IMapper mapper,
                          ILogger<ExclusaoController> logger)
                                         : base(notification)
        {
            _serviceRecursos = serviceRecursos;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Get(string codPlano,
                                              string estado,
                                              string cidade,
                                              string filtro,
                                         string registroANS)
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                _logger.LogDebug($"[{nameof(ExclusaoController)}] inicializando método {nameof(Get)} - Data/Hora -> {DateTime.Now}");

                var consulta = new BaseFilterConsultaDTO
                {
                    CodigoPlano = codPlano,
                    Estado = estado,
                    Cidade = cidade,
                    Filtro = filtro,
                    RegistroANS = registroANS
                };

                return CustomResponse(_mapper.Map<ResultadoViewModel>(await _serviceRecursos.GetExclusoesAsync(consulta)));
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                _logger.LogError(ex, $"[{nameof(ExclusaoController)}] Error - {ex.GetBaseException().Message}");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug($"[{nameof(ExclusaoController)}] finalizando método {nameof(Get)} - Tempo total -> {stopwatch.ElapsedMilliseconds} ms");
            }
            return CustomResponse();
        }
    }
}
