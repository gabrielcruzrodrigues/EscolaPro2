using EscolaPro.Enums;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.Services.Educacional.Interfaces;
using EscolaPro.ViewModels.Educacional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EscolaPro.Controllers.Educacional;

[Route("api/[controller]")]
[ApiController]
public class FinancialResponsibleController : ControllerBase
{
    private readonly IFinancialResponsibleRepository _financialResponsibleRepository;
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly ICompanieRepository _companieRepository;
    private readonly IFinancialResponsibleService _financialResponsibleService;

    public FinancialResponsibleController(
        IFinancialResponsibleRepository familyRepository,
        IUsersGeneralRepository userGeneralRepository,
        ICompanieRepository companieRepository,
        IFinancialResponsibleService familyService
    )
    {
        _financialResponsibleRepository = familyRepository;
        _userGeneralRepository = userGeneralRepository;
        _companieRepository = companieRepository;
        _financialResponsibleService = familyService;
    }

    [HttpGet]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<IEnumerable<FinancialResponsible>>> GetAllAsync()
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

        var response = await _financialResponsibleRepository.GetAllAsync(userCompanie.Name);
        return Ok(response);
    }

    [HttpGet("{financialResponsibleId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<FinancialResponsible>> GetByIdAsync(int financialResponsibleId)
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

        var user = await _financialResponsibleRepository.GetByIdAsync(userCompanie.Name, financialResponsibleId);
        return Ok(user);
    }

    [HttpPost]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<FinancialResponsible>> CreateAsync([FromForm] CreateFinancialResponsibleViewModel request)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userId, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa 

        var response = await _financialResponsibleService.CreateAsync(request, Request, userCompanie.Name);
        return StatusCode(201, response);
    }

    [HttpDelete("{financialResponsibleId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<IActionResult> DisableAsync(int financialResponsibleId)
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

        await _financialResponsibleRepository.Disable(userCompanie.Name, financialResponsibleId);
        return StatusCode(204);
    }

    [HttpGet("search/{param}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
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

        var companie = await _financialResponsibleRepository.Search(userCompanie.Name, param);
        return Ok(companie);
    }

    [HttpPut]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult> Update(UpdateFinancialResponsibleViewModel request)
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

        var family = await _financialResponsibleRepository.GetByIdAsync(userCompanie.Name, request.Id);

        //Validar alteração de imagem aqui

        if (!request.Name.IsNullOrEmpty())
        {
            if (await _financialResponsibleRepository.GetByNameAsync(userCompanie.Name, request.Name) == null)
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
            if (await _financialResponsibleRepository.GetByEmailAsync(userCompanie.Name, request.Email) == null)
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
            if (await _financialResponsibleRepository.GetByRgAsync(userCompanie.Name, request.Rg) == null)
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
            if (await _financialResponsibleRepository.GetByCpfAsync(userCompanie.Name, request.Cpf) == null)
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
            if (await _financialResponsibleRepository.GetByPhoneAsync(userCompanie.Name, request.Phone) == null)
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

        await _financialResponsibleRepository.Update(userCompanie.Name, family);
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
