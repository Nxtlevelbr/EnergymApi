using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Services
{
    
    public class AcademiaService : IAcademiaService
    {
        private readonly IAcademiaRepository _academiaRepository;

       
        public AcademiaService(IAcademiaRepository academiaRepository)
        {
            _academiaRepository = academiaRepository;
        }
        
        
        public async Task<Academia> AdicionarAsync(Academia academia)
        {
            var academiaExistente = await _academiaRepository.ObterPorCnpjAsync(academia.CNPJ);
            if (academiaExistente != null)
            {
                throw new InvalidOperationException("CNPJ já cadastrado.");
            }

            return await _academiaRepository.AdicionarAsync(academia);
        }

        
       
        public async Task<Academia> ObterPorIdAsync(int id)
        {
            var academia = await _academiaRepository.ObterPorIdAsync(id);
            if (academia == null)
            {
                throw new KeyNotFoundException("Academia não encontrada.");
            }

            return academia;
        }

        
        public async Task<IEnumerable<Academia>> ObterTodosAsync()
        {
            return await _academiaRepository.ObterTodosAsync();
        }

        
        public async Task<Academia> AtualizarAsync(Academia academia)
        {
            var academiaExistente = await _academiaRepository.ObterPorIdAsync(academia.Id);
            if (academiaExistente == null)
            {
                throw new KeyNotFoundException("Academia não encontrada.");
            }

            return await _academiaRepository.AtualizarAsync(academia);
        }
        
        public async Task<bool> DeletarAsync(int id)
        {
            return await _academiaRepository.DeletarAsync(id);
        }

       
        public async Task<Academia> ObterPorCNPJAsync(string cnpj)
        {
            return await _academiaRepository.ObterPorCnpjAsync(cnpj);
        }
    }
}
