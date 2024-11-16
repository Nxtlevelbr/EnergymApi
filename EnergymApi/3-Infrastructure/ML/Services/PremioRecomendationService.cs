using Microsoft.ML;
using Microsoft.ML.Data;
using EnergymApi._2_Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnergymApi._3_Infrastructure.ML.Services
{
    /// <summary>
    /// Serviço para recomendar prêmios aos usuários com base na pontuação acumulada.
    /// </summary>
    public class PremioRecomendationService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public PremioRecomendationService()
        {
            _mlContext = new MLContext();
            TreinarModelo();
        }

        /// <summary>
        /// Treina o modelo de recomendação usando um conjunto de dados simulado.
        /// </summary>
        private void TreinarModelo()
        {
            var data = new List<UsuarioPremioData>
            {
                new UsuarioPremioData { PontosAcumulados = 50, PremioId = 1 },
                new UsuarioPremioData { PontosAcumulados = 100, PremioId = 2 },
                new UsuarioPremioData { PontosAcumulados = 75, PremioId = 3 },
                new UsuarioPremioData { PontosAcumulados = 200, PremioId = 4 }
            };

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("PremioId")
                .Append(_mlContext.Transforms.Concatenate("Features", "PontosAcumulados"))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: "PremioId",
                    matrixColumnIndexColumnName: "PontosAcumulados",
                    matrixRowIndexColumnName: "PremioId"));

            _model = pipeline.Fit(trainingData);
        }

        /// <summary>
        /// Recomenda prêmios para um usuário com base em seus pontos acumulados.
        /// </summary>
        /// <param name="pontos">Pontos acumulados do usuário.</param>
        /// <returns>Lista de IDs de prêmios recomendados.</returns>
        public List<int> RecomendarPremios(int pontos)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UsuarioPremioData, PremioPrediction>(_model);

            var prediction = predictionEngine.Predict(new UsuarioPremioData { PontosAcumulados = pontos });

            return prediction.PremioIdPredictions
                .Select(p => (int)p)
                .Where(p => p > 0)
                .ToList();
        }
    }

    /// <summary>
    /// Classe auxiliar para representar dados de treinamento.
    /// </summary>
    public class UsuarioPremioData
    {
        [LoadColumn(0)]
        public float PontosAcumulados { get; set; }

        [LoadColumn(1)]
        public float PremioId { get; set; }
    }

    /// <summary>
    /// Classe de previsão.
    /// </summary>
    public class PremioPrediction
    {
        [ColumnName("Score")]
        public float[] PremioIdPredictions { get; set; }
    }
}
