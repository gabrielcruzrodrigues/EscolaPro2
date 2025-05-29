namespace EscolaPro.Services.Educacional;

using Azure.Core;
using EscolaPro.Enums;
using EscolaPro.Extensions;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Services.Educacional.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels.Educacional;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

public class FamilyService : IFamilyService
{
    private readonly IImageService _imageService;
    private readonly IFamilyRepository _familyRepository;
    private readonly IFileService _fileService;

    public FamilyService(
        IImageService imageService, 
        IFamilyRepository familyRepository,
        IFileService fileService
    )
    {
        _imageService = imageService;
        _familyRepository = familyRepository;
        _fileService = fileService;
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

            imageFamilyUrl = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image, httpRequest);
        }

        string rgFilePath = "";
        if (request.RgFile != null)
            rgFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.RgFile, httpRequest);

        string cpfFilePath = "";
        if (request.CpfFile != null)
            cpfFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.CpfFile, httpRequest);

        string proofOfResidenceFilePath = "";
        if (request.ProofOfResidenceFile != null)
            proofOfResidenceFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.ProofOfResidenceFile, httpRequest);

        List<string> duplicateErrorFields = new();
        bool duplicateErrors = false;

        if (await _familyRepository.GetByEmailAsync(companieName, request.Email) != null)
        {
            duplicateErrorFields.Add("email");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByRgAsync(companieName, request.Rg) != null)
        {
            duplicateErrorFields.Add("rg");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByCpfAsync(companieName, request.Cpf) != null)
        {
            duplicateErrorFields.Add("cpf");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByPhoneAsync(companieName, request.Phone) != null)
        {
            duplicateErrorFields.Add("phone");
            duplicateErrors = true;
        }

        if (duplicateErrors)
        {
            throw new HttpResponseException(409, "Campos duplicados", duplicateErrorFields);
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
            Type = request.Type,
            HomeNumber = request.HomeNumber,
            CpfFilePath = cpfFilePath,
            RgFilePath = rgFilePath,
            ProofOfResidenceFilePath = proofOfResidenceFilePath
        };

        return await _familyRepository.CreateAsync(companieName, family);
    }

    public async Task UpdateAsync(UpdateFamilyViewModel request, HttpRequest httpRequest, string companieName)
    {
        var family = await _familyRepository.GetByIdAsync(companieName, request.Id);

        //Validar alteração de imagem aqui

        List<string> duplicateErrorFields = new();
        bool duplicateErrors = false;

        if (await _familyRepository.GetByEmailAsync(companieName, request.Email) != null && family.Email != request.Email)
        {
            duplicateErrorFields.Add("email");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByRgAsync(companieName, request.Rg) != null && family.Rg != request.Rg)
        {
            duplicateErrorFields.Add("rg");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByCpfAsync(companieName, request.Cpf) != null && family.Cpf != request.Cpf)
        {
            duplicateErrorFields.Add("cpf");
            duplicateErrors = true;
        }

        if (await _familyRepository.GetByPhoneAsync(companieName, request.Phone) != null && family.Phone != request.Phone)
        {
            duplicateErrorFields.Add("phone");
            duplicateErrors = true;
        }

        if (duplicateErrors)
        {
            throw new HttpResponseException(409, "Campos duplicados", duplicateErrorFields);
        }

        if (request.RgFile != null)
        {
            var newRgFilePath = await _fileService.UpdateESaveANewFileInDatabaseAndDeleteTheLastFileAndReturnTheNewUrlAsync(request.RgFile, httpRequest, family.RgFilePath!);
            family.RgFilePath = newRgFilePath;
        } 
        else
        {
            if (request.RgFileDeleted == "true")
            {
                _ = _fileService.DeleteFileFromFileName(family.RgFilePath);
                family.RgFilePath = "";
            }
        }

        if (request.CpfFile != null)
        {
            var newCpfFilePath = await _fileService.UpdateESaveANewFileInDatabaseAndDeleteTheLastFileAndReturnTheNewUrlAsync(request.CpfFile, httpRequest, family.CpfFilePath!);
            family.CpfFilePath = newCpfFilePath;
        }
        else
        {
            if (request.CpfFileDeleted == "true")
            {
                _ = _fileService.DeleteFileFromFileName(family.CpfFilePath);
                family.CpfFilePath = "";
            }
        }

        if (request.ProofOfResidenceFile != null)
        {
            var newProofOfResidenceFilePath = await _fileService.UpdateESaveANewFileInDatabaseAndDeleteTheLastFileAndReturnTheNewUrlAsync(request.ProofOfResidenceFile, httpRequest, family.ProofOfResidenceFilePath!);
            family.ProofOfResidenceFilePath = newProofOfResidenceFilePath;
        }
        else
        {
            if (request.ProofOfResidenceFileDeleted == "true")
            {
                _ = _fileService.DeleteFileFromFileName(family.ProofOfResidenceFilePath);
                family.ProofOfResidenceFilePath = "";
            }
        }

        family.Email = request.Email ?? family.Email;
        family.Name = request.Name ?? family.Name;
        family.DateOfBirth = request.DateOfBirth ?? family.DateOfBirth;
        family.Nationality = request.Nationality ?? family.Nationality;
        family.Naturalness = request.Naturalness ?? family.Naturalness;
        family.Sex = request.Sex ?? family.Sex;
        family.Cep = request.Cep ?? family.Cep;
        family.Address = request.Address ?? family.Address;
        family.Neighborhood = request.Neighborhood ?? family.Neighborhood;
        family.City = request.City ?? family.City;
        family.State = request.State ?? family.State;
        family.RgDispatched = request.RgDispatched ?? family.RgDispatched;
        family.RgDispatchedDate = request.RgDispatchedDate ?? family.RgDispatchedDate;

        await _familyRepository.Update(companieName, family);
    }
}
