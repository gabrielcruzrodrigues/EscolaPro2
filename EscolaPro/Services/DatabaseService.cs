using EscolaPro.Database;
using EscolaPro.Database.Interfaces;
using EscolaPro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Services;

public class DatabaseService : IDatabaseService
{
    private readonly IAppDbContextFactory _appDbContextFactory;

    public DatabaseService(IAppDbContextFactory appDbContextFactory)
    {
        _appDbContextFactory = appDbContextFactory;
    }

    public void CreateDatabase(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new InternalDbContext(optionsBuilder.Options);

        // Isso vai criar o banco (caso ainda não exista) e aplicar as migrations
        context.Database.Migrate();
    }

    public async Task UpdateDatabase(string companyName)
    {
        using var context = _appDbContextFactory.Create(companyName);

        await context.Database.MigrateAsync(); // Aplica todas as migrations pendentes
    }
}
