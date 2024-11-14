using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    public interface IPremioService
    {
        Task<Premio> AdicionarAsync(Premio premio);
        Task<Premio> ObterPorIdAsync(int id);
        Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false); // Permite incluir prêmios inativos opcionalmente
        Task<Premio> AtualizarAsync(Premio premio);
        Task<bool> DeletarAsync(int id);
        Task<bool> AtivarOuDesativarPremioAsync(int id, bool ativar); // Método para ativar/desativar prêmios
    }
}