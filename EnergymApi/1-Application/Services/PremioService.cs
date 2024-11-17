using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergyApi.Services
{
    /// <summary>
    /// Serviço para gerenciar prêmios.
    /// </summary>
    public class PremioService : IPremioService
    {
        private readonly IPremioRepository _premioRepository;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="PremioService"/>.
        /// </summary>
        /// <param name="premioRepository">Repositório de prêmios.</param>
        public PremioService(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        /// <inheritdoc />
        public async Task<Premio> AdicionarAsync(Premio premio)
        {
            premio.Ativo = true; // Ativa o prêmio ao adicioná-lo
            return await _premioRepository.AdicionarAsync(premio);
        }

        /// <inheritdoc />
        public async Task<Premio> ObterPorIdAsync(int id)
        {
            var premio = await _premioRepository.ObterPorIdAsync(id);
            if (premio == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            return premio;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false)
        {
            return await _premioRepository.ObterTodosAsync(incluirInativos);
        }

        /// <inheritdoc />
        public async Task<Premio> AtualizarAsync(Premio premio)
        {
            var premioExistente = await _premioRepository.ObterPorIdAsync(premio.Id);
            if (premioExistente == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            return await _premioRepository.AtualizarAsync(premio);
        }

        /// <inheritdoc />
        public async Task<bool> DeletarAsync(int id)
        {
            return await _premioRepository.DeletarAsync(id);
        }

        /// <inheritdoc />
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
