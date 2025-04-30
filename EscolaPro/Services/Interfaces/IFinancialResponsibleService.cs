using EscolaPro.Models;
using EscolaPro.ViewModels;

namespace EscolaPro.Services.Interfaces
{
    public interface IFinancialResponsibleService
    {
        Task<FinancialResponsible> CreateAsync(CreateFinancialResponsibleViewModel request, HttpRequest httpRequest, string companieName);
    }
};
