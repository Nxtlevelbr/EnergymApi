using EnergymApi._2_Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de registros de exercícios.
    /// </summary>
    public interface IRegistroExercicioRepository
    {
        /// <summary>
        /// Adiciona um novo registro de exercício ao banco de dados.
        /// </summary>
        /// <param name="registroExercicio">Objeto <see cref="RegistroExercicio"/> contendo os dados do novo registro.</param>
        /// <returns>O objeto <see cref="RegistroExercicio"/> adicionado com o ID gerado.</returns>
        Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio);

        /// <summary>
        /// Obtém um registro de exercício pelo seu ID.
        /// </summary>
        /// <param name="id">ID do registro de exercício.</param>
        /// <returns>Objeto <see cref="RegistroExercicio"/> correspondente ou null se não encontrado.</returns>
        Task<RegistroExercicio> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém todos os registros de exercícios cadastrados.
        /// </summary>
        /// <returns>Lista de objetos <see cref="RegistroExercicio"/>.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterTodosAsync();

        /// <summary>
        /// Obtém todos os registros de exercícios de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de objetos <see cref="RegistroExercicio"/> associados ao usuário.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId);

        /// <summary>
        /// Obtém todos os registros de exercícios de uma academia específica.
        /// </summary>
        /// <param name="academiaId">ID da academia.</param>
        /// <returns>Lista de objetos <see cref="RegistroExercicio"/> associados à academia.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId);

        /// <summary>
        /// Atualiza os dados de um registro de exercício existente.
        /// </summary>
        /// <param name="registroExercicio">Objeto <see cref="RegistroExercicio"/> com os dados atualizados.</param>
        /// <returns>Objeto <see cref="RegistroExercicio"/> atualizado.</returns>
        Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio);

        /// <summary>
        /// Deleta um registro de exercício com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do registro de exercício a ser deletado.</param>
        /// <returns>True se a exclusão foi bem-sucedida, false caso contrário.</returns>
        Task<bool> DeletarAsync(int id);

        /// <summary>
        /// Obtém registros de exercício com base no ID do usuário.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de objetos <see cref="RegistroExercicio"/> associados ao usuário.</returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioIdAsync(int usuarioId);
    }
}
