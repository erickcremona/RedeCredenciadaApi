using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using RedeCredenciadaDomain.Validations;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Services
{
    public class ServiceDomainRecursos : ServiceDomainBase<RecursosEntity>, IServiceRecursos
    {
        private readonly IRepositoryRecursos _repositoryRecursos;
        public ServiceDomainRecursos(IRepositoryRecursos repositoryRecursos,
                                                 INotification notification,
                                      ILogger<ServiceDomainRecursos> logger)
                             : base(repositoryRecursos, notification, logger)
                                => _repositoryRecursos = repositoryRecursos;
        public async Task<ResultadoEntity> GetRecursosAsync(BaseFilterConsultaDTO request)
        {
            if (!RunValidation(new SubstituicaoValidation(), request))
                return null;

            return await _repositoryRecursos.GetRecursosAsync(request);
        }
        public async Task<ResultadoEntity> GetExclusoesAsync(BaseFilterConsultaDTO request)
        {
            if (!RunValidation(new ExclusaoValidation(), request))
                return null;

            return await _repositoryRecursos.GetExclusoesAsync(request);
        }
    }
}
