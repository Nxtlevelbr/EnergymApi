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
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoService enderecoService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os endereços", Description = "Retorna uma lista de todos os endereços cadastrados.")]
        [SwaggerResponse(200, "Lista de endereços retornada com sucesso.")]
        [SwaggerResponse(204, "Nenhum endereço encontrado.")]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> GetEnderecos()
        {
            var enderecos = await _enderecoService.ObterTodosAsync();
            if (!enderecos.Any()) return NoContent();

            var enderecosDto = _mapper.Map<IEnumerable<EnderecoDto>>(enderecos);
            return Ok(enderecosDto);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter endereço por ID", Description = "Retorna um endereço pelo ID.")]
        public async Task<ActionResult<EnderecoDto>> GetEnderecoById(int id)
        {
            try
            {
                var endereco = await _enderecoService.ObterPorIdAsync(id);
                var enderecoDto = _mapper.Map<EnderecoDto>(endereco);
                return Ok(enderecoDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo endereço", Description = "Adiciona um novo endereço ao sistema.")]
        public async Task<ActionResult<EnderecoDto>> CreateEndereco([FromBody] EnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            var novoEndereco = await _enderecoService.AdicionarAsync(endereco);
            var novoEnderecoDto = _mapper.Map<EnderecoDto>(novoEndereco);

            return CreatedAtAction(nameof(GetEnderecoById), new { id = novoEnderecoDto.Id }, novoEnderecoDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um endereço", Description = "Atualiza os dados de um endereço existente.")]
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] EnderecoDto enderecoDto)
        {
            if (id != enderecoDto.Id)
            {
                return BadRequest(new { message = "O ID fornecido não corresponde ao ID do endereço." });
            }

            try
            {
                var endereco = _mapper.Map<Endereco>(enderecoDto);
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
