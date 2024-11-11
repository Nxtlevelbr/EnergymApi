using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public interface IRegistroExercicioService
    {
        Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio);
        Task<RegistroExercicio> ObterPorIdAsync(int id);
        Task<IEnumerable<RegistroExercicio>> ObterTodosAsync();
        Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId);
        Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio);
        Task<bool> DeletarAsync(int id);
    }
}