using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICategoriasProdutos.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1,1000,ErrorMessage = "O preco deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }
    }
}
