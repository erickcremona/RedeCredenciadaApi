using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeCredenciadaApi.ViewModels.Produto;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RedeCredenciadaApi.Controllers.V1
{
    [Produces("application/json", "application/xml")]
    [Route("api/v{version:apiVersion}/movimentacao-rede-credenciada/produto")]
    [ApiVersion("1.0")]
    public class ProdutoController : MainController
    {
        private readonly IRepositoryProduto _repositoryroduto;
        private readonly IMapper _mapper;
        private readonly ILogger<ProdutoController> _logger;
        public ProdutoController(INotification notification,
                        IRepositoryProduto repositoryroduto,
                                             IMapper mapper,
                          ILogger<ProdutoController> logger)
                                        : base(notification)
        {
            _repositoryroduto = repositoryroduto;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Get()
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                _logger.LogDebug($"[{nameof(ProdutoController)}] inicializando método {nameof(Get)} - Data/Hora -> {DateTime.Now}");

                var resultado = new Resultado();
                resultado.Produtos.Produto = _mapper.Map<IEnumerable<ProdutoViewModelResponse>>(await _repositoryroduto.GetProdutosAsync());
                return CustomResponse(resultado);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                _logger.LogError(ex, $"[{nameof(ProdutoController)}] Error - {ex.GetBaseException().Message}");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug($"[{nameof(ProdutoController)}] finalizando método {nameof(Get)} - Tempo total -> {stopwatch.ElapsedMilliseconds} ms");
            }

            return CustomResponse();
        }
    }
}
