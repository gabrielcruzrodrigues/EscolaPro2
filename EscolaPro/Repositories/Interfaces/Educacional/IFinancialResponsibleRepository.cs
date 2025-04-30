using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories.Interfaces.Educacional
{
    public interface IFinancialResponsibleRepository
    {
        Task<FinancialResponsible> CreateAsync(string companieName, FinancialResponsible request);
        Task<FinancialResponsible> GetByIdAsync(string companieName, long id);
    }
}
