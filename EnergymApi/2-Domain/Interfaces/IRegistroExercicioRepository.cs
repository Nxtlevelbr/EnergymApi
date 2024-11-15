using EnergymApi._2_Domain.Models;

namespace EnergymApi._2_Domain.Interfaces
{
    public interface IRegistroExercicioRepository
    {
        Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio);
        Task<RegistroExercicio> ObterPorIdAsync(int id);
        Task<IEnumerable<RegistroExercicio>> ObterTodosAsync();
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId);
        Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio);
        Task<bool> DeletarAsync(int id);
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioIdAsync(int usuarioId);
    }
}