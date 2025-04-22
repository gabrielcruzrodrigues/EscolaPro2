using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public FamilyController(
        IFamilyRepository familyRepository, 
        IUsersGeneralRepository userGeneralRepository,
        ICompanieRepository companieRepository,
        IStudentRepository studentRepository
        )
    {
        _familyRepository = familyRepository;
        _userGeneralRepository = userGeneralRepository;
        _companieRepository = companieRepository;
        _studentRepository = studentRepository;
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
    public async Task<ActionResult<Family>> CreateAsync(CreateFamilyViewModel request)
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

        if (await _studentRepository.GetbyIdAsync(userCompanie.Name, request.StudentId) == null)
        {
            return NotFound("Estudante não encontrado!");
        }

        //Adicionar inserção de imagens aqui

        var family = new Family
        {
            Image = "",
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
            StudentId = request.StudentId,
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
