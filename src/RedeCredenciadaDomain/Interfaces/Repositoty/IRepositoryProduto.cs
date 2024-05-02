using RedeCredenciadaDomain.Entities.Produto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeCredenciadaDomain.Interfaces.Repositoty
{
    public interface IRepositoryProduto : IRepositoryBase<ProdutoEntity>
    {
        Task<IEnumerable<ProdutoEntity>> GetProdutosAsync();
    }
}