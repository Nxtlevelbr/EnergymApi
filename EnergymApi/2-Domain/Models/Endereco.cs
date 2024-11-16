using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    
    [Table("tb_enderecos")]
    public class Endereco
    {
      
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("cep")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter exatamente 8 dígitos.")]
        public string CEP { get; set; }

     
        [Required]
        [Column("estado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ser representado por 2 caracteres.")]
        public string Estado { get; set; }

       
        [Required]
        [Column("cidade")]
        [StringLength(100, ErrorMessage = "O nome da cidade pode ter no máximo 100 caracteres.")]
        public string Cidade { get; set; }

     
        [Required]
        [Column("rua")]
        [StringLength(100, ErrorMessage = "O nome da rua pode ter no máximo 100 caracteres.")]
        public string Rua { get; set; }

        [Required]
        [Column("numero")]
        [StringLength(10, ErrorMessage = "O número pode ter no máximo 10 caracteres.")]
        public string Numero { get; set; }

      
        [Column("complemento")]
        [StringLength(50, ErrorMessage = "O complemento pode ter no máximo 50 caracteres.")]
        public string? Complemento { get; set; }
    }
}
