using CatalogoApi.Models;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                return _uof.ProdutoRepository.Get().ToList();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar obter os produtos");
            }
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                return produto is null ? NotFound($"O produto com id = {id} não foi encontrado") : produto;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar obter o produto");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            try
            {
                _uof.ProdutoRepository.Add(produto);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar criar um novo produto");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId) return BadRequest($"Não foi possível atualizar o produto com id = {id}");

                _uof.ProdutoRepository.Update(produto);
                _uof.Commit();

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o produto com id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto is null) return NotFound($"O produto com id = {id} não foi encontrado");

                _uof.ProdutoRepository.Delete(produto);
                _uof.Commit();

                return produto;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar excluir o produto com id = {id}");
            }
        }
    }
}