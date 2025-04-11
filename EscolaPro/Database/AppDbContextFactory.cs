using EscolaPro.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace EscolaPro.Database
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly GeneralDbContext _generalDbContext;

        public AppDbContextFactory(GeneralDbContext generalDbContext)
        {
            _generalDbContext = generalDbContext;
        }

        public GeneralDbContext Create(string companie)
        {
            var tenant = _generalDbContext.Companies
                .FirstOrDefault(t => t.Name == companie);

            if (tenant == null)
            {
                throw new Exception("Empresa não encontrada.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<GeneralDbContext>();
            optionsBuilder.UseNpgsql(tenant.ConnectionString);

            return new GeneralDbContext(optionsBuilder.Options);
        }
    }
}
