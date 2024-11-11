using EnergyApi.Models;
using EnergyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnergyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [SwaggerResponse(200, "Lista de usuários obtida com sucesso.")]
        [SwaggerResponse(204, "Nenhum usuário encontrado.")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodosAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NoContent();
            }
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter usuário por ID", Description = "Retorna um usuário específico pelo ID.")]
        [SwaggerResponse(200, "Usuário encontrado.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterPorIdAsync(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar um novo usuário", Description = "Cadastra um novo usuário.")]
        [SwaggerResponse(201, "Usuário criado com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos para criar o usuário.")]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var novoUsuario = await _usuarioService.AdicionarAsync(usuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um usuário", Description = "Atualiza os dados de um usuário existente.")]
        [SwaggerResponse(204, "Usuário atualizado com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest(new { message = "O ID do usuário não corresponde ao ID na URL." });
            }

            try
            {
                await _usuarioService.AtualizarAsync(usuario);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

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
