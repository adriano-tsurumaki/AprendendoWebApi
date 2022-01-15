using CatalogoApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { }

        public AppDbContext()
        { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos{ get; set; }
    }
}