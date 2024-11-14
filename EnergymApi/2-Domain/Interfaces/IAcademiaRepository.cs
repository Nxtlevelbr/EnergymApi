using EnergymApi._2_Domain.Models;

namespace EnergymApi._2_Domain.Interfaces
{
    public interface IAcademiaRepository
    {
        Task<Academia> AdicionarAsync(Academia academia);
        
        Task<Academia> ObterPorIdAsync(int id);
        
        Task<IEnumerable<Academia>> ObterTodosAsync();
        
        Task<Academia> AtualizarAsync(Academia academia);
       
        Task<bool> DeletarAsync(int id);
        
        Task<Academia> ObterPorCnpjAsync(string cnpj);
    }
}