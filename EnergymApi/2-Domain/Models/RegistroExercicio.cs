using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa um registro de exercício realizado por um usuário.
    /// </summary>
    [Table("tb_registros_exercicios")]
    public class RegistroExercicio
    {
        /// <summary>
        /// Identificador único do registro de exercício.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário que realizou o exercício.
        /// </summary>
        [Required]
        [Column("id_usuario")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Entidade do usuário associada a este registro.
        /// </summary>
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Distância percorrida (em quilômetros) durante o exercício.
        /// </summary>
        [Required]
        [Column("km")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A distância deve ser positiva.")]
        public double Km { get; set; }

        /// <summary>
        /// Data e hora do registro do exercício.
        /// </summary>
        [Required]
        [Column("data_hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}