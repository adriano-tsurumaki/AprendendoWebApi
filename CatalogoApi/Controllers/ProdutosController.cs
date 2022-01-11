using AutoMapper;
using CatalogoApi.DTOs;
using CatalogoApi.Models;
using CatalogoApi.Pagination;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPrecos()
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosPorPreco();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            try
            {
                var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);
                var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

                var metadata = new
                {
                    produtos.TotalCount,
                    produtos.PageSize,
                    produtos.CurrentPage,
                    produtos.TotalPages,
                    produtos.HasNext,
                    produtos.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return produtosDto;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar obter os produtos");
            }
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                return produto is null ? 
                    NotFound($"O produto com id = {id} não foi encontrado") : 
                    _mapper.Map<ProdutoDTO>(produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar obter o produto");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoDto);

                _uof.ProdutoRepository.Add(produto);
                await _uof.Commit();

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao tentar criar um novo produto");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                if (id != produtoDto.ProdutoId) return BadRequest($"Não foi possível atualizar o produto com id = {id}");

                var produto = _mapper.Map<Produto>(produtoDto);

                _uof.ProdutoRepository.Update(produto);
                await _uof.Commit();

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o produto com id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto is null) return NotFound($"O produto com id = {id} não foi encontrado");

                _uof.ProdutoRepository.Delete(produto);
                await _uof.Commit();

                return _mapper.Map<ProdutoDTO>(produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar excluir o produto com id = {id}");
            }
        }
    }
}