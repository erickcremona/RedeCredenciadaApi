using RedeCredenciadaDomain.DTOs;
using RedeCredenciadaDomain.Entities;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Repositoty
{
    public interface IRepositoryRecursos : IRepositoryBase<RecursosEntity>
    {
        Task<ResultadoEntity> GetRecursosAsync(BaseFilterConsultaDTO request);
        Task<ResultadoEntity> GetExclusoesAsync(BaseFilterConsultaDTO request);
    }
}
