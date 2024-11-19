using Microsoft.ML.Data;

namespace EnergymApi._3_Infrastructure.ML.Models
{
    /// <summary>
    /// Classe de previsão para recomendar prêmios.
    /// </summary>
    public class PremioPrediction
    {
        /// <summary>
        /// Score da previsão do prêmio.
        /// Representa o ID do prêmio recomendado com base na pontuação.
        /// </summary>
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}