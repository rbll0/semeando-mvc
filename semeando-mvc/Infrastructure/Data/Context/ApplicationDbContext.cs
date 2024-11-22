using Microsoft.EntityFrameworkCore;
using semeando_mvc.Models;

namespace semeando_mvc.Infrastructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> tb_Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>().ToTable("TB_USUARIO", "RM553326"); // Ajuste o schema

        modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasColumnName("ID_USUARIO");
        modelBuilder.Entity<Usuario>().Property(u => u.NomeUsuario).HasColumnName("NOME_USUARIO");
        modelBuilder.Entity<Usuario>().Property(u => u.Email).HasColumnName("EMAIL");
        modelBuilder.Entity<Usuario>().Property(u => u.Ranking).HasColumnName("RANKING");
        modelBuilder.Entity<Usuario>().Property(u => u.Streak).HasColumnName("STREAK");
        modelBuilder.Entity<Usuario>().Property(u => u.Bio).HasColumnName("BIO");
    }
}

