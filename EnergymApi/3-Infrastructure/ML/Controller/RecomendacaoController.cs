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
            if (pontos < 0) return BadRequest("Pontos acumulados devem ser não negativos.");

            var recomendacoes = _premioRecomendationService.RecomendarPremios(pontos);
            return recomendacoes.Any() ? Ok(recomendacoes) : NotFound("Nenhum prêmio encontrado.");
        }
    }
}