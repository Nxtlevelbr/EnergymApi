using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using EnergyApi.DTOs;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroExercicioController : ControllerBase
    {
        private readonly IRegistroExercicioService _registroExercicioService;
        private readonly IMapper _mapper;

        public RegistroExercicioController(IRegistroExercicioService registroExercicioService, IMapper mapper)
        {
            _registroExercicioService = registroExercicioService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os registros de exercícios", Description = "Retorna uma lista de todos os registros de exercícios.")]
        public async Task<ActionResult<IEnumerable<RegistroExercicioDto>>> GetRegistros()
        {
            var registros = await _registroExercicioService.ObterTodosAsync();
            if (!registros.Any()) return NoContent();

            var registrosDto = _mapper.Map<IEnumerable<RegistroExercicioDto>>(registros);
            return Ok(registrosDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter registro por ID", Description = "Retorna um registro de exercício pelo ID.")]
        public async Task<ActionResult<RegistroExercicioDto>> GetRegistroById(int id)
        {
            try
            {
                var registro = await _registroExercicioService.ObterPorIdAsync(id);
                var registroDto = _mapper.Map<RegistroExercicioDto>(registro);
                return Ok(registroDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo registro de exercício", Description = "Adiciona um novo registro de exercício ao sistema.")]
        public async Task<ActionResult<RegistroExercicioDto>> CreateRegistro([FromBody] RegistroExercicioDto registroExercicioDto)
        {
            var registro = _mapper.Map<RegistroExercicio>(registroExercicioDto);
            var novoRegistro = await _registroExercicioService.AdicionarAsync(registro);
            var novoRegistroDto = _mapper.Map<RegistroExercicioDto>(novoRegistro);

            return CreatedAtAction(nameof(GetRegistroById), new { id = novoRegistroDto.Id }, novoRegistroDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um registro de exercício", Description = "Atualiza os dados de um registro existente.")]
        public async Task<IActionResult> UpdateRegistro(int id, [FromBody] RegistroExercicioDto registroExercicioDto)
        {
            if (id != registroExercicioDto.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do registro." });
            }

            try
            {
                var registro = _mapper.Map<RegistroExercicio>(registroExercicioDto);
                await _registroExercicioService.AtualizarAsync(registro);
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
