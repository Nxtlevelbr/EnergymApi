using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("cpf")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required]
        [Column("senha")]
        public string Senha { get; set; }

        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("sexo")]
        [StringLength(10)]
        public string Sexo { get; set; }

        [Required]
        [Column("pontos")]
        public int Pontos { get; set; }

        public ICollection<RegistroExercicio> RegistrosExercicios { get; set; } = new List<RegistroExercicio>();

        public ICollection<Resgate> Resgates { get; set; } = new List<Resgate>();
    }
}