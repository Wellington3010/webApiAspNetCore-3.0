using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICategoriasProdutos.Models
{
    /// <summary>
    /// Entidade Categoria
    /// </summary>
    [Table("Categoria")]
    public class Categoria
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        /// <summary>
        /// Primary key da entidade categoria
        /// </summary>
        [Key]
        public int CategoriaId { get; set; }

        /// <summary>
        /// ImageUrl
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        public ICollection<Produto> Produtos {get;set;}
    }
}
