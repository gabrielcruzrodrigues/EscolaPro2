using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels.Educacional;

namespace EscolaPro.Services.Interfaces.Educacional
{
    public interface IFinancialResponsibleService
    {
        Task<FinancialResponsible> CreateAsync(CreateFinancialResponsibleViewModel request, HttpRequest httpRequest, string companieName);
    }
};
