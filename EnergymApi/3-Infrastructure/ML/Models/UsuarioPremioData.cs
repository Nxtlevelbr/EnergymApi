using Microsoft.ML.Data;

namespace EnergymApi._3_Infrastructure.ML.Models
{
    /// <summary>
    /// Modelo de dados para representar os pontos acumulados do usuário e o prêmio associado.
    /// </summary>
    public class UsuarioPremioData
    {
        /// <summary>
        /// Pontos acumulados pelo usuário.
        /// </summary>
        [LoadColumn(0)]
        public float PontosAcumulados { get; set; }

        /// <summary>
        /// ID do prêmio associado ao usuário.
        /// </summary>
        [LoadColumn(1)]
        public float PremioId { get; set; }

        /// <summary>
        /// ID do usuário.
        /// </summary>
        [LoadColumn(2)]
        public float UsuarioId { get; set; }
    }
}