using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa um usuário do sistema.
    /// </summary>
    [Table("tb_usuarios")]
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome de usuário.
        /// </summary>
        [Required]
        [Column("username")]
        [StringLength(50, ErrorMessage = "O nome de usuário pode ter no máximo 50 caracteres.")]
        public string Username { get; set; }

        /// <summary>
        /// Endereço de email do usuário.
        /// </summary>
        [Required]
        [Column("email")]
        [StringLength(100, ErrorMessage = "O email pode ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        /// <summary>
        /// CPF do usuário.
        /// </summary>
        [Required]
        [Column("cpf")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        [Required]
        [Column("senha")]
        public string Senha { get; set; }

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Sexo do usuário.
        /// </summary>
        [Column("sexo")]
        [StringLength(10, ErrorMessage = "O campo Sexo pode ter no máximo 10 caracteres.")]
        public string Sexo { get; set; }

        /// <summary>
        /// Pontos acumulados pelo usuário.
        /// </summary>
            [Required]
            [Column("pontos")]
            [Range(0, int.MaxValue, ErrorMessage = "Os pontos devem ser um valor positivo.")]
        public int Pontos { get; set; }

        /// <summary>
        /// Lista de registros de exercícios associados ao usuário.
        /// </summary>
        public ICollection<RegistroExercicio> RegistrosExercicios { get; set; } = new List<RegistroExercicio>();

        /// <summary>
        /// Lista de resgates associados ao usuário.
        /// </summary>
        public ICollection<Resgate> Resgates { get; set; } = new List<Resgate>();
    }
}

