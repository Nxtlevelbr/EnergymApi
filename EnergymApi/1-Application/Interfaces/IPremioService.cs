using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de prêmios.
    /// </summary>
    public interface IPremioService
    {
        /// <summary>
        /// Adiciona um novo prêmio.
        /// </summary>
        /// <param name="premio">Prêmio a ser adicionado.</param>
        /// <returns>O prêmio adicionado.</returns>
        Task<Premio> AdicionarAsync(Premio premio);

        /// <summary>
        /// Obtém um prêmio pelo seu ID.
        /// </summary>
        /// <param name="id">ID do prêmio.</param>
        /// <returns>O prêmio encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Premio> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os prêmios cadastrados.
        /// </summary>
        /// <param name="incluirInativos">Determina se prêmios inativos devem ser incluídos.</param>
        /// <returns>Lista de prêmios.</returns>
        Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false);

        /// <summary>
        /// Atualiza os dados de um prêmio existente.
        /// </summary>
        /// <param name="premio">Prêmio com os dados atualizados.</param>
        /// <returns>O prêmio atualizado.</returns>
        Task<Premio> AtualizarAsync(Premio premio);

        /// <summary>
        /// Deleta um prêmio pelo seu ID.
        /// </summary>
        /// <param name="id">ID do prêmio a ser deletado.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Ativa ou desativa um prêmio com base no ID.
        /// </summary>
        /// <param name="id">ID do prêmio.</param>
        /// <param name="ativar">Determina se o prêmio deve ser ativado (<c>true</c>) ou desativado (<c>false</c>).</param>
        /// <returns><c>true</c> se a operação foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> AtivarOuDesativarPremioAsync(int id, bool ativar);
    }
}
