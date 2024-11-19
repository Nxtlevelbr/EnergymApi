using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa um endereço no sistema.
    /// </summary>
    [Table("tb_enderecos")]
    public class Endereco
    {
        /// <summary>
        /// Identificador único do endereço.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Código postal (CEP) do endereço.
        /// </summary>
        [Required]
        [Column("cep")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter exatamente 8 dígitos.")]
        public string CEP { get; set; }

        /// <summary>
        /// Estado do endereço (representado por 2 caracteres).
        /// </summary>
        [Required]
        [Column("estado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ser representado por 2 caracteres.")]
        public string Estado { get; set; }

        /// <summary>
        /// Cidade do endereço.
        /// </summary>
        [Required]
        [Column("cidade")]
        [StringLength(100, ErrorMessage = "O nome da cidade pode ter no máximo 100 caracteres.")]
        public string Cidade { get; set; }

        /// <summary>
        /// Nome da rua do endereço.
        /// </summary>
        [Required]
        [Column("rua")]
        [StringLength(100, ErrorMessage = "O nome da rua pode ter no máximo 100 caracteres.")]
        public string Rua { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        [Required]
        [Column("numero")]
        [StringLength(10, ErrorMessage = "O número pode ter no máximo 10 caracteres.")]
        public string Numero { get; set; }

        /// <summary>
        /// Complemento do endereço (opcional).
        /// </summary>
        [Column("complemento")]
        [StringLength(50, ErrorMessage = "O complemento pode ter no máximo 50 caracteres.")]
        public string? Complemento { get; set; }
    }
}
