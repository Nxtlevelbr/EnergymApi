using EnergyApi.Models;
using EnergyApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class ResgateService : IResgateService
    {
        private readonly IResgateRepository _resgateRepository;
        private readonly IPremioRepository _premioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRegistroExercicioRepository _registroExercicioRepository;

        public ResgateService(
            IResgateRepository resgateRepository,
            IPremioRepository premioRepository,
            IUsuarioRepository usuarioRepository,
            IRegistroExercicioRepository registroExercicioRepository)
        {
            _resgateRepository = resgateRepository;
            _premioRepository = premioRepository;
            _usuarioRepository = usuarioRepository;
            _registroExercicioRepository = registroExercicioRepository;
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
            // Validar se o prêmio existe e está ativo
            var premio = await _premioRepository.ObterPorIdAsync(premioId);
            if (premio == null || !premio.Ativo)
            {
                throw new KeyNotFoundException("Prêmio não encontrado ou inativo.");
            }

            // Validar se o usuário existe
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            // Verificar os registros de exercícios para calcular pontos acumulados
            var registrosExercicios = await _registroExercicioRepository.ObterPorUsuarioAsync(usuarioId);
            var pontosAcumulados = registrosExercicios.Sum(re => re.Km); // 1 ponto por Km

            if (pontosAcumulados <= 0 || pontosAcumulados < premio.Pontos)
            {
                throw new InvalidOperationException("Pontos insuficientes para resgatar este prêmio.");
            }

            // Deduzir os pontos acumulados e atualizar o usuário
            usuario.Pontos -= premio.Pontos;
            await _usuarioRepository.AtualizarAsync(usuario);

            // Criar e registrar o resgate
            var resgate = new Resgate
            {
                UsuarioId = usuarioId,
                PremioId = premioId,
                DataHora = DateTime.UtcNow
            };

            return await _resgateRepository.AdicionarAsync(resgate);
        }
    }
}
