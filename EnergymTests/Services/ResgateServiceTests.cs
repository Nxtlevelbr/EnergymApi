using System;
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
    public class ResgateServiceTests
    {
        private readonly ResgateService _resgateService;
        private readonly Mock<IResgateRepository> _resgateRepositoryMock;
        private readonly Mock<IPremioRepository> _premioRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        public ResgateServiceTests()
        {
            _resgateRepositoryMock = new Mock<IResgateRepository>();
            _premioRepositoryMock = new Mock<IPremioRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _resgateService = new ResgateService(_resgateRepositoryMock.Object, _premioRepositoryMock.Object, _usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosResgates_DeveRetornarListaDeResgates()
        {
            var resgates = new List<Resgate>
            {
                new Resgate { Id = 1, UsuarioId = 1, PremioId = 1 },
                new Resgate { Id = 2, UsuarioId = 2, PremioId = 2 }
            };

            _resgateRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(resgates);

            var resultado = await _resgateService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _resgateRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterResgatePorId_DeveRetornarResgateQuandoEncontrado()
        {
            var resgate = new Resgate { Id = 1, UsuarioId = 1, PremioId = 1 };

            _resgateRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(resgate);

            var resultado = await _resgateService.ObterPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.UsuarioId);
            _resgateRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterResgatePorId_DeveLancarExcecaoQuandoResgateNaoEncontrado()
        {
            _resgateRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Resgate)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _resgateService.ObterPorIdAsync(99));
            _resgateRepositoryMock.Verify(repo => repo.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task RegistrarResgate_DeveRegistrarQuandoValido()
        {
            var usuarioId = 1;
            var premioId = 1;
            var usuario = new Usuario { Id = usuarioId, Pontos = 100 };
            var premio = new Premio { Id = premioId, Pontos = 50 };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(premioId)).ReturnsAsync(premio);
            _resgateRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Resgate>())).ReturnsAsync(new Resgate { Id = 1, UsuarioId = usuarioId, PremioId = premioId });

            var resultado = await _resgateService.RegistrarResgate(usuarioId, premioId);

            Assert.NotNull(resultado);
            Assert.Equal(usuarioId, resultado.UsuarioId);
            Assert.Equal(premioId, resultado.PremioId);
            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.Is<Usuario>(u => u.Pontos == 50)), Times.Once);
            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Once);
        }

        [Fact]
        public async Task RegistrarResgate_DeveLancarExcecaoQuandoPontosInsuficientes()
        {
            var usuarioId = 1;
            var premioId = 1;
            var usuario = new Usuario { Id = usuarioId, Pontos = 30 };
            var premio = new Premio { Id = premioId, Pontos = 50 };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(premioId)).ReturnsAsync(premio);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _resgateService.RegistrarResgate(usuarioId, premioId));

            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Usuario>()), Times.Never);
            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Never);
        }
    }
}
