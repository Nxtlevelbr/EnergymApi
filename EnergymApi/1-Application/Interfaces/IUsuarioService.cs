using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AdicionarAsync(Usuario usuario);
        Task<Usuario> ObterPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task<Usuario> AtualizarAsync(Usuario usuario);
        Task<bool> DeletarAsync(int id);
        Task<Usuario> ObterPorUsernameAsync(string username);
        Task<Usuario> ObterPorEmailAsync(string email);
        Task<Usuario> ObterPorCPFAsync(string cpf);
    }
}