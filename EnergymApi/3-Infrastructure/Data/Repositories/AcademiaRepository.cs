using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    public class AcademiaRepository : IAcademiaRepository
    {
        private readonly ApplicationDbContext _context;

        public AcademiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Academia> AdicionarAsync(Academia academia)
        {
            _context.Academias.Add(academia);
            await _context.SaveChangesAsync();
            return academia;
        }

        public async Task<Academia> ObterPorIdAsync(int id)
        {
            return await _context.Academias
                .Include(a => a.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Academia>> ObterTodosAsync()
        {
            return await _context.Academias
                .Include(a => a.Endereco)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Academia> AtualizarAsync(Academia academia)
        {
            var academiaExistente = await _context.Academias.FindAsync(academia.Id);
            if (academiaExistente == null) return null;

            _context.Entry(academiaExistente).CurrentValues.SetValues(academia);
            await _context.SaveChangesAsync();
            return academiaExistente;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var academia = await _context.Academias.FindAsync(id);
            if (academia == null) return false;

            _context.Academias.Remove(academia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Academia> ObterPorCnpjAsync(string cnpj)
        {
            return await _context.Academias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CNPJ == cnpj);
        }
    }
}
