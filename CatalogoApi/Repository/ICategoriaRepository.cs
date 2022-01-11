using CatalogoApi.Models;
using CatalogoApi.Pagination;
using System.Collections.Generic;

namespace CatalogoApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriaParameters);
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}