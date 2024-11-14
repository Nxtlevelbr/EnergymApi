
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

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

        public async Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false)
        {
            return await _premioRepository.ObterTodosAsync(incluirInativos);
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

        public Task<bool> AtivarOuDesativarPremioAsync(int id, bool ativar)
        {
            throw new NotImplementedException();
        }
    }
}