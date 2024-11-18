using EnergymApi._2_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnergymApi._3_Infrastructure.Data
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
            var boolToIntConverter = new ValueConverter<bool, int>(
                v => v ? 1 : 0,
                v => v == 1
            );

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.CPF).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Senha).IsRequired();
            });

            modelBuilder.Entity<Academia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.CNPJ).IsUnique();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<RegistroExercicio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Km).IsRequired().HasColumnType("FLOAT");
                entity.Property(e => e.DataHora).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.RegistrosExercicios)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CEP).IsRequired().HasMaxLength(8);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(2);
                entity.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Rua).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Numero).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Premio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Pontos).IsRequired();
                entity.Property(e => e.Empresa).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);

                entity.Property(p => p.Ativo)
                      .HasConversion(boolToIntConverter)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<Resgate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataHora).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Resgates)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Premio)
                      .WithMany(p => p.Resgates)
                      .HasForeignKey(e => e.PremioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
