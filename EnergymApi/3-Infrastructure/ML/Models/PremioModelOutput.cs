namespace EnergymApi._3_Infrastructure.ML
{
    /// <summary>
    /// Representa a saída do modelo de recomendação de prêmios.
    /// </summary>
    public class PremioModelOutput
    {
        public bool Predicao { get; set; }
        public float Score { get; set; }
    }
}