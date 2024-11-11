using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os endereços", Description = "Retorna uma lista de todos os endereços cadastrados.")]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            var enderecos = await _enderecoService.ObterTodosAsync();
            if (!enderecos.Any()) return NoContent();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter endereço por ID", Description = "Retorna um endereço pelo ID.")]
        public async Task<ActionResult<Endereco>> GetEnderecoById(int id)
        {
            try
            {
                var endereco = await _enderecoService.ObterPorIdAsync(id);
                return Ok(endereco);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo endereço", Description = "Adiciona um novo endereço ao sistema.")]
        public async Task<ActionResult<Endereco>> CreateEndereco([FromBody] Endereco endereco)
        {
            var novoEndereco = await _enderecoService.AdicionarAsync(endereco);
            return CreatedAtAction(nameof(GetEnderecoById), new { id = novoEndereco.Id }, novoEndereco);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um endereço", Description = "Atualiza os dados de um endereço existente.")]
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do endereço." });
            }

            try
            {
                await _enderecoService.AtualizarAsync(endereco);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um endereço", Description = "Remove um endereço pelo ID.")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var sucesso = await _enderecoService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }
            return NoContent();
        }
    }
}
