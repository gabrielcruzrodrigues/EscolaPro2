using EscolaPro.Extensions;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Services.Educacional.Interfaces;
using EscolaPro.Services.Interfaces;
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
        string financialResponsibleUrl = "";
        if (request.Image != null)
        {
            if (request.Image.Length == 0)
            {
                throw new HttpResponseException(400, "Arquivo inváido");
            }

            var fileName = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image);
            financialResponsibleUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/uploads/{fileName}";
        }

        if (await _financialResponsibleRepository.GetByNameAsync(companieName, request.Name) != null)
        {
            throw new HttpResponseException(400, "Esse Nome de Responsável Financeiro já foi cadastrado");
        }

        if (await _financialResponsibleRepository.GetByEmailAsync(companieName, request.Email) != null)
        {
            throw new HttpResponseException(400, "Esse email de Responsável Financeiro já foi cadastrado");
        }

        if (await _financialResponsibleRepository.GetByRgAsync(companieName, request.Rg) != null)
        {
            throw new HttpResponseException(400, "Esse RG de Responsável Financeiro já foi cadastrado");
        }

        if (await _financialResponsibleRepository.GetByCpfAsync(companieName, request.Cpf) != null)
        {
            throw new HttpResponseException(400, "Esse CPF de Responsável Financeiro já foi cadastrado");
        }

        if (await _financialResponsibleRepository.GetByPhoneAsync(companieName, request.Phone) != null)
        {
            throw new HttpResponseException(400, "Esse Telefone de Responsável Financeiro já foi cadastrado");
        }

        var financialResponsible = new FinancialResponsible
        {
            Image = financialResponsibleUrl,
            Name = request.Name,
            Email = request.Email,
            Rg = request.Rg,
            Cpf = request.Cpf,
            DateOfBirth = request.DateOfBirth,
            Nationality = request.Nationality,
            Naturalness = request.Naturalness,
            Sex = request.Sex,
            Cep = request.Cep,
            Address = request.Address,
            Phone = request.Phone,
            Neighborhood = request.Neighborhood,
            City = request.City,
            State = request.State,
            Role = request.Role,
            Status = true,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CivilState = request.CivilState,
            RgDispatched = request.RgDispatched,
            RgDispatchedDate = request.RgDispatchedDate,
            HomeNumber = request.HomeNumber
        };

        return await _financialResponsibleRepository.CreateAsync(companieName, financialResponsible);
    }
}
