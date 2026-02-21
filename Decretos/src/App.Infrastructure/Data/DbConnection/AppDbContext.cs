using App.Domain;
using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.DbConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Decreto> Decretos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Secretaria>("secretaria_enum");
        modelBuilder.HasPostgresEnum<UsuarioRole>("usuario_role_enum");
        modelBuilder.HasPostgresEnum<UsuarioStatus>("usuario_status_enum");

        modelBuilder.Entity<Decreto>()
            .HasOne(d => d.Usuario)
            .WithMany(u => u.Decretos)
            .HasForeignKey(d => d.UsuarioId);

        base.OnModelCreating(modelBuilder);
    }
}