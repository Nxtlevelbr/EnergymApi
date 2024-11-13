using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using EnergyApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremioController : ControllerBase
    {
        private readonly IPremioService _premioService;
        private readonly IMapper _mapper;

        public PremioController(IPremioService premioService, IMapper mapper)
        {
            _premioService = premioService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os prêmios", Description = "Retorna uma lista de todos os prêmios disponíveis.")]
        [SwaggerResponse(200, "Lista de prêmios retornada com sucesso.")]
        [SwaggerResponse(204, "Nenhum prêmio encontrado.")]
        public async Task<ActionResult<IEnumerable<PremioDto>>> GetPremios([FromQuery] bool incluirInativos = false)
        {
            var premios = await _premioService.ObterTodosAsync(incluirInativos);
            if (!premios.Any()) return NoContent();

            var premiosDto = _mapper.Map<IEnumerable<PremioDto>>(premios);
            return Ok(premiosDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter prêmio por ID", Description = "Retorna um prêmio pelo ID.")]
        [SwaggerResponse(200, "Prêmio retornado com sucesso.")]
        [SwaggerResponse(404, "Prêmio não encontrado.")]
        public async Task<ActionResult<PremioDto>> GetPremioById(int id)
        {
            try
            {
                var premio = await _premioService.ObterPorIdAsync(id);
                var premioDto = _mapper.Map<PremioDto>(premio);
                return Ok(premioDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo prêmio", Description = "Adiciona um novo prêmio ao sistema.")]
        [SwaggerResponse(201, "Prêmio criado com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos para criação do prêmio.")]
        public async Task<ActionResult<PremioDto>> CreatePremio([FromBody] PremioDto premioDto)
        {
            var premio = _mapper.Map<Premio>(premioDto);
            var novoPremio = await _premioService.AdicionarAsync(premio);
            var novoPremioDto = _mapper.Map<PremioDto>(novoPremio);

            return CreatedAtAction(nameof(GetPremioById), new { id = novoPremioDto.Id }, novoPremioDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um prêmio", Description = "Atualiza os dados de um prêmio existente.")]
        [SwaggerResponse(204, "Prêmio atualizado com sucesso.")]
        [SwaggerResponse(404, "Prêmio não encontrado.")]
        public async Task<IActionResult> UpdatePremio(int id, [FromBody] PremioDto premioDto)
        {
            if (id != premioDto.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do prêmio." });
            }

            try
            {
                var premio = _mapper.Map<Premio>(premioDto);
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
        [SwaggerResponse(204, "Prêmio excluído com sucesso.")]
        [SwaggerResponse(404, "Prêmio não encontrado.")]
        public async Task<IActionResult> DeletePremio(int id)
        {
            var sucesso = await _premioService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Prêmio não encontrado." });
            }
            return NoContent();
        }

        [HttpPatch("{id}/ativar")]
        [SwaggerOperation(Summary = "Ativar um prêmio", Description = "Ativa um prêmio inativo.")]
        [SwaggerResponse(204, "Prêmio ativado com sucesso.")]
        [SwaggerResponse(404, "Prêmio não encontrado.")]
        public async Task<IActionResult> AtivarPremio(int id)
        {
            try
            {
                await _premioService.AtivarOuDesativarPremioAsync(id, true);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/desativar")]
        [SwaggerOperation(Summary = "Desativar um prêmio", Description = "Desativa um prêmio ativo.")]
        [SwaggerResponse(204, "Prêmio desativado com sucesso.")]
        [SwaggerResponse(404, "Prêmio não encontrado.")]
        public async Task<IActionResult> DesativarPremio(int id)
        {
            try
            {
                await _premioService.AtivarOuDesativarPremioAsync(id, false);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
