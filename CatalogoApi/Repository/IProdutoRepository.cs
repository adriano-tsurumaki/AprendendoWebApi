using CatalogoApi.Models;
using CatalogoApi.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}