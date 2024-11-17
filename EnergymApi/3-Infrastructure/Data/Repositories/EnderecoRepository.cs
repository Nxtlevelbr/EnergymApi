using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergymApi._3_Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Endereco"/>.
    /// </summary>
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="EnderecoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        /// <inheritdoc/>
        public async Task<Endereco> ObterPorIdAsync(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            return await _context.Enderecos
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            var enderecoExistente = await _context.Enderecos.FindAsync(endereco.Id);
            if (enderecoExistente == null) return null;

            _context.Entry(enderecoExistente).CurrentValues.SetValues(endereco);
            await _context.SaveChangesAsync();
            return enderecoExistente;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletarAsync(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return false;

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
