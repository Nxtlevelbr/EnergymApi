using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremioController : ControllerBase
    {
        private readonly IPremioService _premioService;

        public PremioController(IPremioService premioService)
        {
            _premioService = premioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os prêmios", Description = "Retorna uma lista de todos os prêmios disponíveis.")]
        public async Task<ActionResult<IEnumerable<Premio>>> GetPremios()
        {
            var premios = await _premioService.ObterTodosAsync();
            if (!premios.Any()) return NoContent();
            return Ok(premios);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter prêmio por ID", Description = "Retorna um prêmio pelo ID.")]
        public async Task<ActionResult<Premio>> GetPremioById(int id)
        {
            try
            {
                var premio = await _premioService.ObterPorIdAsync(id);
                return Ok(premio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo prêmio", Description = "Adiciona um novo prêmio ao sistema.")]
        public async Task<ActionResult<Premio>> CreatePremio([FromBody] Premio premio)
        {
            var novoPremio = await _premioService.AdicionarAsync(premio);
            return CreatedAtAction(nameof(GetPremioById), new { id = novoPremio.Id }, novoPremio);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um prêmio", Description = "Atualiza os dados de um prêmio existente.")]
        public async Task<IActionResult> UpdatePremio(int id, [FromBody] Premio premio)
        {
            if (id != premio.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do prêmio." });
            }

            try
            {
                await _premioService.AtualizarAsync(premio);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um prêmio", Description = "Remove um prêmio pelo ID.")]
        public async Task<IActionResult> DeletePremio(int id)
        {
            var sucesso = await _premioService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Prêmio não encontrado." });
            }
            return NoContent();
        }
    }
}
