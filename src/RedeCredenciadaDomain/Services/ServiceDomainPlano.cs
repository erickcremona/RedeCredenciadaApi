using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities.Plano;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Services
{
    public class ServiceDomainPlano : ServiceDomainBase<PlanoEntity>, IServicePlano
    {
        private readonly IRepositoryPlano _repositotyPlano;
        public ServiceDomainPlano(IRepositoryPlano repositotyPlano,
                                        INotification notification,
                                ILogger<ServiceDomainPlano> logger)
                      : base(repositotyPlano, notification, logger)
                             => _repositotyPlano = repositotyPlano;

        public async Task<IEnumerable<PlanoEntity>> GetPlanosAsync(BaseFilterConsultaDTO request)
                                                => await _repositotyPlano.GetPlanosAsync(request);
    }
}
