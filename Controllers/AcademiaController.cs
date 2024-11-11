using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaController : ControllerBase
    {
        private readonly IAcademiaService _academiaService;

        public AcademiaController(IAcademiaService academiaService)
        {
            _academiaService = academiaService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todas as academias", Description = "Retorna uma lista de todas as academias cadastradas.")]
        public async Task<ActionResult<IEnumerable<Academia>>> GetAcademias()
        {
            var academias = await _academiaService.ObterTodosAsync();
            if (!academias.Any()) return NoContent();
            return Ok(academias);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter academia por ID", Description = "Retorna uma academia pelo ID.")]
        public async Task<ActionResult<Academia>> GetAcademiaById(int id)
        {
            try
            {
                var academia = await _academiaService.ObterPorIdAsync(id);
                return Ok(academia);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar nova academia", Description = "Adiciona uma nova academia ao sistema.")]
        public async Task<ActionResult<Academia>> CreateAcademia([FromBody] Academia academia)
        {
            try
            {
                var novaAcademia = await _academiaService.AdicionarAsync(academia);
                return CreatedAtAction(nameof(GetAcademiaById), new { id = novaAcademia.Id }, novaAcademia);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar uma academia", Description = "Atualiza os dados de uma academia existente.")]
        public async Task<IActionResult> UpdateAcademia(int id, [FromBody] Academia academia)
        {
            if (id != academia.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID da academia." });
            }

            try
            {
                await _academiaService.AtualizarAsync(academia);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir uma academia", Description = "Remove uma academia pelo ID.")]
        public async Task<IActionResult> DeleteAcademia(int id)
        {
            var sucesso = await _academiaService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Academia não encontrada." });
            }
            return NoContent();
        }
    }
}
