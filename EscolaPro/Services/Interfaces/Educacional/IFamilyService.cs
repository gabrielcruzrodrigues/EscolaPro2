using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels.Educacional;

namespace EscolaPro.Services.Interfaces.Educacional
{
    public interface IFamilyService
    {
        Task<Family> CreateAsync(CreateFamilyViewModel request, HttpRequest httpRequest, string companieName);
    }
}
