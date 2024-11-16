using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergymApi._2_Domain.Models
{
    [Table("tb_premios")]
    public class Premio
    {
      
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("descricao")]
        [StringLength(200, ErrorMessage = "A descrição pode ter no máximo 200 caracteres.")]
        public string Descricao { get; set; }
        
        [Required]
        [Column("pontos")]
        [Range(1, int.MaxValue, ErrorMessage = "Os pontos devem ser um valor positivo.")]
        public int Pontos { get; set; }
        
        [Required]
        [Column("empresa")]
        [StringLength(100, ErrorMessage = "O nome da empresa pode ter no máximo 100 caracteres.")]
        public string Empresa { get; set; }
        
        [Required]
        [Column("tipo")]
        [StringLength(50, ErrorMessage = "O tipo do prêmio pode ter no máximo 50 caracteres.")]
        public string Tipo { get; set; }
        
        [Column("ativo")]
        public bool Ativo { get; set; } = true;
        public ICollection<Resgate> Resgates { get; set; } = new List<Resgate>();
    }
}