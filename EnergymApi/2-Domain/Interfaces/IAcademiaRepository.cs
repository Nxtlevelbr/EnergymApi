using EnergymApi._2_Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de Academias.
    /// </summary>
    public interface IAcademiaRepository
    {
        /// <summary>
        /// Adiciona uma nova academia ao banco de dados.
        /// </summary>
        /// <param name="academia">Objeto <see cref="Academia"/> contendo os dados da nova academia.</param>
        /// <returns>O objeto <see cref="Academia"/> adicionado com o ID gerado.</returns>
        Task<Academia> AdicionarAsync(Academia academia);

        /// <summary>
        /// Obtém uma academia pelo seu ID.
        /// </summary>
        /// <param name="id">ID da academia.</param>
        /// <returns>Objeto <see cref="Academia"/> correspondente ou null se não encontrado.</returns>
        Task<Academia> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todas as academias cadastradas.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Academia"/>.</returns>
        Task<IEnumerable<Academia>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de uma academia existente.
        /// </summary>
        /// <param name="academia">Objeto <see cref="Academia"/> com os dados atualizados.</param>
        /// <returns>Objeto <see cref="Academia"/> atualizado.</returns>
        Task<Academia> AtualizarAsync(Academia academia);

        /// <summary>
        /// Deleta uma academia com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID da academia a ser deletada.</param>
        /// <returns>True se a exclusão foi bem-sucedida, false caso contrário.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém uma academia pelo seu CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ da academia.</param>
        /// <returns>Objeto <see cref="Academia"/> correspondente ou null se não encontrado.</returns>
        Task<Academia> ObterPorCnpjAsync(string cnpj);
    }
}
