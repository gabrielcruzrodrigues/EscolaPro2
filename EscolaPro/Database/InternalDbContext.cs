using EscolaPro.Models.Educacional;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Database
{
    public class InternalDbContext : DbContext
    {
        public InternalDbContext(DbContextOptions<InternalDbContext> options) : base(options) { }

        public DbSet<Family> Families { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<FixedHealth> FixedHealths { get; set; }
        public DbSet<FinancialResponsible> FinancialResponsibles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasIndex(f => f.Email).IsUnique();
                entity.HasIndex(f => f.Rg).IsUnique();
                entity.HasIndex(f => f.Cpf).IsUnique();
                entity.HasIndex(f => f.Phone).IsUnique();
            });

            modelBuilder.Entity<FinancialResponsible>(entity =>
            {
                entity.HasIndex(f => f.Email).IsUnique();
                entity.HasIndex(f => f.Rg).IsUnique();
                entity.HasIndex(f => f.Cpf).IsUnique();
                entity.HasIndex(f => f.Phone).IsUnique();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(f => f.Email).IsUnique();
                entity.HasIndex(f => f.Rg).IsUnique();
                entity.HasIndex(f => f.Cpf).IsUnique();
                entity.HasIndex(f => f.Phone).IsUnique();
            });

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.HasIndex(a => a.Name).IsUnique();
            });

            modelBuilder.Entity<FixedHealth>()
                .HasOne(fh => fh.Student)
                .WithOne(s => s.FixedHealth)
                .HasForeignKey<FixedHealth>(fh => fh.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
