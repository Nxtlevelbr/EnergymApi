using EnergyApi.Data;
using EnergyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Repositories
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
            return await _context.Premios.FindAsync(id);
        }

        public async Task<IEnumerable<Premio>> ObterTodosAsync()
        {
            return await _context.Premios.AsNoTracking().ToListAsync();
        }

        public async Task<Premio> AtualizarAsync(Premio premio)
        {
            var premioExistente = await _context.Premios.FindAsync(premio.Id);
            if (premioExistente == null) return null;

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
    }
}