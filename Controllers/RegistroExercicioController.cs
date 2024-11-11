using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroExercicioController : ControllerBase
    {
        private readonly IRegistroExercicioService _registroExercicioService;

        public RegistroExercicioController(IRegistroExercicioService registroExercicioService)
        {
            _registroExercicioService = registroExercicioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os registros de exercícios", Description = "Retorna uma lista de todos os registros de exercícios.")]
        public async Task<ActionResult<IEnumerable<RegistroExercicio>>> GetRegistros()
        {
            var registros = await _registroExercicioService.ObterTodosAsync();
            if (!registros.Any()) return NoContent();
            return Ok(registros);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter registro por ID", Description = "Retorna um registro de exercício pelo ID.")]
        public async Task<ActionResult<RegistroExercicio>> GetRegistroById(int id)
        {
            try
            {
                var registro = await _registroExercicioService.ObterPorIdAsync(id);
                return Ok(registro);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo registro de exercício", Description = "Adiciona um novo registro de exercício ao sistema.")]
        public async Task<ActionResult<RegistroExercicio>> CreateRegistro([FromBody] RegistroExercicio registroExercicio)
        {
            var novoRegistro = await _registroExercicioService.AdicionarAsync(registroExercicio);
            return CreatedAtAction(nameof(GetRegistroById), new { id = novoRegistro.Id }, novoRegistro);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um registro de exercício", Description = "Atualiza os dados de um registro existente.")]
        public async Task<IActionResult> UpdateRegistro(int id, [FromBody] RegistroExercicio registroExercicio)
        {
            if (id != registroExercicio.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do registro." });
            }

            try
            {
                await _registroExercicioService.AtualizarAsync(registroExercicio);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um registro de exercício", Description = "Remove um registro pelo ID.")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            var sucesso = await _registroExercicioService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Registro não encontrado." });
            }
            return NoContent();
        }
    }
}
