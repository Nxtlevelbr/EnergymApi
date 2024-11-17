using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de academias.
    /// </summary>
    public interface IAcademiaService
    {
        /// <summary>
        /// Adiciona uma nova academia.
        /// </summary>
        /// <param name="academia">Academia a ser adicionada.</param>
        /// <returns>A academia adicionada.</returns>
        Task<Academia> AdicionarAsync(Academia academia);

        /// <summary>
        /// Obtém uma academia pelo seu ID.
        /// </summary>
        /// <param name="id">ID da academia.</param>
        /// <returns>A academia encontrada ou <c>null</c> se não encontrada.</returns>
        Task<Academia> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todas as academias cadastradas.
        /// </summary>
        /// <returns>Lista de academias.</returns>
        Task<IEnumerable<Academia>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de uma academia existente.
        /// </summary>
        /// <param name="academia">Academia com os dados atualizados.</param>
        /// <returns>A academia atualizada.</returns>
        Task<Academia> AtualizarAsync(Academia academia);

        /// <summary>
        /// Deleta uma academia pelo seu ID.
        /// </summary>
        /// <param name="id">ID da academia a ser deletada.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém uma academia pelo seu CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ da academia.</param>
        /// <returns>A academia encontrada ou <c>null</c> se não encontrada.</returns>
        Task<Academia> ObterPorCNPJAsync(string cnpj);
    }
}