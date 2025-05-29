using EscolaPro.Enums;
using EscolaPro.Extensions;
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
public class FamilyController : ControllerBase
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly ICompanieRepository _companieRepository;
    private readonly IFamilyService _familyService;

    public FamilyController(
        IFamilyRepository familyRepository,
        IUsersGeneralRepository userGeneralRepository,
        ICompanieRepository companieRepository,
        IFamilyService familyService
    )
    {
        _familyRepository = familyRepository;
        _userGeneralRepository = userGeneralRepository;
        _companieRepository = companieRepository;
        _familyService = familyService;
    }

    [HttpGet]
    [Authorize(policy: Policies.ADMINISTRACAO)]
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

    [HttpGet("{familyId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
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
    [Authorize(policy: Policies.ADMINISTRACAO)]
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

        // ============= Fim validação de empresa e adquirimento do nome da empresa 

        var response = await _familyService.CreateAsync(request, Request, userCompanie.Name);
        return StatusCode(201, response);
    }

    [HttpDelete("{familyId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
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

        var companie = await _familyRepository.Search(userCompanie.Name, param);
        return Ok(companie);
    }

    [HttpPut]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult> Update([FromForm] UpdateFamilyViewModel request)
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

        await _familyService.UpdateAsync(request, Request, userCompanie.Name);
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
