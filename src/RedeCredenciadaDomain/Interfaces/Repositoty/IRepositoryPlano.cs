using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities.Plano;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Repositoty
{
    public interface IRepositoryPlano : IRepositoryBase<PlanoEntity>
    {
        Task<IEnumerable<PlanoEntity>> GetPlanosAsync(BaseFilterConsultaDTO request);
    }
}
