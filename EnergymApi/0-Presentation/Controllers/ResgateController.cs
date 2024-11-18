using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergymApi._0_Presentation.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas aos resgates.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ResgateController : ControllerBase
    {
        private readonly IResgateService _resgateService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResgateController"/>.
        /// </summary>
        /// <param name="resgateService">Serviço de resgates.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public ResgateController(IResgateService resgateService, IMapper mapper)
        {
            _resgateService = resgateService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os resgates realizados.
        /// </summary>
        /// <returns>Uma lista de resgates.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os resgates", Description = "Retorna uma lista de todos os resgates realizados.")]
        [SwaggerResponse(200, "Lista de resgates retornada com sucesso.", typeof(IEnumerable<ResgateDto>))]
        [SwaggerResponse(204, "Nenhum resgate encontrado.")]
        public async Task<ActionResult<IEnumerable<ResgateDto>>> GetResgates()
        {
            var resgates = await _resgateService.ObterTodosAsync();
            if (!resgates.Any()) return NoContent();

            var resgatesDto = _mapper.Map<IEnumerable<ResgateDto>>(resgates);
            return Ok(resgatesDto);
        }

        /// <summary>
        /// Obtém um resgate específico pelo ID.
        /// </summary>
        /// <param name="id">ID do resgate.</param>
        /// <returns>Detalhes do resgate.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter resgate por ID", Description = "Retorna um resgate pelo ID.")]
        [SwaggerResponse(200, "Resgate retornado com sucesso.", typeof(ResgateDto))]
        [SwaggerResponse(404, "Resgate não encontrado.")]
        public async Task<ActionResult<ResgateDto>> GetResgateById(int id)
        {
            try
            {
                var resgate = await _resgateService.ObterPorIdAsync(id);
                var resgateDto = _mapper.Map<ResgateDto>(resgate);
                return Ok(resgateDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Registra um novo resgate de prêmio.
        /// </summary>
        /// <param name="resgateDto">Dados do resgate a ser criado.</param>
        /// <returns>O resgate criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo resgate", Description = "Registra um novo resgate de prêmio.")]
        [SwaggerResponse(201, "Resgate criado com sucesso.", typeof(ResgateDto))]
        public async Task<ActionResult<ResgateDto>> CreateResgate([FromBody] ResgateDto resgateDto)
        {
            var resgate = _mapper.Map<Resgate>(resgateDto);
            var novoResgate = await _resgateService.AdicionarAsync(resgate);
            var novoResgateDto = _mapper.Map<ResgateDto>(novoResgate);
            return CreatedAtAction(nameof(GetResgateById), new { id = novoResgateDto.Id }, novoResgateDto);
        }

        /// <summary>
        /// Atualiza os dados de um resgate existente.
        /// </summary>
        /// <param name="id">ID do resgate a ser atualizado.</param>
        /// <param name="resgateDto">Dados atualizados do resgate.</param>
        /// <returns>Resposta sem conteúdo se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um resgate", Description = "Atualiza os dados de um resgate existente.")]
        [SwaggerResponse(204, "Resgate atualizado com sucesso.")]
        [SwaggerResponse(400, "O ID fornecido não corresponde ao ID do resgate.")]
        [SwaggerResponse(404, "Resgate não encontrado.")]
        public async Task<IActionResult> UpdateResgate(int id, [FromBody] ResgateDto resgateDto)
        {
            if (id != resgateDto.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do resgate." });
            }

            try
            {
                var resgate = _mapper.Map<Resgate>(resgateDto);
                await _resgateService.AtualizarAsync(resgate);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um resgate pelo ID.
        /// </summary>
        /// <param name="id">ID do resgate a ser removido.</param>
        /// <returns>Resposta sem conteúdo se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um resgate", Description = "Remove um resgate pelo ID.")]
        [SwaggerResponse(204, "Resgate excluído com sucesso.")]
        [SwaggerResponse(404, "Resgate não encontrado.")]
        public async Task<IActionResult> DeleteResgate(int id)
        {
            var sucesso = await _resgateService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Resgate não encontrado." });
            }
            return NoContent();
        }

        /// <summary>
        /// Registra um novo resgate com base no usuário e prêmio.
        /// </summary>
        /// <param name="usuarioId">ID do usuário que realiza o resgate.</param>
        /// <param name="premioId">ID do prêmio a ser resgatado.</param>
        /// <returns>O resgate registrado.</returns>
        [HttpPost("registrar")]
        [SwaggerOperation(Summary = "Registrar resgate", Description = "Registra um novo resgate baseado no ID do usuário e do prêmio.")]
        [SwaggerResponse(201, "Resgate registrado com sucesso.", typeof(ResgateDto))]
        [SwaggerResponse(400, "Erro de validação ou pontos insuficientes.")]
        [SwaggerResponse(404, "Usuário ou prêmio não encontrados.")]
        public async Task<ActionResult<ResgateDto>> RegistrarResgate([FromQuery] int usuarioId, [FromQuery] int premioId)
        {
            try
            {
                var resgate = await _resgateService.RegistrarResgate(usuarioId, premioId);
                var resgateDto = _mapper.Map<ResgateDto>(resgate);
                return CreatedAtAction(nameof(GetResgateById), new { id = resgateDto.Id }, resgateDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
