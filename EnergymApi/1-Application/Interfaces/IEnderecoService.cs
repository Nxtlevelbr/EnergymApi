using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de endereços.
    /// </summary>
    public interface IEnderecoService
    {
        /// <summary>
        /// Adiciona um novo endereço.
        /// </summary>
        /// <param name="endereco">Endereço a ser adicionado.</param>
        /// <returns>O endereço adicionado.</returns>
        Task<Endereco> AdicionarAsync(Endereco endereco);

        /// <summary>
        /// Obtém um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço.</param>
        /// <returns>O endereço encontrado ou <c>null</c> se não encontrado.</returns>
        Task<Endereco> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os endereços cadastrados.
        /// </summary>
        /// <returns>Lista de endereços.</returns>
        Task<IEnumerable<Endereco>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="endereco">Endereço com os dados atualizados.</param>
        /// <returns>O endereço atualizado.</returns>
        Task<Endereco> AtualizarAsync(Endereco endereco);

        /// <summary>
        /// Deleta um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser deletado.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);
    }
}