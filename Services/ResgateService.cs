using EnergyApi.Models;
using EnergyApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class ResgateService : IResgateService
    {
        private readonly IResgateRepository _resgateRepository;

        public ResgateService(IResgateRepository resgateRepository)
        {
            _resgateRepository = resgateRepository;
        }

        public async Task<Resgate> AdicionarAsync(Resgate resgate)
        {
            return await _resgateRepository.AdicionarAsync(resgate);
        }

        public async Task<Resgate> ObterPorIdAsync(int id)
        {
            var resgate = await _resgateRepository.ObterPorIdAsync(id);
            if (resgate == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return resgate;
        }

        public async Task<IEnumerable<Resgate>> ObterTodosAsync()
        {
            return await _resgateRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _resgateRepository.ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<Resgate> AtualizarAsync(Resgate resgate)
        {
            var resgateExistente = await _resgateRepository.ObterPorIdAsync(resgate.Id);
            if (resgateExistente == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return await _resgateRepository.AtualizarAsync(resgate);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _resgateRepository.DeletarAsync(id);
        }
    }
}