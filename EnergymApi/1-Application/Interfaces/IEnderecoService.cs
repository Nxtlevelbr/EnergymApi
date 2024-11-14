using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> AdicionarAsync(Endereco endereco);
        Task<Endereco> ObterPorIdAsync(int id);
        Task<IEnumerable<Endereco>> ObterTodosAsync();
        Task<Endereco> AtualizarAsync(Endereco endereco);
        Task<bool> DeletarAsync(int id);
    }
}