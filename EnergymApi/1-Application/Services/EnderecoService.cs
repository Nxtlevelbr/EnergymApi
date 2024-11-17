using EnergymApi._1_Application.Interfaces;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Services
{
    /// <summary>
    /// Serviço para gerenciar endereços.
    /// </summary>
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="EnderecoService"/>.
        /// </summary>
        /// <param name="enderecoRepository">Repositório de endereços.</param>
        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        /// <inheritdoc />
        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            return await _enderecoRepository.AdicionarAsync(endereco);
        }

        /// <inheritdoc />
        public async Task<Endereco> ObterPorIdAsync(int id)
        {
            var endereco = await _enderecoRepository.ObterPorIdAsync(id);
            if (endereco == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado.");
            }

            return endereco;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            return await _enderecoRepository.ObterTodosAsync();
        }

        /// <inheritdoc />
        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            var enderecoExistente = await _enderecoRepository.ObterPorIdAsync(endereco.Id);
            if (enderecoExistente == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado.");
            }

            return await _enderecoRepository.AtualizarAsync(endereco);
        }

        /// <inheritdoc />
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
