using EscolaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Database
{
    public class InternalDbContext : DbContext
    {
        public InternalDbContext(DbContextOptions<InternalDbContext> options) : base(options) { }

        public DbSet<Family> Families { get; set; }
    }
}
