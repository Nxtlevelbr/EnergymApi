using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
   
    [Table("tb_academias")]
    public class Academia
    {
        
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("cnpj")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos.")]
        public string CNPJ { get; set; }

       
        [Required]
        [Column("nome")]
        [StringLength(100, ErrorMessage = "O nome da academia pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Column("observacao")]
        [StringLength(255, ErrorMessage = "A observação pode ter no máximo 255 caracteres.")]
        public string? Observacao { get; set; }

        
        [Required]
        [Column("id_endereco")]
        public int EnderecoId { get; set; }

      
        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

       
        [Required]
        [Column("usuario")]
        [StringLength(50, ErrorMessage = "O nome de usuário pode ter no máximo 50 caracteres.")]
        public string Usuario { get; set; }

      
        [Required]
        [Column("senha")]
        public string Senha { get; set; }
        
        public ICollection<RegistroExercicio> RegistrosExercicios { get; set; } = new List<RegistroExercicio>();
    }
}
