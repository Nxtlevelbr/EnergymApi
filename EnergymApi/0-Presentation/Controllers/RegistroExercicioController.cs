using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergymApi._0_Presentation.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar os registros de exercícios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroExercicioController : ControllerBase
    {
        private readonly IRegistroExercicioService _registroExercicioService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do controlador <see cref="RegistroExercicioController"/>.
        /// </summary>
        /// <param name="registroExercicioService">Serviço de registro de exercícios.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public RegistroExercicioController(IRegistroExercicioService registroExercicioService, IMapper mapper)
        {
            _registroExercicioService = registroExercicioService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os registros de exercícios.
        /// </summary>
        /// <returns>Lista de registros de exercícios.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os registros de exercícios", Description = "Retorna uma lista de todos os registros de exercícios.")]
        [SwaggerResponse(200, "Lista de registros retornada com sucesso.")]
        [SwaggerResponse(204, "Nenhum registro encontrado.")]
        public async Task<ActionResult<IEnumerable<RegistroExercicioDto>>> GetRegistros()
        {
            var registros = await _registroExercicioService.ObterTodosAsync();
            if (!registros.Any()) return NoContent();

            var registrosDto = _mapper.Map<IEnumerable<RegistroExercicioDto>>(registros);
            return Ok(registrosDto);
        }

        /// <summary>
        /// Obtém um registro de exercício específico pelo ID.
        /// </summary>
        /// <param name="id">ID do registro de exercício.</param>
        /// <returns>Registro de exercício correspondente ao ID fornecido.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter registro por ID", Description = "Retorna um registro de exercício pelo ID.")]
        [SwaggerResponse(200, "Registro encontrado com sucesso.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
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

        /// <summary>
        /// Adiciona um novo registro de exercício.
        /// </summary>
        /// <param name="registroExercicioDto">Dados do novo registro de exercício.</param>
        /// <returns>O registro criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo registro de exercício", Description = "Adiciona um novo registro de exercício ao sistema.")]
        [SwaggerResponse(201, "Registro criado com sucesso.")]
        public async Task<ActionResult<RegistroExercicioDto>> CreateRegistro([FromBody] RegistroExercicioDto registroExercicioDto)
        {
            var registro = _mapper.Map<RegistroExercicio>(registroExercicioDto);
            var novoRegistro = await _registroExercicioService.AdicionarAsync(registro);
            var novoRegistroDto = _mapper.Map<RegistroExercicioDto>(novoRegistro);

            return CreatedAtAction(nameof(GetRegistroById), new { id = novoRegistroDto.Id }, novoRegistroDto);
        }

        /// <summary>
        /// Atualiza um registro de exercício existente.
        /// </summary>
        /// <param name="id">ID do registro a ser atualizado.</param>
        /// <param name="registroExercicioDto">Dados atualizados do registro.</param>
        /// <returns>Resposta sem conteúdo ou erro caso o ID não corresponda.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um registro de exercício", Description = "Atualiza os dados de um registro existente.")]
        [SwaggerResponse(204, "Registro atualizado com sucesso.")]
        [SwaggerResponse(400, "O ID fornecido não corresponde ao ID do registro.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
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

        /// <summary>
        /// Exclui um registro de exercício específico.
        /// </summary>
        /// <param name="id">ID do registro a ser excluído.</param>
        /// <returns>Resposta sem conteúdo ou erro caso o registro não seja encontrado.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um registro de exercício", Description = "Remove um registro pelo ID.")]
        [SwaggerResponse(204, "Registro excluído com sucesso.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
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
