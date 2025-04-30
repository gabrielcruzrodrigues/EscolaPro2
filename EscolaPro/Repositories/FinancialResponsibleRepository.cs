using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories;

public class FinancialResponsibleRepository : IFinancialResponsibleRepository
{
    public Task<FinancialResponsible> CreateAsync(FinancialResponsible request)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialResponsible> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
