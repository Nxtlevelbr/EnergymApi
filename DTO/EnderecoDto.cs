namespace EnergyApi.DTOs
{
    public class EnderecoDto
    {
        public int Id { get; set; } // ID do Endereço
        public string Cep { get; set; } // Código postal do endereço
        public string Estado { get; set; } // Estado do endereço (Ex: SP, RJ)
        public string Cidade { get; set; } // Nome da cidade
        public string Rua { get; set; } // Nome da rua
        public string Numero { get; set; } // Número do local
        public string? Complemento { get; set; } // Informação adicional sobre o endereço
    }
}