using EscolaPro.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace EscolaPro.Database
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly GeneralDbContext _generalDbContext;

        public DbContextFactory(GeneralDbContext generalDbContext)
        {
            _generalDbContext = generalDbContext;
        }

        public InternalDbContext Create(string companie)
        {
            var internalDb = _generalDbContext.Companies
                .FirstOrDefault(t => t.Name == companie);

            if (internalDb == null)
            {
                throw new Exception("Empresa não encontrada.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>();
            optionsBuilder.UseSqlServer(internalDb.ConnectionString);

            return new InternalDbContext(optionsBuilder.Options);
        }
    }
}
