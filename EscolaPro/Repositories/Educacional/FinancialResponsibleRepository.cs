using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories.Educacional;

public class FinancialResponsibleRepository : IFinancialResponsibleRepository
{
    public Task<FinancialResponsible> CreateAsync(string companieName, FinancialResponsible request)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialResponsible> GetByIdAsync(string companieName, long id)
    {
        throw new NotImplementedException();
    }
}
