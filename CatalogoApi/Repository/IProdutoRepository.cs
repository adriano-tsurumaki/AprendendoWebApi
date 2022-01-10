using CatalogoApi.Models;
using CatalogoApi.Pagination;
using System.Collections.Generic;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}