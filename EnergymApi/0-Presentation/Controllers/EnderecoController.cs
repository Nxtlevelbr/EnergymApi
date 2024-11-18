using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergymApi._0_Presentation.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas a endereços.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do EnderecoController.
        /// </summary>
        /// <param name="enderecoService">Serviço de endereços.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
        public EnderecoController(IEnderecoService enderecoService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os endereços cadastrados.
        /// </summary>
        /// <returns>Uma lista de endereços.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os endereços", Description = "Retorna uma lista de todos os endereços cadastrados.")]
        [SwaggerResponse(200, "Lista de endereços retornada com sucesso.", typeof(IEnumerable<EnderecoDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado.")]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> GetEnderecos()
        {
            var enderecos = await _enderecoService.ObterTodosAsync();
            if (!enderecos.Any()) return NoContent();

            var enderecosDto = _mapper.Map<IEnumerable<EnderecoDto>>(enderecos);
            return Ok(enderecosDto);
        }

        /// <summary>
        /// Obtém um endereço específico pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço.</param>
        /// <returns>Detalhes do endereço.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter endereço por ID", Description = "Retorna um endereço pelo ID.")]
        [SwaggerResponse(200, "Endereço encontrado com sucesso.", typeof(EnderecoDto))]
        [SwaggerResponse(404, "Endereço não encontrado.")]
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

        /// <summary>
        /// Adiciona um novo endereço ao sistema.
        /// </summary>
        /// <param name="enderecoDto">Objeto contendo os dados do novo endereço.</param>
        /// <returns>O endereço criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo endereço", Description = "Adiciona um novo endereço ao sistema.")]
        [SwaggerResponse(201, "Endereço criado com sucesso.", typeof(EnderecoDto))]
        public async Task<ActionResult<EnderecoDto>> CreateEndereco([FromBody] EnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            var novoEndereco = await _enderecoService.AdicionarAsync(endereco);
            var novoEnderecoDto = _mapper.Map<EnderecoDto>(novoEndereco);

            return CreatedAtAction(nameof(GetEnderecoById), new { id = novoEnderecoDto.Id }, novoEnderecoDto);
        }

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="id">ID do endereço a ser atualizado.</param>
        /// <param name="enderecoDto">Objeto contendo os novos dados do endereço.</param>
        /// <returns>Resposta sem conteúdo se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um endereço", Description = "Atualiza os dados de um endereço existente.")]
        [SwaggerResponse(204, "Endereço atualizado com sucesso.")]
        [SwaggerResponse(400, "O ID fornecido não corresponde ao ID do endereço.")]
        [SwaggerResponse(404, "Endereço não encontrado.")]
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

        /// <summary>
        /// Exclui um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser excluído.</param>
        /// <returns>Resposta sem conteúdo se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um endereço", Description = "Remove um endereço pelo ID.")]
        [SwaggerResponse(204, "Endereço excluído com sucesso.")]
        [SwaggerResponse(404, "Endereço não encontrado.")]
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
