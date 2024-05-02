using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Service
{
    public interface IServiceRecursos : IServiceBase<RecursosEntity>
    {
        Task<ResultadoEntity> GetRecursosAsync(BaseFilterConsultaDTO request);
        Task<ResultadoEntity> GetExclusoesAsync(BaseFilterConsultaDTO request);
    }
}
