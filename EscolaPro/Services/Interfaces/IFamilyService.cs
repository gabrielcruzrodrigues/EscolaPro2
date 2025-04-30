using EscolaPro.Models;
using EscolaPro.ViewModels;

namespace EscolaPro.Services.Interfaces
{
    public interface IFamilyService
    {
        Task<Family> CreateAsync(CreateFamilyViewModel request, HttpRequest httpRequest, string companieName);
    }
}
