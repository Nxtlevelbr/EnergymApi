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
    public class PremioServiceTests
    {
        private readonly PremioService _premioService;
        private readonly Mock<IPremioRepository> _premioRepositoryMock;

        public PremioServiceTests()
        {
            _premioRepositoryMock = new Mock<IPremioRepository>();
            _premioService = new PremioService(_premioRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosPremios_DeveRetornarListaDePremios()
        {
            var premios = new List<Premio>
            {
                new Premio { Id = 1, Descricao = "Premio 1", Ativo = true },
                new Premio { Id = 2, Descricao = "Premio 2", Ativo = true }
            };

            _premioRepositoryMock.Setup(repo => repo.ObterTodosAsync(false)).ReturnsAsync(premios);

            var resultado = await _premioService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _premioRepositoryMock.Verify(repo => repo.ObterTodosAsync(false), Times.Once);
        }

        [Fact]
        public async Task ObterPremioPorId_DeveRetornarPremioQuandoEncontrado()
        {
            var premio = new Premio { Id = 1, Descricao = "Premio 1", Ativo = true };

            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(premio);

            var resultado = await _premioService.ObterPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal("Premio 1", resultado.Descricao);
            _premioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterPremioPorId_DeveLancarExcecaoQuandoPremioNaoEncontrado()
        {
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(99)).ReturnsAsync((Premio)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _premioService.ObterPorIdAsync(99));
            _premioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task AdicionarPremio_DeveSalvarPremio()
        {
            var novoPremio = new Premio { Id = 3, Descricao = "Novo Premio", Ativo = true };

            _premioRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Premio>())).ReturnsAsync(novoPremio);

            var resultado = await _premioService.AdicionarAsync(novoPremio);

            Assert.NotNull(resultado);
            Assert.Equal("Novo Premio", resultado.Descricao);
            _premioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Premio>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarPremio_DeveAtualizarPremioQuandoEncontrado()
        {
            var premioExistente = new Premio { Id = 1, Descricao = "Premio Atual", Ativo = true };
            var premioAtualizado = new Premio { Id = 1, Descricao = "Premio Atualizado", Ativo = true };

            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(premioExistente);
            _premioRepositoryMock.Setup(repo => repo.AtualizarAsync(It.IsAny<Premio>())).ReturnsAsync(premioAtualizado);

            var resultado = await _premioService.AtualizarAsync(premioAtualizado);

            Assert.NotNull(resultado);
            Assert.Equal("Premio Atualizado", resultado.Descricao);
            _premioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
            _premioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Premio>()), Times.Once);
        }

        [Fact]
        public async Task DeletarPremio_DeveRemoverPremioQuandoEncontrado()
        {
            _premioRepositoryMock.Setup(repo => repo.DeletarAsync(1)).ReturnsAsync(true);

            var resultado = await _premioService.DeletarAsync(1);

            Assert.True(resultado);
            _premioRepositoryMock.Verify(repo => repo.DeletarAsync(1), Times.Once);
        }

        [Fact]
        public async Task AtivarOuDesativarPremio_DeveAtualizarStatus()
        {
            var premio = new Premio { Id = 1, Descricao = "Premio Teste", Ativo = false };

            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(premio);
            _premioRepositoryMock.Setup(repo => repo.AtualizarAsync(It.IsAny<Premio>())).ReturnsAsync(premio);

            var resultado = await _premioService.AtivarOuDesativarPremioAsync(1, true);

            Assert.True(resultado);
            Assert.True(premio.Ativo);
            _premioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
            _premioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Premio>()), Times.Once);
        }
    }
}
