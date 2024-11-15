using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            return await _enderecoRepository.AdicionarAsync(endereco);
        }

        public async Task<Endereco> ObterPorIdAsync(int id)
        {
            var endereco = await _enderecoRepository.ObterPorIdAsync(id);
            if (endereco == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado.");
            }

            return endereco;
        }

        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            return await _enderecoRepository.ObterTodosAsync();
        }

        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            var enderecoExistente = await _enderecoRepository.ObterPorIdAsync(endereco.Id);
            if (enderecoExistente == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado.");
            }

            return await _enderecoRepository.AtualizarAsync(endereco);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var endereco = await _enderecoRepository.ObterPorIdAsync(id);
            if (endereco == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado.");
            }

            return await _enderecoRepository.DeletarAsync(id);
        }

    }
}