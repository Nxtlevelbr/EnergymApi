using EnergymApi._2_Domain.Models;

namespace EnergymApi._2_Domain.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegistroExercicioRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registroExercicio"></param>
        /// <returns></returns>
        Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RegistroExercicio> ObterPorIdAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RegistroExercicio>> ObterTodosAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="academiaId"></param>
        /// <returns></returns>
        Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registroExercicio"></param>
        /// <returns></returns>
        Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletarAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        Task<object> ObterPorUsuarioIdAsync(int usuarioId);
    }
}