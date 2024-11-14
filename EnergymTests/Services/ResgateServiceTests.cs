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
        private readonly Mock<IRegistroExercicioRepository> _registroExercicioRepositoryMock;

        public ResgateServiceTests()
        {
            _resgateRepositoryMock = new Mock<IResgateRepository>();
            _premioRepositoryMock = new Mock<IPremioRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _registroExercicioRepositoryMock = new Mock<IRegistroExercicioRepository>();

            _resgateService = new ResgateService(
                _resgateRepositoryMock.Object,
                _premioRepositoryMock.Object,
                _usuarioRepositoryMock.Object,
                _registroExercicioRepositoryMock.Object);
        }

        [Fact]
        public async Task RegistrarResgate_DeveRegistrarQuandoPontosAcumuladosSuficientes()
        {
            // Arrange
            var usuarioId = 1;
            var premioId = 1;
            var usuario = new Usuario { Id = usuarioId, Pontos = 0 }; // Base inicial
            var premio = new Premio { Id = premioId, Pontos = 50, Ativo = true };

            var registrosExercicio = new List<RegistroExercicio>
            {
                new RegistroExercicio { Id = 1, UsuarioId = usuarioId, Km = 30 },
                new RegistroExercicio { Id = 2, UsuarioId = usuarioId, Km = 25 }
            };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(premioId)).ReturnsAsync(premio);
            _registroExercicioRepositoryMock.Setup(repo => repo.ObterPorUsuarioIdAsync(usuarioId)).ReturnsAsync(registrosExercicio);
            _resgateRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Resgate>()))
                .ReturnsAsync(new Resgate { Id = 1, UsuarioId = usuarioId, PremioId = premioId });

            // Act
            var resultado = await _resgateService.RegistrarResgate(usuarioId, premioId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuarioId, resultado.UsuarioId);
            Assert.Equal(premioId, resultado.PremioId);

            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.Is<Usuario>(u => u.Pontos == 5)), Times.Once);
            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Once);
        }

        [Fact]
        public async Task RegistrarResgate_DeveLancarExcecaoSePontosAcumuladosInsuficientes()
        {
            // Arrange
            var usuarioId = 1;
            var premioId = 1;
            var usuario = new Usuario { Id = usuarioId, Pontos = 0 };
            var premio = new Premio { Id = premioId, Pontos = 50, Ativo = true };

            var registrosExercicio = new List<RegistroExercicio>
            {
                new RegistroExercicio { Id = 1, UsuarioId = usuarioId, Km = 20 } // Pontos acumulados insuficientes
            };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(premioId)).ReturnsAsync(premio);
            _registroExercicioRepositoryMock.Setup(repo => repo.ObterPorUsuarioIdAsync(usuarioId)).ReturnsAsync(registrosExercicio);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _resgateService.RegistrarResgate(usuarioId, premioId));

            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Usuario>()), Times.Never);
            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Never);
        }

        [Fact]
        public async Task RegistrarResgate_DeveLancarExcecaoQuandoPremioInativo()
        {
            // Arrange
            var usuarioId = 1;
            var premioId = 1;
            var usuario = new Usuario { Id = usuarioId, Pontos = 100 };
            var premio = new Premio { Id = premioId, Pontos = 50, Ativo = false }; // Prêmio inativo

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
            _premioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(premioId)).ReturnsAsync(premio);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _resgateService.RegistrarResgate(usuarioId, premioId));

            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Never);
        }
    }
}
