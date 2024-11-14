using EnergyApi.Data;
using EnergyApi.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyApi.Repositories
{
    public class ResgateRepository : IResgateRepository
    {
        private readonly ApplicationDbContext _context;

        public ResgateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Resgate> AdicionarAsync(Resgate resgate)
        {
            _context.Resgates.Add(resgate);
            await _context.SaveChangesAsync();
            return resgate;
        }

        public async Task<Resgate> ObterPorIdAsync(int id)
        {
            return await _context.Resgates
                .Include(r => r.Usuario)
                .Include(r => r.Premio)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Resgate>> ObterTodosAsync()
        {
            return await _context.Resgates
                .Include(r => r.Usuario)
                .Include(r => r.Premio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.Resgates
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Premio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Resgate>> ObterPorPremioIdAsync(int premioId)
        {
            return await _context.Resgates
                .Where(r => r.PremioId == premioId)
                .Include(r => r.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Resgate> AtualizarAsync(Resgate resgate)
        {
            var resgateExistente = await _context.Resgates.FindAsync(resgate.Id);
            if (resgateExistente == null) return null;

            _context.Entry(resgateExistente).CurrentValues.SetValues(resgate);
            await _context.SaveChangesAsync();
            return resgateExistente;
        }

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
