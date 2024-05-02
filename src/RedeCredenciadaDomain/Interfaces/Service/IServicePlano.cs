using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities.Plano;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Service
{
    public interface IServicePlano : IServiceBase<PlanoEntity>
    {
        Task<IEnumerable<PlanoEntity>> GetPlanosAsync(BaseFilterConsultaDTO request);
    }
}
