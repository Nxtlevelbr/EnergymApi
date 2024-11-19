using EnergymApi._3_Infrastructure.ML.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergymApi._0_Presentation.Controllers
{
    /// <summary>
    /// Controlador para expor os serviços de recomendação de prêmios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly PremioRecomendationService _premioRecomendationService;

        /// <summary>
        /// Inicializa o controlador de recomendação.
        /// </summary>
        public RecomendacaoController(PremioRecomendationService premioRecomendationService)
        {
            _premioRecomendationService = premioRecomendationService;
        }

        /// <summary>
        /// Retorna prêmios recomendados para o usuário com base nos pontos.
        /// </summary>
        [HttpPost("sugerir")]
        public IActionResult SugerirPremios([FromBody] float pontos)
        {
            var recomendacoes = _premioRecomendationService.RecomendarPremios(pontos);

            if (recomendacoes.Any(r => r.Contains("Nenhum prêmio")))
            {
                return NotFound(new { message = "Nenhum prêmio disponível para os pontos acumulados." });
            }

            return Ok(new { recomendacoes });
        }


    }
}