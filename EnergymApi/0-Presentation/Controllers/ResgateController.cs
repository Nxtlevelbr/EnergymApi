using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergymApi._0_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResgateController : ControllerBase
    {
        private readonly IResgateService _resgateService;
        private readonly IMapper _mapper;

        public ResgateController(IResgateService resgateService, IMapper mapper)
        {
            _resgateService = resgateService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os resgates", Description = "Retorna uma lista de todos os resgates realizados.")]
        public async Task<ActionResult<IEnumerable<ResgateDto>>> GetResgates()
        {
            var resgates = await _resgateService.ObterTodosAsync();
            if (!resgates.Any()) return NoContent();

            var resgatesDto = _mapper.Map<IEnumerable<ResgateDto>>(resgates);
            return Ok(resgatesDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter resgate por ID", Description = "Retorna um resgate pelo ID.")]
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

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo resgate", Description = "Registra um novo resgate de prêmio.")]
        public async Task<ActionResult<ResgateDto>> CreateResgate([FromBody] ResgateDto resgateDto)
        {
            var resgate = _mapper.Map<Resgate>(resgateDto);
            var novoResgate = await _resgateService.AdicionarAsync(resgate);
            var novoResgateDto = _mapper.Map<ResgateDto>(novoResgate);
            return CreatedAtAction(nameof(GetResgateById), new { id = novoResgateDto.Id }, novoResgateDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um resgate", Description = "Atualiza os dados de um resgate existente.")]
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

        [HttpPost("registrar")]
        [SwaggerOperation(Summary = "Registrar resgate", Description = "Registra um novo resgate baseado no ID do usuário e do prêmio.")]
        [SwaggerResponse(201, "Resgate registrado com sucesso.")]
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
