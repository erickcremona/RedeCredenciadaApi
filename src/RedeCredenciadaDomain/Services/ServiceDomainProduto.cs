using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.Entities.Produto;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Services
{
    public class ServiceDomainProduto : ServiceDomainBase<ProdutoEntity>, IServiceProduto
    {
        private readonly IRepositoryProduto _repositoryProduto;
        public ServiceDomainProduto(IRepositoryProduto repositoryProduto,
                                              INotification notification,
                                    ILogger<ServiceDomainProduto> logger)
                          : base(repositoryProduto, notification, logger)
                               => _repositoryProduto = repositoryProduto;

        public async Task<IEnumerable<ProdutoEntity>> GetProdutosAsync()
                         => await _repositoryProduto.GetProdutosAsync();
    }
}
