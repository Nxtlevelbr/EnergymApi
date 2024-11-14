using EnergyApi.Services;
using EnergymApi._1_Application.Services;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Moq;
using Xunit;

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
            // Arrange
            var premios = new List<Premio>
            {
                new Premio { Id = 1, Descricao = "Premio 1", Ativo = true },
                new Premio { Id = 2, Descricao = "Premio 2", Ativo = true }
            };

            _premioRepositoryMock.Setup(repo => repo.ObterTodosAsync(It.IsAny<bool>())).ReturnsAsync(premios);

            // Act
            var resultado = await _premioService.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _premioRepositoryMock.Verify(repo => repo.ObterTodosAsync(It.IsAny<bool>()), Times.Once);
        }
    }
}