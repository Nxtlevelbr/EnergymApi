using Microsoft.ML;
using EnergymApi._3_Infrastructure.ML.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnergymApi._3_Infrastructure.ML.Services
{
    public class PremioRecomendationService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private const string ModelPath = "ML/Models/PremioRecomendationModel.zip"; // Caminho relativo
        private const string DataPath = "Data/premios_dataset.csv"; // Caminho relativo

        public PremioRecomendationService()
        {
            _mlContext = new MLContext(seed: 0);

            if (File.Exists(ModelPath))
            {
                CarregarModelo();
            }
            else
            {
                TreinarModelo();
                SalvarModelo();
            }
        }

        private void TreinarModelo()
        {
            if (!File.Exists(DataPath))
                throw new FileNotFoundException($"O arquivo de dados '{DataPath}' não foi encontrado.");

            var trainingData = _mlContext.Data.LoadFromTextFile<UsuarioPremioData>(
                path: DataPath,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("PontosAcumulados")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("UsuarioId"))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: "PremioId",
                    matrixRowIndexColumnName: "UsuarioId",
                    matrixColumnIndexColumnName: "PontosAcumulados"));

            _model = pipeline.Fit(trainingData);

            var predictions = _model.Transform(trainingData);
            var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "PremioId");

            Console.WriteLine($"RMSE: {metrics.RootMeanSquaredError}, MAE: {metrics.MeanAbsoluteError}");
        }

        private void SalvarModelo()
        {
            var directory = Path.GetDirectoryName(ModelPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            if (_model != null)
            {
                _mlContext.Model.Save(_model, null, ModelPath);
            }
        }

        private void CarregarModelo()
        {
            if (File.Exists(ModelPath))
            {
                _model = _mlContext.Model.Load(ModelPath, out _);
            }
            else
            {
                throw new FileNotFoundException($"Modelo não encontrado em {ModelPath}");
            }
        }

        public List<int> RecomendarPremios(float pontos)
        {
            if (_model == null)
                throw new InvalidOperationException("O modelo não está carregado.");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UsuarioPremioData, PremioPrediction>(_model);
            var prediction = predictionEngine.Predict(new UsuarioPremioData { PontosAcumulados = pontos });

            return prediction.PremioIdPredictions?.Select(p => (int)p).Where(p => p > 0).ToList() ?? new List<int>();
        }
    }
}
