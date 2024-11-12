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
    public class AcademiaServiceTests
    {
        private readonly AcademiaService _academiaService;
        private readonly Mock<IAcademiaRepository> _academiaRepositoryMock;

        public AcademiaServiceTests()
        {
            _academiaRepositoryMock = new Mock<IAcademiaRepository>();
            _academiaService = new AcademiaService(_academiaRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodasAcademias_DeveRetornarListaDeAcademias()
        {
            var academias = new List<Academia>
            {
                new Academia { Id = 1, Nome = "Academia 1" },
                new Academia { Id = 2, Nome = "Academia 2" }
            };

            _academiaRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(academias);

            var resultado = await _academiaService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _academiaRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterAcademiaPorId_DeveRetornarAcademiaQuandoEncontrada()
        {
            var academia = new Academia { Id = 1, Nome = "Academia 1" };

            _academiaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(academia);

            var resultado = await _academiaService.ObterPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal("Academia 1", resultado.Nome);
            _academiaRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterAcademiaPorId_DeveLancarExcecaoQuandoNaoEncontrada()
        {
            _academiaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Academia)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _academiaService.ObterPorIdAsync(99));
            _academiaRepositoryMock.Verify(repo => repo.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task AdicionarAcademia_DeveSalvarAcademia()
        {
            var academia = new Academia { Id = 1, Nome = "Nova Academia" };

            _academiaRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Academia>())).ReturnsAsync(academia);

            var resultado = await _academiaService.AdicionarAsync(academia);

            Assert.NotNull(resultado);
            Assert.Equal("Nova Academia", resultado.Nome);
            _academiaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Academia>()), Times.Once);
        }
    }
}
