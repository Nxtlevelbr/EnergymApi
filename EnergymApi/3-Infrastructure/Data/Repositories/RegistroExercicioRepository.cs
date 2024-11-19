using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="RegistroExercicio"/>.
    /// </summary>
    public class RegistroExercicioRepository : IRegistroExercicioRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="RegistroExercicioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public RegistroExercicioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio)
        {
            _context.RegistrosExercicios.Add(registroExercicio);
            await _context.SaveChangesAsync();
            return registroExercicio;
        }

        /// <inheritdoc/>
        public async Task<RegistroExercicio> ObterPorIdAsync(int id)
        {
            return await _context.RegistrosExercicios
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RegistroExercicio>> ObterTodosAsync()
        {
            return await _context.RegistrosExercicios
                .Include(r => r.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.RegistrosExercicios
                .Where(r => r.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc />
        public Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio)
        {
            var registroExistente = await _context.RegistrosExercicios.FindAsync(registroExercicio.Id);
            if (registroExistente == null) return null;

            _context.Entry(registroExistente).CurrentValues.SetValues(registroExercicio);
            await _context.SaveChangesAsync();
            return registroExistente;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletarAsync(int id)
        {
            var registro = await _context.RegistrosExercicios.FindAsync(id);
            if (registro == null) return false;

            _context.RegistrosExercicios.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.RegistrosExercicios
                .Where(r => r.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
