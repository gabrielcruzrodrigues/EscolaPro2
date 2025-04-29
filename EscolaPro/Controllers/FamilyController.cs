using EscolaPro.Enums;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EscolaPro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly ICompanieRepository _companieRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IImageService _imageService;

    public FamilyController(
        IFamilyRepository familyRepository,
        IUsersGeneralRepository userGeneralRepository,
        ICompanieRepository companieRepository,
        IStudentRepository studentRepository,
        IImageService imageService
        )
    {
        _familyRepository = familyRepository;
        _userGeneralRepository = userGeneralRepository;
        _companieRepository = companieRepository;
        _studentRepository = studentRepository;
        _imageService = imageService;
    }

    [HttpGet]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<IEnumerable<Family>>> GetAllAsync()
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============
        
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);
        
        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var response = await _familyRepository.GetAllAsync(userCompanie.Name);
        return Ok(response);
    }

    [HttpGet("{familyId:int}")]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<Family>> GetByIdAsync(int familyId)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============
        
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);
        
        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var user = await _familyRepository.GetByIdAsync(userCompanie.Name, familyId);
        return Ok(user);
    }

    [HttpPost]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<Family>> CreateAsync([FromForm] CreateFamilyViewModel request)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============
        
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        string imageFamilyUrl = "";
        if (request.Image != null)
        {
            if (request.Image.Length == 0)
            {
                return BadRequest("Arquivo inváido");
            }

            var fileName = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image);
            imageFamilyUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        }

        if (await _familyRepository.GetByNameAsync(userCompanie.Name, request.Name) != null)
        {
            return Conflict(new { message = "Esse Nome já foi cadastrado", type = "name", code = 409 });
        }

        if (await _familyRepository.GetByEmailAsync(userCompanie.Name, request.Email) != null)
        {
            return Conflict(new { message = "Esse email já foi cadastrado", type = "email", code = 409 });
        }

        if (await _familyRepository.GetByRgAsync(userCompanie.Name, request.Rg) != null)
        {
            return Conflict(new { message = "Esse RG já foi cadastrado", type = "rg", code = 409 });
        }

        if (await _familyRepository.GetByCpfAsync(userCompanie.Name, request.Cpf) != null)
        {
            return Conflict(new { message = "Esse CPF já foi cadastrado", type = "cpf", code = 409 });
        }

        if (await _familyRepository.GetByPhoneAsync(userCompanie.Name, request.Phone) != null)
        {
            return Conflict(new { message = "Esse Telefone já foi cadastrado", type = "phone", code = 409 });
        }

        var family = new Family
        {
            Image = imageFamilyUrl,
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
            WorkAddress = request.WorkAddress ?? "",
            Ocupation = request.Ocupation,
            Type = request.Type
        };

        var response = await _familyRepository.CreateAsync(userCompanie.Name, family);
        return StatusCode(201, response);
    }

    [HttpDelete("{familyId:int}")]
    [Authorize(policy: "admin_internal")]
    public async Task<IActionResult> DisableAsync(int familyId)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        await _familyRepository.Disable(userCompanie.Name, familyId);
        return StatusCode(204);
    }

    [HttpGet("search/{param}")]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult> Search(string param)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var companie = await _familyRepository.Search(userCompanie.Name, param);
        return Ok(companie);
    }

    [HttpPut]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult> Update(UpdateFamilyViewModel request)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var family = await _familyRepository.GetByIdAsync(userCompanie.Name, request.Id);

        //Validar alteração de imagem aqui

        if (!request.Name.IsNullOrEmpty())
        {
            if (await _familyRepository.GetByNameAsync(userCompanie.Name, request.Name) == null)
            {
                family.Name = request.Name;
            }
            else
            {
                return BadRequest("Este Nome já está cadastrado no banco de dados!");
            }
        }

        if (!request.Email.IsNullOrEmpty())
        {
            if (await _familyRepository.GetByEmailAsync(userCompanie.Name, request.Email) == null)
            {
                family.Email = request.Email;
            }
            else
            {
                return BadRequest("Este Email já está cadastrado no banco de dados!");
            }
        }
        

        if (!request.Rg.IsNullOrEmpty())
        {
            if (await _familyRepository.GetByRgAsync(userCompanie.Name, request.Rg) == null)
            {
                family.Rg = request.Rg;
            }
            else
            {
                return BadRequest("Este RG já está cadastrado no banco de dados!");
            }
        }

        if (!request.Cpf.IsNullOrEmpty())
        {
            if (await _familyRepository.GetByCpfAsync(userCompanie.Name, request.Cpf) == null)
            {
                family.Cpf = request.Cpf;
            }
            else
            {
                return BadRequest("Este CPF já está cadastrado no banco de dados!");
            }
        }
        

        if (!request.Phone.IsNullOrEmpty())
        {
            if (await _familyRepository.GetByPhoneAsync(userCompanie.Name, request.Phone) == null)
            {
                family.Phone = request.Phone;
            }
            else
            {
                return BadRequest("Este Telefone já está cadastrado no banco de dados!");
            }
        }
        

        if (request.Role != null)
        {
            if (Enum.IsDefined(typeof(RolesEnum), request.Role))
            {
                family.Role = request.Role.Value;
            }
            else
            {
                return BadRequest("Essa Role não está cadastrada no banco de dados!");
            }
        }
        

        if (request.Type != null)
        {
            if (Enum.IsDefined(typeof(Type), request.Type))
            {
                family.Type = request.Type.Value;
            }
            else
            {
                return BadRequest("Esse tipo não está cadastrado no banco de dados!");
            }
        }

        family.DateOfBirth = request.DateOfBirth ?? family.DateOfBirth;
        family.Nationality = request.Nationality ?? family.Nationality;
        family.Naturalness = request.Naturalness ?? family.Naturalness;
        family.Sex = request.Sex ?? family.Sex;
        family.Cep = request.Cep ?? family.Cep;
        family.Address = request.Address ?? family.Address;
        family.Neighborhood = request.Neighborhood ?? family.Neighborhood;
        family.City = request.City ?? family.City;
        family.State = request.State ?? family.State;
        family.WorkAddress = request.WorkAddress ?? family.WorkAddress;
        family.Ocupation = request.Ocupation ?? family.Ocupation;

        await _familyRepository.Update(userCompanie.Name, family);
        return NoContent();
    }

    private async Task<bool> CompanieValidation(long userId, int companieId)
    {
        var userGeneral = await _userGeneralRepository.GetByIdAsync(userId);

        if (userGeneral.CompanieId == companieId)
        {
            return true;
        }

        return false;
    }

}
