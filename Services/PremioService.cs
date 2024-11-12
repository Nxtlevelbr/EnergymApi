using EnergyApi.Models;
using EnergyApi.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            // Novos prêmios começam como ativos
            premio.Ativo = true;
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

        public Task<IEnumerable<Premio>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false)
        {
            var premios = await _premioRepository.ObterTodosAsync();
            return incluirInativos ? premios : premios.Where(p => p.Ativo);
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

        public async Task<bool> AtivarOuDesativarPremioAsync(int id, bool ativar)
        {
            var premio = await _premioRepository.ObterPorIdAsync(id);
            if (premio == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            premio.Ativo = ativar;
            await _premioRepository.AtualizarAsync(premio);
            return true;
        }
    }
}
