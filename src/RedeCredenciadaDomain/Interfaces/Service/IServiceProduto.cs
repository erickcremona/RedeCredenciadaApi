using RedeCredenciadaDomain.Entities.Produto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Service
{
    public interface IServiceProduto : IServiceBase<ProdutoEntity>
    {
        Task<IEnumerable<ProdutoEntity>> GetProdutosAsync();
    }
}
