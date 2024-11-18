using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergymApi._0_Presentation.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas aos usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="UsuarioController"/>.
        /// </summary>
        /// <param name="usuarioService">Serviço de usuários.</param>
        /// <param name="mapper">Instância do AutoMapper.</param>
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os usuários cadastrados.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [SwaggerResponse(200, "Lista de usuários obtida com sucesso.", typeof(IEnumerable<UsuarioDto>))]
        [SwaggerResponse(204, "Nenhum usuário encontrado.")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodosAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NoContent();
            }

            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }

        /// <summary>
        /// Obtém um usuário específico pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Os detalhes do usuário.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter usuário por ID", Description = "Retorna um usuário específico pelo ID.")]
        [SwaggerResponse(200, "Usuário encontrado.", typeof(UsuarioDto))]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterPorIdAsync(id);
                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                return Ok(usuarioDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="usuarioDto">Dados do novo usuário.</param>
        /// <returns>O usuário recém-criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar um novo usuário", Description = "Cadastra um novo usuário.")]
        [SwaggerResponse(201, "Usuário criado com sucesso.", typeof(UsuarioDto))]
        [SwaggerResponse(400, "Dados inválidos para criar o usuário.")]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                var novoUsuario = await _usuarioService.AdicionarAsync(usuario);
                var novoUsuarioDto = _mapper.Map<UsuarioDto>(novoUsuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = novoUsuarioDto.Id }, novoUsuarioDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuarioDto">Dados atualizados do usuário.</param>
        /// <returns>Resposta sem conteúdo se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um usuário", Description = "Atualiza os dados de um usuário existente.")]
        [SwaggerResponse(204, "Usuário atualizado com sucesso.")]
        [SwaggerResponse(400, "O ID do usuário não corresponde ao ID na URL.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                return BadRequest(new { message = "O ID do usuário não corresponde ao ID na URL." });
            }

            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                await _usuarioService.AtualizarAsync(usuario);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        /// <summary>
        /// Remove um usuário do sistema pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        /// <returns>Resposta sem conteúdo se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um usuário", Description = "Remove um usuário do sistema pelo ID.")]
        [SwaggerResponse(204, "Usuário excluído com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var sucesso = await _usuarioService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return NoContent();
        }
    }
}
