using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResgateController : ControllerBase
    {
        private readonly IResgateService _resgateService;

        public ResgateController(IResgateService resgateService)
        {
            _resgateService = resgateService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os resgates", Description = "Retorna uma lista de todos os resgates realizados.")]
        public async Task<ActionResult<IEnumerable<Resgate>>> GetResgates()
        {
            var resgates = await _resgateService.ObterTodosAsync();
            if (!resgates.Any()) return NoContent();
            return Ok(resgates);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter resgate por ID", Description = "Retorna um resgate pelo ID.")]
        public async Task<ActionResult<Resgate>> GetResgateById(int id)
        {
            try
            {
                var resgate = await _resgateService.ObterPorIdAsync(id);
                return Ok(resgate);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo resgate", Description = "Registra um novo resgate de prêmio.")]
        public async Task<ActionResult<Resgate>> CreateResgate([FromBody] Resgate resgate)
        {
            var novoResgate = await _resgateService.AdicionarAsync(resgate);
            return CreatedAtAction(nameof(GetResgateById), new { id = novoResgate.Id }, novoResgate);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um resgate", Description = "Atualiza os dados de um resgate existente.")]
        public async Task<IActionResult> UpdateResgate(int id, [FromBody] Resgate resgate)
        {
            if (id != resgate.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do resgate." });
            }

            try
            {
                await _resgateService.AtualizarAsync(resgate);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um resgate", Description = "Remove um resgate pelo ID.")]
        public async Task<IActionResult> DeleteResgate(int id)
        {
            var sucesso = await _resgateService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Resgate não encontrado." });
            }
            return NoContent();
        }
    }
}
