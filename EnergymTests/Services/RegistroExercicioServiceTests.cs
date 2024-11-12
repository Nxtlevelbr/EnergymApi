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
            var registros = new List<RegistroExercicio>
            {
                new RegistroExercicio { Id = 1, Km = 10 },
                new RegistroExercicio { Id = 2, Km = 15 }
            };

            _registroExercicioRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(registros);

            var resultado = await _registroExercicioService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _registroExercicioRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task AdicionarRegistro_DeveSalvarRegistro()
        {
            var registro = new RegistroExercicio { Id = 1, Km = 20 };

            _registroExercicioRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<RegistroExercicio>())).ReturnsAsync(registro);

            var resultado = await _registroExercicioService.AdicionarAsync(registro);

            Assert.NotNull(resultado);
            Assert.Equal(20, resultado.Km);
            _registroExercicioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<RegistroExercicio>()), Times.Once);
        }
    }
}
