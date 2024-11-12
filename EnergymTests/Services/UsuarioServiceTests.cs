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
    public class UsuarioServiceTests
    {
        private readonly UsuarioService _usuarioService;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosUsuarios_DeveRetornarListaDeUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Username = "User1" },
                new Usuario { Id = 2, Username = "User2" }
            };

            _usuarioRepositoryMock.Setup(repo => repo.ObterTodosAsync())
                                  .ReturnsAsync(usuarios);

            var resultado = await _usuarioService.ObterTodosAsync();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _usuarioRepositoryMock.Verify(repo => repo.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterUsuarioPorId_DeveRetornarUsuarioQuandoEncontrado()
        {
            var usuario = new Usuario { Id = 1, Username = "User1" };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1))
                                  .ReturnsAsync(usuario);

            var resultado = await _usuarioService.ObterPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal("User1", resultado.Username);
            _usuarioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ObterUsuarioPorId_DeveLancarExcecaoQuandoUsuarioNaoEncontrado()
        {
            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _usuarioService.ObterPorIdAsync(99));
            _usuarioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task AdicionarUsuario_DeveSalvarUsuario()
        {
            var usuario = new Usuario { Id = 1, Username = "NewUser" };

            _usuarioRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Usuario>()))
                                  .ReturnsAsync(usuario);

            var resultado = await _usuarioService.AdicionarAsync(usuario);

            Assert.NotNull(resultado);
            Assert.Equal("NewUser", resultado.Username);
            _usuarioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarUsuario_DeveAtualizarUsuarioQuandoEncontrado()
        {
            var usuarioExistente = new Usuario { Id = 1, Username = "OldUser" };
            var usuarioAtualizado = new Usuario { Id = 1, Username = "UpdatedUser" };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(1))
                                  .ReturnsAsync(usuarioExistente);

            _usuarioRepositoryMock.Setup(repo => repo.AtualizarAsync(usuarioAtualizado))
                                  .ReturnsAsync(usuarioAtualizado);

            var resultado = await _usuarioService.AtualizarAsync(usuarioAtualizado);

            Assert.NotNull(resultado);
            Assert.Equal("UpdatedUser", resultado.Username);
            _usuarioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(1), Times.Once);
            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(usuarioAtualizado), Times.Once);
        }

        [Fact]
        public async Task DeletarUsuario_DeveRemoverUsuarioQuandoEncontrado()
        {
            var usuarioId = 1;
            var usuarioExistente = new Usuario { Id = usuarioId, Username = "Teste" };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId))
                                  .ReturnsAsync(usuarioExistente);

            _usuarioRepositoryMock.Setup(repo => repo.DeletarAsync(usuarioId))
                                  .ReturnsAsync(true);

            var resultado = await _usuarioService.DeletarAsync(usuarioId);

            Assert.True(resultado);
            _usuarioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(usuarioId), Times.Once);
            _usuarioRepositoryMock.Verify(repo => repo.DeletarAsync(usuarioId), Times.Once);
        }

        [Fact]
        public async Task DeletarUsuario_DeveLancarExcecaoQuandoUsuarioNaoEncontrado()
        {
            var usuarioId = 999;
            _usuarioRepositoryMock.Setup(repo => repo.ObterPorIdAsync(usuarioId))
                                  .ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _usuarioService.DeletarAsync(usuarioId);
            });

            _usuarioRepositoryMock.Verify(repo => repo.ObterPorIdAsync(usuarioId), Times.Once);
        }
    }
}
