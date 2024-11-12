using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
{
    [Table("tb_premios")]
    public class Premio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("descricao")]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [Column("pontos")]
        public int Pontos { get; set; }

        [Required]
        [Column("empresa")]
        [StringLength(100)]
        public string Empresa { get; set; }

        [Required]
        [Column("tipo")]
        [StringLength(50)]
        public string Tipo { get; set; } // Ex: "Produto" ou "Serviço"

        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}