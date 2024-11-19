using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa uma academia no sistema.
    /// </summary>
    [Table("tb_academias")]
    public class Academia
    {
        /// <summary>
        /// Identificador único da academia.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// CNPJ da academia.
        /// </summary>
        [Required]
        [Column("cnpj")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos.")]
        public string CNPJ { get; set; }

        /// <summary>
        /// Nome da academia.
        /// </summary>
        [Required]
        [Column("nome")]
        [StringLength(100, ErrorMessage = "O nome da academia pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Observação sobre a academia.
        /// </summary>
        [Column("observacao")]
        [StringLength(255, ErrorMessage = "A observação pode ter no máximo 255 caracteres.")]
        public string? Observacao { get; set; }

        /// <summary>
        /// Identificador do endereço da academia.
        /// </summary>
        [Required]
        [Column("id_endereco")]
        public int EnderecoId { get; set; }

        /// <summary>
        /// Endereço associado à academia.
        /// </summary>
        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

        /// <summary>
        /// Nome de usuário associado à academia.
        /// </summary>
        [Required]
        [Column("usuario")]
        [StringLength(50, ErrorMessage = "O nome de usuário pode ter no máximo 50 caracteres.")]
        public string Usuario { get; set; }

        /// <summary>
        /// Senha da academia.
        /// </summary>
        [Required]
        [Column("senha")]
        public string Senha { get; set; }

        /// <summary>
        /// Registros de exercícios associados à academia.
        /// </summary>
        public ICollection<RegistroExercicio> RegistrosExercicios { get; set; } = new List<RegistroExercicio>();
    }
}
