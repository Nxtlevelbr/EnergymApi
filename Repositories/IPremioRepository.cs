using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Repositories
{
    public interface IPremioRepository
    {
        Task<Premio> AdicionarAsync(Premio premio);
        Task<Premio> ObterPorIdAsync(int id);
        Task<IEnumerable<Premio>> ObterTodosAsync();
        Task<Premio> AtualizarAsync(Premio premio);
        Task<bool> DeletarAsync(int id);
    }
}