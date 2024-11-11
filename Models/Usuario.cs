using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
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
        public string Sexo { get; set; }

        [Column("pontos")]
        public int Pontos { get; set; }
    }
}