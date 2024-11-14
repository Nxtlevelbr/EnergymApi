namespace EnergymApi._1_Application.DTO
{
    public class ResgateDto
    {
        public int Id { get; set; } // Identificador do resgate

        public int UsuarioId { get; set; } // Identificador do usuário que realizou o resgate

        public string UsuarioNome { get; set; } // Nome do usuário (opcional, caso desejado para exibição)

        public int PremioId { get; set; } // Identificador do prêmio resgatado

        public string PremioDescricao { get; set; } // Descrição do prêmio (opcional, caso desejado para exibição)

        public DateTime DataHora { get; set; } // Data e hora do resgate
    }
}