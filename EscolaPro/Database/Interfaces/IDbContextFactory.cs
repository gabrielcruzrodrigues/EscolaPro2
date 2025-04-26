using System;
using EscolaPro.Database;

namespace EscolaPro.Database.Interfaces;

public interface IDbContextFactory
{
    InternalDbContext Create(string companie);
}
