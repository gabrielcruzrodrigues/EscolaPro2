using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels.Educacional;

namespace EscolaPro.Services.Educacional.Interfaces
{
    public interface IFamilyService
    {
        Task<Family> CreateAsync(CreateFamilyViewModel request, HttpRequest httpRequest, string companieName);
        Task UpdateAsync(UpdateFamilyViewModel request, HttpRequest httpRequest, string companieName);
    }
}
