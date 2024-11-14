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
        [StringLength(14)]
        public string CNPJ { get; set; }

        [Required]
        [Column("nome")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Column("observacao")]
        [StringLength(255)]
        public string? Observacao { get; set; }

        [Required]
        [Column("id_endereco")]
        public int EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

        [Required]
        [Column("usuario")]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Required]
        [Column("senha")]
        public string Senha { get; set; }
    }
}