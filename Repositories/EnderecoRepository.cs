using EnergyApi.Data;
using EnergyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationDbContext _context;

        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> ObterPorIdAsync(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            return await _context.Enderecos.AsNoTracking().ToListAsync();
        }

        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            var enderecoExistente = await _context.Enderecos.FindAsync(endereco.Id);
            if (enderecoExistente == null) return null;

            _context.Entry(enderecoExistente).CurrentValues.SetValues(endereco);
            await _context.SaveChangesAsync();
            return enderecoExistente;
        }

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