using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Usuario"/>.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <inheritdoc/>
        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Usuario> AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletarAsync(int id)
        {
            var usuario = await ObterPorIdAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<Usuario> ObterPorUsernameAsync(string username)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <inheritdoc/>
        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <inheritdoc/>
        public async Task<Usuario> ObterPorCpfAsync(string cpf)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.CPF == cpf);
        }

        /// <inheritdoc/>
        public async Task<Usuario> ObterPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == usuarioId);
        }
    }
}
