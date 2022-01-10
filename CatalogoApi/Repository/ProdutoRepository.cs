using CatalogoApi.Context;
using CatalogoApi.Models;
using CatalogoApi.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace CatalogoApi.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {

        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(
                Get().OrderBy(on => on.ProdutoId), 
                produtosParameters.PageNumber, 
                produtosParameters.PageSize);
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}