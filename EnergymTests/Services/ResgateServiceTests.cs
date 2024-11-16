using EnergyApi.Services;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._2_Domain.Models;
using Moq;
using Xunit;

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
            var usuario = new Usuario { Id = usuarioId, Pontos = 0 };
            var premio = new Premio { Id = premioId, Pontos = 50, Ativo = true };

            // Registros de exercício com total de 55 pontos
            var registrosExercicio = new List<RegistroExercicio>
            {
                new RegistroExercicio { Id = 1, UsuarioId = usuarioId, Km = 30 },
                new RegistroExercicio { Id = 2, UsuarioId = usuarioId, Km = 25 },
                new RegistroExercicio { Id = 3, UsuarioId = usuarioId, Km = 12 }
                
            };

            // Mockando os métodos dos repositórios
            _usuarioRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(usuarioId))
                .ReturnsAsync(usuario);

            _premioRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(premioId))
                .ReturnsAsync(premio);

            _registroExercicioRepositoryMock
                .Setup(repo => repo.ObterPorUsuarioIdAsync(usuarioId))
                .ReturnsAsync(registrosExercicio);

            _resgateRepositoryMock
                .Setup(repo => repo.AdicionarAsync(It.IsAny<Resgate>()))
                .ReturnsAsync((Resgate resgate) => resgate);

            _usuarioRepositoryMock
                .Setup(repo => repo.AtualizarAsync(It.IsAny<Usuario>()))
                .ReturnsAsync((Usuario u) => u);
            
            var resultado = await _resgateService.RegistrarResgate(usuarioId, premioId);

            
            Assert.NotNull(resultado);
            Assert.Equal(usuarioId, resultado.UsuarioId);
            Assert.Equal(premioId, resultado.PremioId);

            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.Is<Usuario>(u => u.Pontos == 5)), Times.Once);
            _resgateRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Resgate>()), Times.Once);
        }

    }
}
