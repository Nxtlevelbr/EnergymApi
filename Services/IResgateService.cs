using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public interface IResgateService
    {
        Task<Resgate> AdicionarAsync(Resgate resgate);
        Task<Resgate> ObterPorIdAsync(int id);
        Task<IEnumerable<Resgate>> ObterTodosAsync();
        Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId);
        Task<Resgate> AtualizarAsync(Resgate resgate);
        Task<bool> DeletarAsync(int id);
    }
}