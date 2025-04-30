using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Services.Interfaces;
using EscolaPro.Services.Interfaces.Educacional;
using EscolaPro.ViewModels.Educacional;

namespace EscolaPro.Services.Educacional;

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
        //var financialResponsible = new FinancialResponsible();

        //return await _financialResponsibleRepository.CreateAsync(companieName, financialResponsible);
        return null;
    }
}
