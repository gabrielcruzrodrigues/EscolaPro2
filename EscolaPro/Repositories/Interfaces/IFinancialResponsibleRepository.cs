using EscolaPro.Models;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories.Interfaces
{
    public interface IFinancialResponsibleRepository
    {
        Task<FinancialResponsible> CreateAsync(string companieName, FinancialResponsible request);
        Task<FinancialResponsible> GetByIdAsync(string companieName, long id);
    }
}
