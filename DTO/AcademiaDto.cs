namespace EnergyApi.DTOs
{
    public class AcademiaDto
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string? Observacao { get; set; }
        
        public EnderecoDto Endereco { get; set; }

        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}