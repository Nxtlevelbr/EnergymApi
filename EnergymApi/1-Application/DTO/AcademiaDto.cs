using System.ComponentModel.DataAnnotations;

namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar uma Academia.
    /// </summary>
    public class AcademiaDto
    {
        /// <summary>
        /// Identificador único da academia.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// CNPJ da academia.
        /// </summary>
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        public string CNPJ { get; set; }
        
        /// <summary>
        /// Nome da academia.
        /// </summary>
        [Required(ErrorMessage = "O nome da academia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }
        
        /// <summary>
        /// Observação sobre a academia.
        /// </summary>
        public string? Observacao { get; set; }
        
        /// <summary>
        /// Endereço da academia.
        /// </summary>
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public EnderecoDto Endereco { get; set; }
        
        /// <summary>
        /// Nome de usuário do administrador da academia.
        /// </summary>
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O usuário não pode exceder 50 caracteres.")]
        public string Usuario { get; set; }
        
        /// <summary>
        /// Senha do administrador da academia.
        /// </summary>
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}