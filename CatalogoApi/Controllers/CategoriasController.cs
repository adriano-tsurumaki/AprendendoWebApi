using CatalogoApi.Context;
using CatalogoApi.Models;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasComProdutos()
        {
            try
            {
                return _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias com produtos");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _uof.CategoriaRepository.Get().ToList();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

                return categoria is null ? 
                    NotFound($"A categoria com id = {id} não foi encontrada!") : 
                    categoria;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter a categoria");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar uma nova categoria");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId) return BadRequest($"Não foi possível atualizar a categoria com id = {id}");

                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar a categoria com id = {id}");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

                if (categoria is null) return NotFound($"A categoria com id = {id} não foi encontrada");

                _uof.CategoriaRepository.Delete(categoria);
                _uof.Commit();

                return categoria;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir a categoria com id = {id}");
            }
        }
    }
}