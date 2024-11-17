using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de usuários.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="usuario">Usuário a ser adicionado.</param>
        /// <returns>O usuário adicionado.</returns>
        Task<Usuario> AdicionarAsync(Usuario usuario);

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>O usuário encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Usuario> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        Task<IEnumerable<Usuario>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuario">Usuário com os dados atualizados.</param>
        /// <returns>O usuário atualizado.</returns>
        Task<Usuario> AtualizarAsync(Usuario usuario);

        /// <summary>
        /// Deleta um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém um usuário pelo seu nome de usuário.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>O usuário encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Usuario> ObterPorUsernameAsync(string username);

        /// <summary>
        /// Obtém um usuário pelo seu email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>O usuário encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Usuario> ObterPorEmailAsync(string email);

        /// <summary>
        /// Obtém um usuário pelo seu CPF.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>O usuário encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Usuario> ObterPorCPFAsync(string cpf);
    }
}
