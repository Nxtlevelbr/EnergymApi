using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
{
    /// <summary>
    /// Representa o registro de um resgate realizado por um usuário para um prêmio específico.
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
        /// Relacionamento com o usuário que realizou o resgate.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador do prêmio resgatado.
        /// </summary>
        [Required]
        [Column("id_premio")]
        public int PremioId { get; set; }

        /// <summary>
        /// Relacionamento com o prêmio que foi resgatado.
        /// </summary>
        public Premio Premio { get; set; }

        /// <summary>
        /// Data e hora do resgate.
        /// </summary>
        [Required]
        [Column("data_hora")]
        public DateTime DataHora { get; set; } = DateTime.UtcNow; // Corrige "DeuateTime" para "DateTime"

    }
}