using EnergymApi._1_Application.Services;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Moq;
using Xunit;

namespace EnergymTests.Services
{
    public class RegistroExercicioServiceTests
    {
        private readonly RegistroExercicioService _registroExercicioService;
        private readonly Mock<IRegistroExercicioRepository> _registroExercicioRepositoryMock;

        public RegistroExercicioServiceTests()
        {
            _registroExercicioRepositoryMock = new Mock<IRegistroExercicioRepository>();
            _registroExercicioService = new RegistroExercicioService(_registroExercicioRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosRegistros_DeveRetornarListaDeRegistros()
        {
            // Arrange
            var registros = new List<RegistroExercicio>
            {
                new RegistroExercicio { Id = 1, Km = 10 },
                new RegistroExercicio { Id = 2, Km = 15 }
            };

            _registroExercicioRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(registros);

            // Act
            var resultado = await _registroExercicioService.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _registroExercicioRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task AdicionarRegistro_DeveSalvarRegistro()
        {
            // Arrange
            var registro = new RegistroExercicio { Id = 1, UsuarioId = 1, Km = 20 };

            _registroExercicioRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<RegistroExercicio>())).ReturnsAsync(registro);

            // Act
            var resultado = await _registroExercicioService.AdicionarAsync(registro);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(20, resultado.Km);
            _registroExercicioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<RegistroExercicio>()), Times.Once);
        }

        [Fact]
        public async Task ObterRegistroPorId_DeveRetornarRegistro()
        {
            // Arrange
            var registro = new RegistroExercicio { Id = 1, UsuarioId = 1, Km = 10 };

            _registroExercicioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(registro);

            // Act
            var resultado = await _registroExercicioService.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal(10, resultado.Km);
            _registroExercicioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterRegistroPorId_DeveLancarExcecaoSeNaoEncontrado()
        {
            // Arrange
            _registroExercicioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync((RegistroExercicio)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _registroExercicioService.ObterPorIdAsync(1));
            _registroExercicioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeletarRegistro_DeveRetornarTrueSeBemSucedido()
        {
            // Arrange
            _registroExercicioRepositoryMock.Setup(repo => repo.DeletarAsync(1)).ReturnsAsync(true);

            // Act
            var resultado = await _registroExercicioService.DeletarAsync(1);

            // Assert
            Assert.True(resultado);
            _registroExercicioRepositoryMock.Verify(repo => repo.DeletarAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeletarRegistro_DeveRetornarFalseSeNaoEncontrado()
        {
            // Arrange
            _registroExercicioRepositoryMock.Setup(repo => repo.DeletarAsync(1)).ReturnsAsync(false);

            // Act
            var resultado = await _registroExercicioService.DeletarAsync(1);

            // Assert
            Assert.False(resultado);
            _registroExercicioRepositoryMock.Verify(repo => repo.DeletarAsync(1), Times.Once);
        }
    }
}
