using System;
using System.Linq; // Necessário para Sum
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

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
            var premio = await _premioRepository.ObterPorIdAsync(premioId);
            if (premio == null || !premio.Ativo)
            {
                throw new KeyNotFoundException("Prêmio não encontrado ou inativo.");
            }

            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            var registrosExercicio = await _registroExercicioRepository.ObterPorUsuarioIdAsync(usuarioId);
            var pontosAcumulados = registrosExercicio.Sum(re => re.Km);

            if (pontosAcumulados < premio.Pontos)
            {
                throw new InvalidOperationException("Pontos insuficientes para resgatar este prêmio.");
            }

            usuario.Pontos = pontosAcumulados - premio.Pontos;
            await _usuarioRepository.AtualizarAsync(usuario);

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
