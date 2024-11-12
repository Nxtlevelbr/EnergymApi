using EnergyApi.Models;
using EnergyApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class ResgateService : IResgateService
    {
        private readonly IResgateRepository _resgateRepository;
        private readonly IPremioRepository _premioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ResgateService(
            IResgateRepository resgateRepository, 
            IPremioRepository premioRepository, 
            IUsuarioRepository usuarioRepository)
        {
            _resgateRepository = resgateRepository;
            _premioRepository = premioRepository;
            _usuarioRepository = usuarioRepository;
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
            var resgateExistente = await _resgateRepository.ObterPorIdAsync(id);
            if (resgateExistente == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return await _resgateRepository.DeletarAsync(id);
        }

        public async Task<Resgate> RegistrarResgate(int usuarioId, int premioId)
        {
            // Validar se o prêmio existe
            var premio = await _premioRepository.ObterPorIdAsync(premioId);
            if (premio == null)
            {
                throw new KeyNotFoundException("Prêmio não encontrado.");
            }

            // Validar se o usuário existe
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            // Validar se o usuário tem pontos suficientes para ativar o prêmio
            if (usuario.Pontos < premio.Pontos)
            {
                throw new InvalidOperationException("Pontos insuficientes para resgatar este prêmio.");
            }

            // Se o usuário tiver pontos suficientes, o prêmio é considerado ativo
            premio.Ativo = true;

            // Atualizar os pontos do usuário
            usuario.Pontos -= premio.Pontos;
            await _usuarioRepository.AtualizarAsync(usuario);

            // Criar o resgate
            var resgate = new Resgate
            {
                UsuarioId = usuarioId,
                PremioId = premioId,
                DataHora = DateTime.UtcNow
            };

            // Salvar o resgate
            return await _resgateRepository.AdicionarAsync(resgate);
        }
    }
}
