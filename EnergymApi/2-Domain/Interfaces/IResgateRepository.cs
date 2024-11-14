using EnergymApi._2_Domain.Models;

namespace EnergymApi._2_Domain.Interfaces
{
  
    public interface IResgateRepository
    {
        
        Task<Resgate> AdicionarAsync(Resgate resgate);
        
        Task<Resgate> ObterPorIdAsync(int id);
        
        Task<IEnumerable<Resgate>> ObterTodosAsync();
        
        Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId);
        
        Task<Resgate> AtualizarAsync(Resgate resgate);
        
        Task<bool> DeletarAsync(int id);
        
        Task<IEnumerable<Resgate>> ObterPorPremioIdAsync(int premioId);

    }
}