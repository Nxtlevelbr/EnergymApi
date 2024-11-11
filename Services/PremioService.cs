using EnergyApi.Models;
using EnergyApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class PremioService : IPremioService
    {
        private readonly IPremioRepository _premioRepository;

        public PremioService(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        public async Task<Premio> AdicionarAsync(Premio premio)
        {
            return await _premioRepository.AdicionarAsync(premio);
        }

        public async Task<Premio> ObterPorIdAsync(int id)
        {
            var premio = await _premioRepository.ObterPorIdAsync(id);
            if (premio == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            return premio;
        }

        public async Task<IEnumerable<Premio>> ObterTodosAsync()
        {
            return await _premioRepository.ObterTodosAsync();
        }

        public async Task<Premio> AtualizarAsync(Premio premio)
        {
            var premioExistente = await _premioRepository.ObterPorIdAsync(premio.Id);
            if (premioExistente == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            return await _premioRepository.AtualizarAsync(premio);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _premioRepository.DeletarAsync(id);
        }
    }
}