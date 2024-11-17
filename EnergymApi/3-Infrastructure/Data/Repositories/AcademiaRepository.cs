using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para gerenciar operações relacionadas à entidade Academia.
    /// </summary>
    public class AcademiaRepository : IAcademiaRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="AcademiaRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public AcademiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova academia ao banco de dados.
        /// </summary>
        /// <param name="academia">A academia a ser adicionada.</param>
        /// <returns>A academia adicionada.</returns>
        public async Task<Academia> AdicionarAsync(Academia academia)
        {
            await _context.Academias.AddAsync(academia);
            await _context.SaveChangesAsync();
            return academia;
        }

        /// <summary>
        /// Obtém uma academia pelo seu ID.
        /// </summary>
        /// <param name="id">ID da academia.</param>
        /// <returns>A academia encontrada ou <c>null</c> se não encontrada.</returns>
        public async Task<Academia> ObterPorIdAsync(int id)
        {
            return await _context.Academias
                .Include(a => a.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Obtém todas as academias cadastradas.
        /// </summary>
        /// <returns>Lista de academias.</returns>
        public async Task<IEnumerable<Academia>> ObterTodosAsync()
        {
            return await _context.Academias
                .Include(a => a.Endereco)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Atualiza os dados de uma academia existente.
        /// </summary>
        /// <param name="academia">A academia com os dados atualizados.</param>
        /// <returns>A academia atualizada ou <c>null</c> se não encontrada.</returns>
        public async Task<Academia> AtualizarAsync(Academia academia)
        {
            var academiaExistente = await _context.Academias.FindAsync(academia.Id);
            if (academiaExistente == null) return null;

            _context.Entry(academiaExistente).CurrentValues.SetValues(academia);
            await _context.SaveChangesAsync();
            return academiaExistente;
        }

        /// <summary>
        /// Deleta uma academia pelo seu ID.
        /// </summary>
        /// <param name="id">ID da academia a ser deletada.</param>
        /// <returns><c>true</c> se a exclusão foi bem-sucedida, caso contrário, <c>false</c>.</returns>
        public async Task<bool> DeletarAsync(int id)
        {
            var academia = await _context.Academias.FindAsync(id);
            if (academia == null) return false;

            _context.Academias.Remove(academia);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Obtém uma academia pelo seu CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ da academia.</param>
        /// <returns>A academia encontrada ou <c>null</c> se não encontrada.</returns>
        public async Task<Academia> ObterPorCnpjAsync(string cnpj)
        {
            return await _context.Academias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CNPJ == cnpj);
        }
    }
}
