using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoApi.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string ImagemUrl { get; set; }

        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }


        //Define o relacionamento
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}