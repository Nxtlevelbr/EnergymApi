using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de registros de exercícios.
    /// </summary>
    public interface IRegistroExercicioService
    {
        /// <summary>
        /// Adiciona um novo registro de exercício.
        /// </summary>
        /// <param name="registroExercicio">Registro de exercício a ser adicionado.</param>
        /// <returns>O registro de exercício adicionado.</returns>
        Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio);

        /// <summary>
        /// Obtém um registro de exercício pelo seu ID.
        /// </summary>
        /// <param name="id">ID do registro de exercício.</param>
        /// <returns>O registro de exercício encontrado ou <c>null</c> se não encontrado.</returns>
        Task<RegistroExercicio> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os registros de exercícios cadastrados.
        /// </summary>
        /// <returns>Lista de registros de exercícios.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterTodosAsync();

        /// <summary>
        /// Obtém todos os registros de exercícios de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de registros de exercícios do usuário.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId);

        /// <summary>
        /// Obtém todos os registros de exercícios de uma academia específica.
        /// </summary>
        /// <param name="academiaId">ID da academia.</param>
        /// <returns>Lista de registros de exercícios da academia.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId);

        /// <summary>
        /// Atualiza os dados de um registro de exercício existente.
        /// </summary>
        /// <param name="registroExercicio">Registro de exercício com os dados atualizados.</param>
        /// <returns>O registro de exercício atualizado.</returns>
        Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio);

        /// <summary>
        /// Deleta um registro de exercício pelo seu ID.
        /// </summary>
        /// <param name="id">ID do registro de exercício a ser deletado.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        Task<bool> DeletarAsync(int id);
    }
}
