using CatalogoApi.Models;
using CatalogoApi.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriaParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}