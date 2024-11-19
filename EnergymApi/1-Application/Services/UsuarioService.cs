using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;


namespace EnergymApi._1_Application.Services
{
    /// <summary>
    /// Serviço para gerenciamento de usuários.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor do serviço de usuários.
        /// </summary>
        /// <param name="usuarioRepository">Repositório de usuários.</param>
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <inheritdoc />
        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                throw new InvalidOperationException("Senha é obrigatória.");
            }

            if (await _usuarioRepository.ObterPorUsernameAsync(usuario.Username) != null)
            {
                throw new InvalidOperationException("Username já está em uso.");
            }

            if (await _usuarioRepository.ObterPorEmailAsync(usuario.Email) != null)
            {
                throw new InvalidOperationException("Email já está em uso.");
            }

            if (await _usuarioRepository.ObterPorCpfAsync(usuario.CPF) != null)
            {
                throw new InvalidOperationException("CPF já está em uso.");
            }

            // Qualquer regra de negócios adicional pode ser aplicada aqui.
            return await _usuarioRepository.AdicionarAsync(usuario);
        }

        /// <inheritdoc />
        public async Task<Usuario> AtualizarAsync(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                throw new InvalidOperationException("Senha é obrigatória.");
            }

            if (await _usuarioRepository.ObterPorIdAsync(usuario.Id) == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return await _usuarioRepository.AtualizarAsync(usuario);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _usuarioRepository.ObterTodosAsync();
        }

        /// <inheritdoc />
        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        /// <inheritdoc />
        public async Task<bool> DeletarAsync(int id)
        {
            if (await _usuarioRepository.ObterPorIdAsync(id) == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return await _usuarioRepository.DeletarAsync(id);
        }

        public Task<Usuario> ObterPorUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterPorEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterPorCPFAsync(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
