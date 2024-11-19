namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar um resgate de prêmio.
    /// </summary>
    public class ResgateDto
    {
        /// <summary>
        /// Identificador único do resgate.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador único do usuário que realizou o resgate.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nome do usuário que realizou o resgate (opcional).
        /// </summary>
        public string UsuarioNome { get; set; }

        /// <summary>
        /// Identificador único do prêmio resgatado.
        /// </summary>
        public int PremioId { get; set; }

        /// <summary>
        /// Descrição do prêmio resgatado (opcional).
        /// </summary>
        public string PremioDescricao { get; set; }

        /// <summary>
        /// Data e hora em que o resgate foi realizado.
        /// </summary>
        public DateTime DataHora { get; set; }
    }
}