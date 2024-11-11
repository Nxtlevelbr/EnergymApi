using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
{
    [Table("tb_registros_exercicios")]
    public class RegistroExercicio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } // Chave primária simples

        [Required]
        [Column("id_usuario")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        [Column("id_academia")]
        public int AcademiaId { get; set; }

        [ForeignKey("AcademiaId")]
        public Academia Academia { get; set; }

        [Required]
        [Column("km")]
        public double Km { get; set; }

        [Required]
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
    }
}