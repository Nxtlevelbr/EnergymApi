using System.ComponentModel.DataAnnotations;

namespace EnergymApi._1_Application.DTO
{
    public class AcademiaDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        public string CNPJ { get; set; }
        
        [Required(ErrorMessage = "O nome da academia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }
        
        public string? Observacao { get; set; }
        
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public EnderecoDto Endereco { get; set; }
        
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O usuário não pode exceder 50 caracteres.")]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}