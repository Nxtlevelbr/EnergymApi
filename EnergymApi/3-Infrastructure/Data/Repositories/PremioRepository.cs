using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Premio"/>.
    /// </summary>
    public class PremioRepository : IPremioRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="PremioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public PremioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Premio> AdicionarAsync(Premio premio)
        {
            _context.Premios.Add(premio);
            await _context.SaveChangesAsync();
            return premio;
        }

        /// <inheritdoc/>
        public async Task<Premio> ObterPorIdAsync(int id)
        {
            return await _context.Premios
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false)
        {
            return await _context.Premios
                .AsNoTracking()
                .Where(p => incluirInativos || p.Ativo) // Considera prêmios ativos ou inativos
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Premio> AtualizarAsync(Premio premio)
        {
            var premioExistente = await _context.Premios.FindAsync(premio.Id);
            if (premioExistente == null) return null;

            _context.Entry(premioExistente).CurrentValues.SetValues(premio);
            await _context.SaveChangesAsync();
            return premioExistente;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletarAsync(int id)
        {
            var premio = await _context.Premios.FindAsync(id);
            if (premio == null) return false;

            _context.Premios.Remove(premio);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Premio>> ObterPremiosAtivosAsync()
        {
            return await _context.Premios
                .AsNoTracking()
                .Where(p => p.Ativo)
                .ToListAsync();
        }
    }
}
