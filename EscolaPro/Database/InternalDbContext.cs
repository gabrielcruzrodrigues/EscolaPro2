using EscolaPro.Models;
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

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.HasIndex(a => a.Name).IsUnique();
            });

            modelBuilder.Entity<FixedHealth>()
                .HasOne(fh => fh.Student)
                .WithOne(s => s.FixedHealth)
                .HasForeignKey<FixedHealth>(fh => fh.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Student>()
            //    .HasOne(s => s.Father)
            //    .WithMany()
            //    .HasForeignKey(s => s.FatherId)
            //    .OnDelete(DeleteBehavior.SetNull); 

            //modelBuilder.Entity<Student>()
            //    .HasOne(s => s.Mother)
            //    .WithMany()
            //    .HasForeignKey(s => s.MotherId)
            //    .OnDelete(DeleteBehavior.SetNull); 

            //modelBuilder.Entity<Student>()
            //    .HasOne(s => s.Responsible)
            //    .WithMany()
            //    .HasForeignKey(s => s.ResponsibleId)
            //    .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
