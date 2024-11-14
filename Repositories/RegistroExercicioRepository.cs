using EnergyApi.Data;
using EnergyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyApi.Repositories
{
    public class RegistroExercicioRepository : IRegistroExercicioRepository
    {
        private readonly ApplicationDbContext _context;

        public RegistroExercicioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio)
        {
            _context.RegistrosExercicios.Add(registroExercicio);
            await _context.SaveChangesAsync();
            return registroExercicio;
        }

        public async Task<RegistroExercicio> ObterPorIdAsync(int id)
        {
            return await _context.RegistrosExercicios
                .Include(r => r.Usuario)
                .Include(r => r.Academia)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterTodosAsync()
        {
            return await _context.RegistrosExercicios
                .Include(r => r.Usuario)
                .Include(r => r.Academia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.RegistrosExercicios
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Academia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId)
        {
            return await _context.RegistrosExercicios
                .Where(r => r.AcademiaId == academiaId)
                .Include(r => r.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio)
        {
            var registroExistente = await _context.RegistrosExercicios.FindAsync(registroExercicio.Id);
            if (registroExistente == null) return null;

            _context.Entry(registroExistente).CurrentValues.SetValues(registroExercicio);
            await _context.SaveChangesAsync();
            return registroExistente;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var registro = await _context.RegistrosExercicios.FindAsync(id);
            if (registro == null) return false;

            _context.RegistrosExercicios.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<object> ObterPorUsuarioIdAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ObterPontosTotaisPorUsuarioAsync(int usuarioId)
        {
            var totalKm = await _context.RegistrosExercicios
                .Where(r => r.UsuarioId == usuarioId)
                .SumAsync(r => r.Km); // Km é double

            // Corrigir a conversão de double para int
            return (int)totalKm;
        }

    }
}
