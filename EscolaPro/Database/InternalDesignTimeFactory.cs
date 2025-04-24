using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscolaPro.Database
{
    public class InternalDesignTimeFactory : IDesignTimeDbContextFactory<InternalDbContext>
    {
        public InternalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=design_db;User Id=sa;Password=12345678;TrustServerCertificate=True;");
            return new InternalDbContext(optionsBuilder.Options);
        }
    }
}
