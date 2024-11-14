using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    public class PremioRepository : IPremioRepository
    {
        private readonly ApplicationDbContext _context;

        public PremioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Premio> AdicionarAsync(Premio premio)
        {
            _context.Premios.Add(premio);
            await _context.SaveChangesAsync();
            return premio;
        }

        public async Task<Premio> ObterPorIdAsync(int id)
        {
            return await _context.Premios
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false)
        {
            return await _context.Premios
                .AsNoTracking()
                .Where(p => incluirInativos || p.Ativo) // Trabalha diretamente com bool
                .ToListAsync();
        }

        public async Task<Premio> AtualizarAsync(Premio premio)
        {
            var premioExistente = await _context.Premios.FindAsync(premio.Id);
            if (premioExistente == null) return null;

            // Atualiza os valores
            _context.Entry(premioExistente).CurrentValues.SetValues(premio);
            await _context.SaveChangesAsync();
            return premioExistente;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var premio = await _context.Premios.FindAsync(id);
            if (premio == null) return false;

            _context.Premios.Remove(premio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Premio>> ObterPremiosAtivosAsync()
        {
            return await _context.Premios
                .AsNoTracking()
                .Where(p => p.Ativo) // Apenas prêmios ativos
                .ToListAsync();
        }
    }
}
