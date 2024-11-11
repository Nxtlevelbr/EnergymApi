using Microsoft.EntityFrameworkCore;
using EnergyApi.Models;

namespace EnergyApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Academia> Academias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<RegistroExercicio> RegistrosExercicios { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<Resgate> Resgates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.CPF).IsUnique();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.CPF).IsRequired();
                entity.Property(e => e.Senha).IsRequired();
            });

            modelBuilder.Entity<Academia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.CNPJ).IsUnique();
                entity.Property(e => e.Nome).IsRequired();
            });

            modelBuilder.Entity<RegistroExercicio>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária simples
                entity.Property(e => e.Km).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Academia)
                      .WithMany()
                      .HasForeignKey(e => e.AcademiaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CEP).IsRequired();
                entity.Property(e => e.Estado).IsRequired();
                entity.Property(e => e.Cidade).IsRequired();
                entity.Property(e => e.Rua).IsRequired();
                entity.Property(e => e.Numero).IsRequired();
            });

            modelBuilder.Entity<Premio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.Pontos).IsRequired();
                entity.Property(e => e.Empresa).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();
            });

            modelBuilder.Entity<Resgate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataHora).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Premio)
                      .WithMany()
                      .HasForeignKey(e => e.PremioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
