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
                new Premio { Id = 1, Descricao = "Premio 1" },
                new Premio { Id = 2, Descricao = "Premio 2" }
            };

            _premioRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(premios);

            var resultado = await _premioService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _premioRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }
    }
}