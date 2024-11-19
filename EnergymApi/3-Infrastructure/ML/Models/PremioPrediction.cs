using Microsoft.ML.Data;

namespace EnergymApi._3_Infrastructure.ML.Models
{
    /// <summary>
    /// Modelo de previsão contendo os IDs dos prêmios recomendados.
    /// </summary>
    public class PremioPrediction
    {
        /// <summary>
        /// IDs de prêmios recomendados com base na pontuação do usuário.
        /// </summary>
        [ColumnName("Score")]
        public float[]? PremioIdPredictions { get; set; }
    }
}