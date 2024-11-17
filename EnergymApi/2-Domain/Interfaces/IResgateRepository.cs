using EnergymApi._2_Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de resgates.
    /// </summary>
    public interface IResgateRepository
    {
        /// <summary>
        /// Adiciona um novo resgate ao banco de dados.
        /// </summary>
        /// <param name="resgate">Objeto <see cref="Resgate"/> contendo os dados do novo resgate.</param>
        /// <returns>O objeto <see cref="Resgate"/> adicionado com o ID gerado.</returns>
        Task<Resgate> AdicionarAsync(Resgate resgate);

        /// <summary>
        /// Obtém um resgate pelo seu ID.
        /// </summary>
        /// <param name="id">ID do resgate.</param>
        /// <returns>Objeto <see cref="Resgate"/> correspondente ou null se não encontrado.</returns>
        Task<Resgate> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os resgates cadastrados.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Resgate"/>.</returns>
        Task<IEnumerable<Resgate>> ObterTodosAsync();

        /// <summary>
        /// Obtém todos os resgates realizados por um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de objetos <see cref="Resgate"/> associados ao usuário.</returns>
        Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId);

        /// <summary>
        /// Atualiza os dados de um resgate existente.
        /// </summary>
        /// <param name="resgate">Objeto <see cref="Resgate"/> com os dados atualizados.</param>
        /// <returns>Objeto <see cref="Resgate"/> atualizado.</returns>
        Task<Resgate> AtualizarAsync(Resgate resgate);

        /// <summary>
        /// Deleta um resgate com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do resgate a ser deletado.</param>
        /// <returns>True se a exclusão foi bem-sucedida, false caso contrário.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém todos os resgates associados a um prêmio específico.
        /// </summary>
        /// <param name="premioId">ID do prêmio.</param>
        /// <returns>Lista de objetos <see cref="Resgate"/> associados ao prêmio.</returns>
        Task<IEnumerable<Resgate>> ObterPorPremioIdAsync(int premioId);
    }
}
