using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;

namespace EscolaPro.Services;

public class FinancialResponsibleService : IFinancialResponsibleService
{
    private readonly IImageService _imageService;
    private readonly IFinancialResponsibleRepository _financialResponsibleRepository;

    public FinancialResponsibleService(IImageService imageService, IFinancialResponsibleRepository financialResponsibleRepository)
    {
        _imageService = imageService;
        _financialResponsibleRepository = financialResponsibleRepository;
    }

    public async Task<FinancialResponsible> CreateAsync(CreateFinancialResponsibleViewModel request, HttpRequest httpRequest, string companieName)
    {
        var financialResponsible = new FinancialResponsible
        {
            Name = request.Name
        };

        return await _financialResponsibleRepository.CreateAsync(companieName, financialResponsible);
    }
}
