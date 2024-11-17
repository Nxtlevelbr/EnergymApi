using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de resgates.
    /// </summary>
    public interface IResgateService
    {
        /// <summary>
        /// Adiciona um novo resgate.
        /// </summary>
        /// <param name="resgate">Resgate a ser adicionado.</param>
        /// <returns>O resgate adicionado.</returns>
        Task<Resgate> AdicionarAsync(Resgate resgate);

        /// <summary>
        /// Obtém um resgate pelo seu ID.
        /// </summary>
        /// <param name="id">ID do resgate.</param>
        /// <returns>O resgate encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Resgate> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os resgates cadastrados.
        /// </summary>
        /// <returns>Lista de resgates.</returns>
        Task<IEnumerable<Resgate>> ObterTodosAsync();

        /// <summary>
        /// Obtém todos os resgates realizados por um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de resgates realizados pelo usuário.</returns>
        Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId);

        /// <summary>
        /// Atualiza os dados de um resgate existente.
        /// </summary>
        /// <param name="resgate">Resgate com os dados atualizados.</param>
        /// <returns>O resgate atualizado.</returns>
        Task<Resgate> AtualizarAsync(Resgate resgate);

        /// <summary>
        /// Deleta um resgate pelo seu ID.
        /// </summary>
        /// <param name="id">ID do resgate a ser deletado.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Registra um resgate para um usuário e um prêmio específicos.
        /// </summary>
        /// <param name="usuarioId">ID do usuário que está realizando o resgate.</param>
        /// <param name="premioId">ID do prêmio a ser resgatado.</param>
        /// <returns>O resgate registrado.</returns>
        Task<Resgate> RegistrarResgate(int usuarioId, int premioId);
    }
}
