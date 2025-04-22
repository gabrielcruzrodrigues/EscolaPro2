using System;
using EscolaPro.Database;

namespace EscolaPro.Database.Interfaces
{
    public interface IAppDbContextFactory
    {
        InternalDbContext Create(string companie);
    }
}
