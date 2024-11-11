using EnergyApi.Models;
using EnergyApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            // Exemplo de lógica adicional: verificar se o username já existe
            var usuarioExistente = await _usuarioRepository.ObterPorUsernameAsync(usuario.Username);
            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("Username já está em uso.");
            }

            return await _usuarioRepository.AdicionarAsync(usuario);
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _usuarioRepository.ObterTodosAsync();
        }

        public async Task<Usuario> AtualizarAsync(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return await _usuarioRepository.AtualizarAsync(usuario);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _usuarioRepository.DeletarAsync(id);
        }

        public async Task<Usuario> ObterPorUsernameAsync(string username)
        {
            return await _usuarioRepository.ObterPorUsernameAsync(username);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _usuarioRepository.ObterPorEmailAsync(email);
        }

        public async Task<Usuario> ObterPorCPFAsync(string cpf)
        {
            return await _usuarioRepository.ObterPorCPFAsync(cpf);
        }
    }
}
