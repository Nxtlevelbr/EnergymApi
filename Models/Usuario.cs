using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyApi.Models
{
    /// <summary>
    /// Representa os dados de um usuário no sistema.
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
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        [Required]
        [Column("email")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "O e-mail fornecido é inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// CPF do usuário (11 dígitos).
        /// </summary>
        [Required]
        [Column("cpf")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
        public string CPF { get; set; }

        /// <summary>
        /// Senha do usuário (armazenada em hash).
        /// </summary>
        [Required]
        [Column("senha")]
        public string Senha { get; set; }

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        [Column("data_nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Sexo do usuário.
        /// </summary>
        [Column("sexo")]
        [StringLength(10)]
        public string Sexo { get; set; }

        /// <summary>
        /// Pontuação acumulada do usuário.
        /// </summary>
        [Required]
        [Column("pontos")]
        [Range(0, int.MaxValue, ErrorMessage = "A pontuação deve ser um valor não negativo.")]
        public int Pontos { get; set; }

        /// <summary>
        /// Propriedade calculada para determinar a pontuação total baseada em registros de exercícios.
        /// </summary>
        [NotMapped] // Não mapeia para o banco; é calculado dinamicamente.
        public int PontuacaoTotal { get; private set; }

        /// <summary>
        /// Atualiza a pontuação total do usuário com base nos registros de exercícios.
        /// </summary>
        /// <param name="registrosKm">A lista de quilômetros percorridos pelo usuário.</param>
        public void AtualizarPontuacaoTotal(IEnumerable<int> registrosKm)
        {
            PontuacaoTotal = registrosKm.Sum(km => km); // 1 km = 1 ponto.
        }
    }
}
