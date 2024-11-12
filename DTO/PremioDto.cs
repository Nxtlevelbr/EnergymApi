namespace EnergyApi.DTOs
{
    public class PremioDto
    {
        public int Id { get; set; } // Identificador do prêmio

        public string Descricao { get; set; } // Descrição do prêmio

        public int Pontos { get; set; } // Pontos necessários para resgatar o prêmio

        public string Empresa { get; set; } // Empresa fornecedora do prêmio

        public string Tipo { get; set; } // Tipo do prêmio, ex: "Produto" ou "Serviço"

        public bool Ativo { get; set; } // Indica se o prêmio está ativo
    }
}