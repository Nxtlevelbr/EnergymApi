using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Resgate"/>.
    /// </summary>
    public class ResgateRepository : IResgateRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResgateRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public ResgateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Resgate> AdicionarAsync(Resgate resgate)
        {
            _context.Resgates.Add(resgate);
            await _context.SaveChangesAsync();
            return resgate;
        }

        /// <inheritdoc/>
        public async Task<Resgate> ObterPorIdAsync(int id)
        {
            return await _context.Resgates
                .Include(r => r.Usuario)
                .Include(r => r.Premio)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Resgate>> ObterTodosAsync()
        {
            return await _context.Resgates
                .Include(r => r.Usuario)
                .Include(r => r.Premio)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.Resgates
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Premio)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Resgate>> ObterPorPremioIdAsync(int premioId)
        {
            return await _context.Resgates
                .Where(r => r.PremioId == premioId)
                .Include(r => r.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Resgate> AtualizarAsync(Resgate resgate)
        {
            var resgateExistente = await _context.Resgates.FindAsync(resgate.Id);
            if (resgateExistente == null) return null;

            _context.Entry(resgateExistente).CurrentValues.SetValues(resgate);
            await _context.SaveChangesAsync();
            return resgateExistente;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletarAsync(int id)
        {
            var resgate = await _context.Resgates.FindAsync(id);
            if (resgate == null) return false;

            _context.Resgates.Remove(resgate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
