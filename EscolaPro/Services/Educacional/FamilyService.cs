﻿namespace EscolaPro.Services.Educacional;

using Azure.Core;
using EscolaPro.Services.Interfaces;
using System.Threading.Tasks;
using EscolaPro.Extensions;
using EscolaPro.ViewModels.Educacional;
using EscolaPro.Services.Interfaces.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Models.Educacional;

public class FamilyService : IFamilyService
{
    private readonly IImageService _imageService;
    private readonly IFamilyRepository _familyRepository;

    public FamilyService(IImageService imageService, IFamilyRepository familyRepository)
    {
        _imageService = imageService;
        _familyRepository = familyRepository;
    }

    public async Task<Family> CreateAsync(CreateFamilyViewModel request, HttpRequest httpRequest, string companieName)
    {
        string imageFamilyUrl = "";
        if (request.Image != null)
        {
            if (request.Image.Length == 0)
            {
                throw new HttpResponseException(400, "Arquivo inváido");
            }

            var fileName = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image);
            imageFamilyUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/uploads/{fileName}";
        }

        if (await _familyRepository.GetByNameAsync(companieName, request.Name) != null)
        {
            throw new HttpResponseException(400, "Esse Nome de Familiar já foi cadastrado");
        }

        if (await _familyRepository.GetByEmailAsync(companieName, request.Email) != null)
        {
            throw new HttpResponseException(400, "Esse email de Familiar já foi cadastrado");
        }

        if (await _familyRepository.GetByRgAsync(companieName, request.Rg) != null)
        {
            throw new HttpResponseException(400, "Esse RG de Familiar já foi cadastrado");
        }

        if (await _familyRepository.GetByCpfAsync(companieName, request.Cpf) != null)
        {
            throw new HttpResponseException(400, "Esse CPF de Familiar já foi cadastrado");
        }

        if (await _familyRepository.GetByPhoneAsync(companieName, request.Phone) != null)
        {
            throw new HttpResponseException(400, "Esse Telefone de Familiar já foi cadastrado");
        }

        var family = new Family
        {
            Image = imageFamilyUrl,
            Name = request.Name,
            Email = request.Email,
            Rg = request.Rg,
            RgDispatched = request.RgDispatched,
            RgDispatchedDate = request.RgDispatchedDate,
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
            WorkAddress = request.WorkAddress ?? "",
            Ocupation = request.Ocupation,
            Type = request.Type
        };

        return await _familyRepository.CreateAsync(companieName, family);
    }
}
