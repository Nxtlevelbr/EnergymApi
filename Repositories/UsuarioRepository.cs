using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EnergyApi.Data;
using EnergyApi.Models;
using EnergyApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Usuario> AdicionarAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        throw new NotImplementedException();
    }

    Task<Usuario> IUsuarioRepository.AtualizarAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletarAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> ObterPorUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> ObterPorEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> ObterPorCPFAsync(string cpf)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> ObterPorUsuarioIdAsync(int usuarioId)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }
}