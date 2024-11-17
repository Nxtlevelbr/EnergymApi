using EnergymApi._2_Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de Endereços.
    /// </summary>
    public interface IEnderecoRepository
    {
        /// <summary>
        /// Adiciona um novo endereço ao banco de dados.
        /// </summary>
        /// <param name="endereco">Objeto <see cref="Endereco"/> contendo os dados do novo endereço.</param>
        /// <returns>O objeto <see cref="Endereco"/> adicionado com o ID gerado.</returns>
        Task<Endereco> AdicionarAsync(Endereco endereco);

        /// <summary>
        /// Obtém um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço.</param>
        /// <returns>Objeto <see cref="Endereco"/> correspondente ou null se não encontrado.</returns>
        Task<Endereco> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os endereços cadastrados.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Endereco"/>.</returns>
        Task<IEnumerable<Endereco>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="endereco">Objeto <see cref="Endereco"/> com os dados atualizados.</param>
        /// <returns>Objeto <see cref="Endereco"/> atualizado.</returns>
        Task<Endereco> AtualizarAsync(Endereco endereco);

        /// <summary>
        /// Deleta um endereço com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do endereço a ser deletado.</param>
        /// <returns>True se a exclusão foi bem-sucedida, false caso contrário.</returns>
        Task<bool> DeletarAsync(int id);
    }
}