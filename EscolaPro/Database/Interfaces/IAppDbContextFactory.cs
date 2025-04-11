using System;
using EscolaPro.Database;

namespace EscolaPro.Database.Interfaces
{
    public interface IAppDbContextFactory
    {
        GeneralDbContext Create(string companie);
    }
}
