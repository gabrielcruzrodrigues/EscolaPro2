namespace EscolaPro.Services.Educacional;

using Azure.Core;
using EscolaPro.Services.Interfaces;
using System.Threading.Tasks;
using EscolaPro.Extensions;
using EscolaPro.ViewModels.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Models.Educacional;
using EscolaPro.Services.Educacional.Interfaces;

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

            var fileName = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image);
            imageFamilyUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/uploads/{fileName}";
        }

        string rgFilePath = "";
        //if (request.RgFile != null)
        //    rgFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.RgFile);

        string cpfFilePath = "";
        //if (request.CpfFile != null)
        //    cpfFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.CpfFile);

        string proofOfResidenceFilePath = "";
        //if (request.ProofOfResidenceFile != null)
        //    proofOfResidenceFilePath = await _fileService.SaveFileInDatabaseAndReturnUrlAsync(request.ProofOfResidenceFile);

        List<string> duplicateErrorFields = new();
        bool duplicateErrors = false;

        if (await _familyRepository.GetByNameAsync(companieName, request.Name) != null)
        {
            duplicateErrorFields.Add("name");
            duplicateErrors = true;
        }

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
}
