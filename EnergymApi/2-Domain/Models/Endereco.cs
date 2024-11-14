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
        [StringLength(8)]
        public string CEP { get; set; }

        [Required]
        [Column("estado")]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [Column("cidade")]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required]
        [Column("rua")]
        [StringLength(100)]
        public string Rua { get; set; }

        [Required]
        [Column("numero")]
        [StringLength(10)]
        public string Numero { get; set; }

        [Column("complemento")]
        [StringLength(50)]
        public string? Complemento { get; set; }
    }
}