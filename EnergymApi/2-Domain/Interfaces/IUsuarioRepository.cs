using EnergymApi._2_Domain.Models;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// interface do repositorio de usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="usuario">Usuário a ser adicionado.</param>
        /// <returns>O usuário adicionado.</returns>
        Task<Usuario> AdicionarAsync(Usuario usuario);

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Task<Usuario> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        Task<IEnumerable<Usuario>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de um usuário.
        /// </summary>
        /// <param name="usuario">Usuário com os dados atualizados.</param>
        /// <returns>O usuário atualizado.</returns>
        Task<Usuario> AtualizarAsync(Usuario usuario);

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns>True se a exclusão foi bem-sucedida, caso contrário, false.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém um usuário pelo nome de usuário.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Task<Usuario> ObterPorUsernameAsync(string username);

        /// <summary>
        /// Obtém um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Task<Usuario> ObterPorEmailAsync(string email);

        /// <summary>
        /// Obtém um usuário pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Task<Usuario> ObterPorCpfAsync(string cpf);

        /// <summary>
        /// Obtém um usuário pelo ID, com possíveis extensões ou operações adicionais.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Task<Usuario> ObterPorUsuarioIdAsync(int usuarioId);
    }
}
