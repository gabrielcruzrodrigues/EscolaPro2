using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscolaPro.Database
{
    public class InternalDesignTimeFactory : IDesignTimeDbContextFactory<InternalDbContext>
    {
        public InternalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dummy_empresa;Username=postgres;Password=1234");
            return new InternalDbContext(optionsBuilder.Options);
        }
    }
}
