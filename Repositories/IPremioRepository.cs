using EnergyApi.Models;

public interface IPremioRepository
{
    Task<Premio> AdicionarAsync(Premio premio);
    Task<Premio> ObterPorIdAsync(int id);
    Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false);
    Task<Premio> AtualizarAsync(Premio premio);
    Task<bool> DeletarAsync(int id);
}