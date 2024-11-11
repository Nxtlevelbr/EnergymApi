using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Repositories
{
    public interface IEnderecoRepository
    {
        Task<Endereco> AdicionarAsync(Endereco endereco);
        Task<Endereco> ObterPorIdAsync(int id);
        Task<IEnumerable<Endereco>> ObterTodosAsync();
        Task<Endereco> AtualizarAsync(Endereco endereco);
        Task<bool> DeletarAsync(int id);
    }
}