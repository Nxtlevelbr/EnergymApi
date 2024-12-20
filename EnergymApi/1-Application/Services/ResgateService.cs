
using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergyApi.Services
{
    /// <summary>
    /// Serviço para gerenciar resgates de prêmios.
    /// </summary>
    public class ResgateService : IResgateService
    {
        private readonly IResgateRepository _resgateRepository;
        private readonly IPremioRepository _premioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRegistroExercicioRepository _registroExercicioRepository;

        /// <summary>
        /// Construtor do ResgateService.
        /// </summary>
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

        /// <inheritdoc />
        public async Task<Resgate> AdicionarAsync(Resgate resgate)
        {
            return await _resgateRepository.AdicionarAsync(resgate);
        }

        /// <inheritdoc />
        public async Task<Resgate> ObterPorIdAsync(int id)
        {
            var resgate = await _resgateRepository.ObterPorIdAsync(id);
            if (resgate == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return resgate;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Resgate>> ObterTodosAsync()
        {
            return await _resgateRepository.ObterTodosAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Resgate>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _resgateRepository.ObterPorUsuarioAsync(usuarioId);
        }

        /// <inheritdoc />
        public async Task<Resgate> AtualizarAsync(Resgate resgate)
        {
            var resgateExistente = await _resgateRepository.ObterPorIdAsync(resgate.Id);
            if (resgateExistente == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return await _resgateRepository.AtualizarAsync(resgate);
        }

        /// <inheritdoc />
        public async Task<bool> DeletarAsync(int id)
        {
            var resgateExistente = await _resgateRepository.ObterPorIdAsync(id);
            if (resgateExistente == null)
            {
                throw new KeyNotFoundException("Resgate não encontrado.");
            }

            return await _resgateRepository.DeletarAsync(id);
        }

        /// <summary>
        /// Registra um resgate de prêmio.
        /// </summary>
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

            var registrosExercicio = await _registroExercicioRepository.ObterPorUsuarioAsync(usuarioId) ?? new List<RegistroExercicio>();
            var pontosAcumulados = registrosExercicio.Sum(re => (int)re.Km);

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
