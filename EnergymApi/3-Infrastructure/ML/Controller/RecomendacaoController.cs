using Microsoft.AspNetCore.Mvc;
using EnergymApi._3_Infrastructure.ML;
using EnergymApi._3_Infrastructure.ML.Services;

namespace EnergymApi._3_Infrastructure.ML.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly PremioRecomendationService _premioRecomendationService;

        public RecomendacaoController(PremioRecomendationService premioRecomendationService)
        {
            _premioRecomendationService = premioRecomendationService;
        }

        /// <summary>
        /// Sugere prêmios com base nos pontos acumulados do usuário.
        /// </summary>
        /// <param name="input">Modelo contendo os pontos acumulados.</param>
        /// <returns>Lista de prêmios recomendados.</returns>
        [HttpPost("sugerir")]
        public IActionResult SugerirPremios([FromBody] PremioModelInput input)
        {
            if (input.PontosAcumulados < 0)
            {
                return BadRequest("Pontos acumulados não podem ser negativos.");
            }

            var recomendacoes = _premioRecomendationService.RecomendarPremios(input.PontosAcumulados);
            if (recomendacoes == null || !recomendacoes.Any())
            {
                return NotFound("Nenhum prêmio disponível para os pontos acumulados.");
            }

            return Ok(new { Recomendacoes = recomendacoes });
        }
    }

    /// <summary>
    /// Modelo de entrada para recomendação de prêmios.
    /// </summary>
    public class PremioModelInput
    {
        public int PontosAcumulados { get; set; }
    }
}