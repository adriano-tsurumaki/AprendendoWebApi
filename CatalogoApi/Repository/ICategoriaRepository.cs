using CatalogoApi.Models;
using System.Collections.Generic;

namespace CatalogoApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}