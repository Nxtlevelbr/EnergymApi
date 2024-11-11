using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Repositories
{
    public interface IAcademiaRepository
    {
        Task<Academia> AdicionarAsync(Academia academia);
        Task<Academia> ObterPorIdAsync(int id);
        Task<IEnumerable<Academia>> ObterTodosAsync();
        Task<Academia> AtualizarAsync(Academia academia);
        Task<bool> DeletarAsync(int id);
        Task<Academia> ObterPorCNPJAsync(string cnpj);
    }
}