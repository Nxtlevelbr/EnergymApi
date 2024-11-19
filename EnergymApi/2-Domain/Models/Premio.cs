using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    /// <summary>
    /// Representa um prêmio que pode ser resgatado pelos usuários.
    /// </summary>
    [Table("tb_premios")]
    public class Premio
    {
        /// <summary>
        /// Identificador único do prêmio.
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Descrição do prêmio.
        /// </summary>
        [Required]
        [Column("descricao")]
        [StringLength(200, ErrorMessage = "A descrição pode ter no máximo 200 caracteres.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Pontos necessários para resgatar o prêmio.
        /// </summary>
        [Required]
        [Column("pontos")]
        [Range(1, int.MaxValue, ErrorMessage = "Os pontos devem ser um valor positivo.")]
        public int Pontos { get; set; }

        /// <summary>
        /// Nome da empresa fornecedora do prêmio.
        /// </summary>
        [Required]
        [Column("empresa")]
        [StringLength(100, ErrorMessage = "O nome da empresa pode ter no máximo 100 caracteres.")]
        public string Empresa { get; set; }

        /// <summary>
        /// Tipo do prêmio (ex.: "Produto", "Serviço").
        /// </summary>
        [Required]
        [Column("tipo")]
        [StringLength(50, ErrorMessage = "O tipo do prêmio pode ter no máximo 50 caracteres.")]
        public string Tipo { get; set; }

        /// <summary>
        /// Indica se o prêmio está ativo.
        /// </summary>
        [Column("ativo")]
        public bool Ativo { get; set; } = true;

        /// <summary>
        /// Lista de resgates associados a este prêmio.
        /// </summary>
        public ICollection<Resgate> Resgates { get; set; } = new List<Resgate>();
    }
}