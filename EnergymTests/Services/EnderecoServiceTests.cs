using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using EnergyApi.Models;
using EnergyApi.Repositories;
using EnergyApi.Services;

namespace EnergymTests.Services
{
    public class EnderecoServiceTests
    {
        private readonly EnderecoService _enderecoService;
        private readonly Mock<IEnderecoRepository> _enderecoRepositoryMock;

        public EnderecoServiceTests()
        {
            _enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            _enderecoService = new EnderecoService(_enderecoRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosEnderecos_DeveRetornarListaDeEnderecos()
        {
            // Arrange
            var enderecos = new List<Endereco>
            {
                new Endereco { Id = 1, Rua = "Rua A" },
                new Endereco { Id = 2, Rua = "Rua B" }
            };

            _enderecoRepositoryMock.Setup(repo => repo.ObterTodosAsync())
                                   .ReturnsAsync(enderecos);

            // Act
            var resultado = await _enderecoService.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _enderecoRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterEnderecoPorId_DeveRetornarEnderecoQuandoEncontrado()
        {
            // Arrange
            var endereco = new Endereco { Id = 1, Rua = "Rua A" };

            _enderecoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1))
                                   .ReturnsAsync(endereco);

            // Act
            var resultado = await _enderecoService.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Rua A", resultado.Rua);
            _enderecoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterEnderecoPorId_DeveLancarExcecaoQuandoEnderecoNaoEncontrado()
        {
            // Arrange
            _enderecoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((Endereco)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _enderecoService.ObterPorIdAsync(99));
            _enderecoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task AdicionarEndereco_DeveSalvarEndereco()
        {
            // Arrange
            var endereco = new Endereco { Id = 1, Rua = "Rua Nova" };

            _enderecoRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Endereco>()))
                                   .ReturnsAsync(endereco);

            // Act
            var resultado = await _enderecoService.AdicionarAsync(endereco);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Rua Nova", resultado.Rua);
            _enderecoRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Endereco>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarEndereco_DeveAtualizarEnderecoQuandoEncontrado()
        {
            // Arrange
            var enderecoExistente = new Endereco { Id = 1, Rua = "Rua Antiga" };
            var enderecoAtualizado = new Endereco { Id = 1, Rua = "Rua Atualizada" };

            _enderecoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1))
                                   .ReturnsAsync(enderecoExistente);

            _enderecoRepositoryMock.Setup(repo => repo.AtualizarAsync(enderecoAtualizado))
                                   .ReturnsAsync(enderecoAtualizado);

            // Act
            var resultado = await _enderecoService.AtualizarAsync(enderecoAtualizado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Rua Atualizada", resultado.Rua);
            _enderecoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
            _enderecoRepositoryMock.Verify(repo => repo.AtualizarAsync(enderecoAtualizado), Times.Once);
        }

        [Fact]
        public async Task DeletarEndereco_DeveRemoverEnderecoQuandoEncontrado()
        {
            // Arrange
            var enderecoId = 1;
            var enderecoExistente = new Endereco { Id = enderecoId, Rua = "Rua Teste" };

            _enderecoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(enderecoId))
                                   .ReturnsAsync(enderecoExistente);

            _enderecoRepositoryMock.Setup(repo => repo.DeletarAsync(enderecoId))
                                   .ReturnsAsync(true);

            // Act
            var resultado = await _enderecoService.DeletarAsync(enderecoId);

            // Assert
            Assert.True(resultado);
            _enderecoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(enderecoId), Times.Once);
            _enderecoRepositoryMock.Verify(repo => repo.DeletarAsync(enderecoId), Times.Once);
        }

        [Fact]
        public async Task DeletarEndereco_DeveLancarExcecaoQuandoEnderecoNaoEncontrado()
        {
            // Arrange
            var enderecoId = 999;
            _enderecoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(enderecoId))
                                   .ReturnsAsync((Endereco)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _enderecoService.DeletarAsync(enderecoId));

            _enderecoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(enderecoId), Times.Once);
        }
    }
}
