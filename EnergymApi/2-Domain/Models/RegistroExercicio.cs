using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    [Table("tb_registros_exercicios")]
    public class RegistroExercicio
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
        [Column("km")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A distância deve ser positiva.")]
        public double Km { get; set; }

        [Required]
        [Column("data_hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}