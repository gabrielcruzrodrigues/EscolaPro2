using EscolaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Database
{
    public class InternalDbContext : DbContext
    {
        public InternalDbContext(DbContextOptions<InternalDbContext> options) : base(options) { }

        public DbSet<Family> Families { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Allergie> Allergies { get; set; }
        public DbSet<FixedHealth> FixedHealths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasIndex(f => f.Name).IsUnique();
                entity.HasIndex(f => f.Email).IsUnique();
                entity.HasIndex(f => f.Rg).IsUnique();
                entity.HasIndex(f => f.Cpf).IsUnique();
                entity.HasIndex(f => f.Phone).IsUnique();
            });

            modelBuilder.Entity<Allergie>(entity =>
            {
                entity.HasIndex(a => a.Name).IsUnique();
            });
        }
    }
}
