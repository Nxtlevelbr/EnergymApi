using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAcademiaService
    {
        Task<Academia> AdicionarAsync(Academia academia);
        Task<Academia> ObterPorIdAsync(int id);
        Task<IEnumerable<Academia>> ObterTodosAsync();
        Task<Academia> AtualizarAsync(Academia academia);
        Task<bool> DeletarAsync(int id);
        Task<Academia> ObterPorCNPJAsync(string cnpj);
    }
}