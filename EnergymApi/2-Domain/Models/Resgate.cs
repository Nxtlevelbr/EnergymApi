using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa um resgate de prêmio realizado por um usuário.
    /// </summary>
    [Table("tb_resgates")]
    public class Resgate
    {
        /// <summary>
        /// Identificador único do resgate.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário que realizou o resgate.
        /// </summary>
        [Required]
        [Column("id_usuario")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Entidade do usuário associada ao resgate.
        /// </summary>
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador do prêmio resgatado.
        /// </summary>
        [Required]
        [Column("id_premio")]
        public int PremioId { get; set; }

        /// <summary>
        /// Entidade do prêmio associado ao resgate.
        /// </summary>
        [ForeignKey("PremioId")]
        public Premio Premio { get; set; }

        /// <summary>
        /// Data e hora em que o resgate foi realizado.
        /// </summary>
        [Required]
        [Column("data_hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}