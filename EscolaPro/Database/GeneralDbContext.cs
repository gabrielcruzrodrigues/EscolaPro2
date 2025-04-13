using EscolaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Database;

public class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) { }

    public DbSet<Companies> Companies { get; set; }
    public DbSet<UserGeneral> UsersGeneral { get; set; }
    public DbSet<Salts> Salts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Companies>()
            .HasIndex(e => e.CNPJ)
            .IsUnique();
    }
}
