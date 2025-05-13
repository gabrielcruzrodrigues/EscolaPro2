using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels.Educacional;

namespace EscolaPro.Services.Educacional.Interfaces
{
    public interface IFinancialResponsibleService
    {
        Task<FinancialResponsible> CreateAsync(CreateFinancialResponsibleViewModel request, HttpRequest httpRequest, string companieName);
    }
};
