namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar um Prêmio.
    /// </summary>
    public class PremioDto
    {
        /// <summary>
        /// Identificador único do prêmio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição do prêmio.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Pontos necessários para resgatar o prêmio.
        /// </summary>
        public int Pontos { get; set; }

        /// <summary>
        /// Nome da empresa fornecedora do prêmio.
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Tipo do prêmio, por exemplo, "Produto" ou "Serviço".
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Indica se o prêmio está ativo.
        /// </summary>
        public bool Ativo { get; set; }
    }
}