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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [SwaggerResponse(200, "Lista de usuários obtida com sucesso.")]
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter usuário por ID", Description = "Retorna um usuário específico pelo ID.")]
        [SwaggerResponse(200, "Usuário encontrado.")]
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

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar um novo usuário", Description = "Cadastra um novo usuário.")]
        [SwaggerResponse(201, "Usuário criado com sucesso.")]
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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um usuário", Description = "Atualiza os dados de um usuário existente.")]
        [SwaggerResponse(204, "Usuário atualizado com sucesso.")]
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
