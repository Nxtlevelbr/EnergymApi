using Microsoft.ML;
using EnergymApi._3_Infrastructure.ML.Models;

namespace EnergymApi._3_Infrastructure.ML.Services
{
    /// <summary>
    /// Serviço de recomendação de prêmios com base na pontuação acumulada dos usuários.
    /// </summary>
    public class PremioRecomendationService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private readonly string _modelPath;
        private readonly string _dataPath;

        /// <summary>
        /// Inicializa uma nova instância do serviço de recomendação de prêmios.
        /// </summary>
        public PremioRecomendationService()
        {
            _mlContext = new MLContext(seed: 0);

            // Caminhos absolutos baseados no diretório da solução
            var basePath = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            if (basePath == null) throw new DirectoryNotFoundException("O diretório base não foi encontrado.");

            _modelPath = Path.Combine(basePath, "3-Infrastructure", "ML", "Models", "PremioRecomendationModel.zip");
            _dataPath = Path.Combine(basePath, "3-Infrastructure", "Data", "premios_dataset.csv");

            if (File.Exists(_modelPath))
            {
                CarregarModelo();
            }
            else
            {
                TreinarModelo();
                SalvarModelo();
            }
        }

        /// <summary>
        /// Treina o modelo de recomendação.
        /// </summary>
        private void TreinarModelo()
        {
            if (!File.Exists(_dataPath))
                throw new FileNotFoundException($"O arquivo de dados '{_dataPath}' não foi encontrado.");

            var trainingData = _mlContext.Data.LoadFromTextFile<UsuarioPremioData>(
                path: _dataPath,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("PremioId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("UsuarioId"))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: "PremioId",
                    matrixRowIndexColumnName: "UsuarioId",
                    matrixColumnIndexColumnName: "PontosAcumulados"));

            _model = pipeline.Fit(trainingData);
            SalvarModelo();
        }


        /// <summary>
        /// Salva o modelo treinado no disco.
        /// </summary>
        private void SalvarModelo()
        {
            var directory = Path.GetDirectoryName(_modelPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            if (_model != null)
            {
                _mlContext.Model.Save(_model, null, _modelPath);
            }
        }

        /// <summary>
        /// Carrega o modelo salvo do disco.
        /// </summary>
        private void CarregarModelo()
        {
            if (File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out _);
            }
            else
            {
                throw new FileNotFoundException($"Modelo não encontrado em {_modelPath}");
            }
        }

        /// <summary>
        /// Recomenda prêmios com base nos pontos acumulados do usuário.
        /// </summary>
        /// <param name="pontos">Pontos acumulados pelo usuário.</param>
        /// <returns>Lista de descrições dos prêmios recomendados.</returns>
        /// <exception cref="InvalidOperationException">Se o modelo não estiver carregado.</exception>
        public List<string> RecomendarPremios(float pontos)
        {
            if (_model == null)
                throw new InvalidOperationException("O modelo não está carregado.");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UsuarioPremioData, PremioPrediction>(_model);
            var prediction = predictionEngine.Predict(new UsuarioPremioData { PontosAcumulados = pontos });

            var premioId = (int)Math.Round(prediction.Score);

            var premioMapping = new Dictionary<int, string>
            {
                { 1, "voucher 15% desconto" },
                { 2, "15% desconto curso SAP" },
                { 3, "ingresso fórmula 1" },
                { 4, "50% desconto próxima mensalidade" },
                { 5, "certificação grátis SAP" }
            };

            return premioMapping.TryGetValue(premioId, out var premio)
                ? new List<string> { $"Prêmio Recomendado: {premio}" }
                : new List<string> { "Prêmio desconhecido." };
        }




    }
}
