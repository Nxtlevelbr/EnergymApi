using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyApi.DTOs;
using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaController : ControllerBase
    {
        private readonly IAcademiaService _academiaService;
        private readonly IMapper _mapper;

        public AcademiaController(IAcademiaService academiaService, IMapper mapper)
        {
            _academiaService = academiaService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todas as academias", Description = "Retorna uma lista de todas as academias cadastradas.")]
        [SwaggerResponse(200, "Lista de academias retornada com sucesso.", typeof(IEnumerable<AcademiaDto>))]
        [SwaggerResponse(204, "Nenhuma academia encontrada.")]
        public async Task<ActionResult<IEnumerable<AcademiaDto>>> GetAcademias()
        {
            var academias = await _academiaService.ObterTodosAsync();
            if (!academias.Any()) return NoContent();
            
            var academiasDto = _mapper.Map<IEnumerable<AcademiaDto>>(academias);
            return Ok(academiasDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter academia por ID", Description = "Retorna uma academia pelo ID.")]
        [SwaggerResponse(200, "Academia encontrada com sucesso.", typeof(AcademiaDto))]
        [SwaggerResponse(404, "Academia não encontrada.")]
        public async Task<ActionResult<AcademiaDto>> GetAcademiaById(int id)
        {
            try
            {
                var academia = await _academiaService.ObterPorIdAsync(id);
                var academiaDto = _mapper.Map<AcademiaDto>(academia);
                return Ok(academiaDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar nova academia", Description = "Adiciona uma nova academia ao sistema.")]
        [SwaggerResponse(201, "Academia criada com sucesso.", typeof(AcademiaDto))]
        [SwaggerResponse(400, "Erro de validação ou operação inválida.")]
        public async Task<ActionResult<AcademiaDto>> CreateAcademia([FromBody] AcademiaDto academiaDto)
        {
            try
            {
                var academia = _mapper.Map<Academia>(academiaDto);
                var novaAcademia = await _academiaService.AdicionarAsync(academia);
                var novaAcademiaDto = _mapper.Map<AcademiaDto>(novaAcademia);
                
                return CreatedAtAction(nameof(GetAcademiaById), new { id = novaAcademiaDto.Id }, novaAcademiaDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar uma academia", Description = "Atualiza os dados de uma academia existente.")]
        [SwaggerResponse(204, "Academia atualizada com sucesso.")]
        [SwaggerResponse(400, "O ID fornecido não corresponde ao ID da academia.")]
        [SwaggerResponse(404, "Academia não encontrada.")]
        public async Task<IActionResult> UpdateAcademia(int id, [FromBody] AcademiaDto academiaDto)
        {
            if (id != academiaDto.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID da academia." });
            }

            try
            {
                var academia = _mapper.Map<Academia>(academiaDto);
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
        [SwaggerResponse(204, "Academia excluída com sucesso.")]
        [SwaggerResponse(404, "Academia não encontrada.")]
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
