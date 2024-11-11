using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public interface IPremioService
    {
        Task<Premio> AdicionarAsync(Premio premio);
        Task<Premio> ObterPorIdAsync(int id);
        Task<IEnumerable<Premio>> ObterTodosAsync();
        Task<Premio> AtualizarAsync(Premio premio);
        Task<bool> DeletarAsync(int id);
    }
}