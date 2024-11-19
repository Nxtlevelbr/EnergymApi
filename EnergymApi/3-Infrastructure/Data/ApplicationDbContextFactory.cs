using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EnergymApi._3_Infrastructure.Data
{
    /// <summary>
    /// Fábrica para criar instâncias de <see cref="ApplicationDbContext"/> em tempo de design.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Cria uma nova instância de <see cref="ApplicationDbContext"/> com as opções configuradas.
        /// </summary>
        /// <param name="args">Argumentos passados em tempo de execução.</param>
        /// <returns>Uma nova instância de <see cref="ApplicationDbContext"/>.</returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Configuração do arquivo de configuração appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obtém a string de conexão configurada
            var connectionString = configuration.GetConnectionString("OracleConnection");
            
            // Configura o DbContext para usar Oracle
            optionsBuilder.UseOracle(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}