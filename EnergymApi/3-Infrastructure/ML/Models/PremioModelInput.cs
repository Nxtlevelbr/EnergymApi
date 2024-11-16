using Microsoft.ML.Data;

namespace EnergymApi._3_Infrastructure.ML
{
    /// <summary>
    /// Representa os dados de entrada para o modelo de recomendação de prêmios.
    /// </summary>
    public class PremioModelInput
    {
        [LoadColumn(0)]
        public float Pontos { get; set; }

        [LoadColumn(1)]
        public bool PremioDisponivel { get; set; }
    }
}