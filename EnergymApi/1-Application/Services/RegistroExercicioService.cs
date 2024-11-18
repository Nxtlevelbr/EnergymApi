using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Services
{
    /// <summary>
    /// Serviço para gerenciar registros de exercícios.
    /// </summary>
    public class RegistroExercicioService : IRegistroExercicioService
    {
        private readonly IRegistroExercicioRepository _registroExercicioRepository;

        /// <summary>
        /// Inicializa uma nova instância de RegistroExercicioService.
        /// </summary>
        /// <param name="registroExercicioRepository">Repositório de registros de exercícios.</param>
        public RegistroExercicioService(IRegistroExercicioRepository registroExercicioRepository)
        {
            _registroExercicioRepository = registroExercicioRepository;
        }

        /// <inheritdoc />
        public async Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio)
        {
            return await _registroExercicioRepository.AdicionarAsync(registroExercicio);
        }


        /// <inheritdoc />
        public async Task<RegistroExercicio> ObterPorIdAsync(int id)
        {
            var registro = await _registroExercicioRepository.ObterPorIdAsync(id);
            if (registro == null)
            {
                throw new KeyNotFoundException("Registro de exercício não encontrado.");
            }

            return registro;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RegistroExercicio>> ObterTodosAsync()
        {
            return await _registroExercicioRepository.ObterTodosAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _registroExercicioRepository.ObterPorUsuarioAsync(usuarioId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId)
        {
            return await _registroExercicioRepository.ObterPorAcademiaAsync(academiaId);
        }

        /// <inheritdoc />
        public async Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio)
        {
            var registroExistente = await _registroExercicioRepository.ObterPorIdAsync(registroExercicio.Id);
            if (registroExistente == null)
            {
                throw new KeyNotFoundException("Registro de exercício não encontrado.");
            }

            return await _registroExercicioRepository.AtualizarAsync(registroExercicio);
        }

        /// <inheritdoc />
        public async Task<bool> DeletarAsync(int id)
        {
            return await _registroExercicioRepository.DeletarAsync(id);
        }
    }
}
