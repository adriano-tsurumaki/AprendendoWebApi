using CatalogoApi.Models;
using System.Collections.Generic;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}