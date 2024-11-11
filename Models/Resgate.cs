using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
{
    [Table("tb_resgates")]
    public class Resgate
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_usuario")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        [Column("id_premio")]
        public int PremioId { get; set; }

        [ForeignKey("PremioId")]
        public Premio Premio { get; set; }

        [Required]
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
    }
}