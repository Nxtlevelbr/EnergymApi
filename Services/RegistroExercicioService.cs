using EnergyApi.Models;
using EnergyApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyApi.Services
{
    public class RegistroExercicioService : IRegistroExercicioService
    {
        private readonly IRegistroExercicioRepository _registroExercicioRepository;

        public RegistroExercicioService(IRegistroExercicioRepository registroExercicioRepository)
        {
            _registroExercicioRepository = registroExercicioRepository;
        }

        public async Task<RegistroExercicio> AdicionarAsync(RegistroExercicio registroExercicio)
        {
            return await _registroExercicioRepository.AdicionarAsync(registroExercicio);
        }

        public async Task<RegistroExercicio> ObterPorIdAsync(int id)
        {
            var registro = await _registroExercicioRepository.ObterPorIdAsync(id);
            if (registro == null)
            {
                throw new KeyNotFoundException("Registro de exercício não encontrado.");
            }

            return registro;
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterTodosAsync()
        {
            return await _registroExercicioRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _registroExercicioRepository.ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<IEnumerable<RegistroExercicio>> ObterPorAcademiaAsync(int academiaId)
        {
            return await _registroExercicioRepository.ObterPorAcademiaAsync(academiaId);
        }

        public async Task<RegistroExercicio> AtualizarAsync(RegistroExercicio registroExercicio)
        {
            var registroExistente = await _registroExercicioRepository.ObterPorIdAsync(registroExercicio.Id);
            if (registroExistente == null)
            {
                throw new KeyNotFoundException("Registro de exercício não encontrado.");
            }

            return await _registroExercicioRepository.AtualizarAsync(registroExercicio);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _registroExercicioRepository.DeletarAsync(id);
        }
    }
}
