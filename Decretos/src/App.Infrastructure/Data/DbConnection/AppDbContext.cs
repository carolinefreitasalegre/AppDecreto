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
    
    public DbSet<HistoricoDecreto> HistoricoDecreto { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Secretaria>("secretaria_enum");
        modelBuilder.HasPostgresEnum<UsuarioRole>("usuario_role_enum");
        modelBuilder.HasPostgresEnum<UsuarioStatus>("usuario_status_enum");

        modelBuilder.HasSequence<int>("decreto_numero_seq")
            .StartsAt(5966)
            .IncrementsBy(1);

        modelBuilder.Entity<Decreto>()
            .Property(d => d.NumeroDecreto)
            .HasDefaultValueSql("nextval('decreto_numero_seq')")
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Entity<Decreto>()
            .Property(s => s.Secretaria)
            .HasConversion<int>();
        
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Role)
            .HasConversion<int>();
        
        modelBuilder.Entity<Usuario>()
            .Property(s => s.Status)
            .HasConversion<int>();
        
        modelBuilder.Entity<Decreto>()
            .HasOne(d => d.Usuario)
            .WithMany(u => u.Decretos)
            .HasForeignKey(d => d.UsuarioId);
        
        base.OnModelCreating(modelBuilder);
    }
}