using EscolaPro.Enums;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.ViewModels.Educacional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EscolaPro.Controllers.Educacional;

[Route("api/[controller]")]
[ApiController]
public class FixedHealthController : ControllerBase
{
    private readonly IFixedHealthRepository _fixedHealthRepository;
    private readonly ICompanieRepository _companieRepository;
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IAllergieRepository _allergieRepository;

    public FixedHealthController(
        IFixedHealthRepository fixedHealthRepository,
        ICompanieRepository companieRepository,
        IUsersGeneralRepository userGeneralRepository,
        IStudentRepository studentRepository,
        IAllergieRepository allergieRepository
    )
    {
        _fixedHealthRepository = fixedHealthRepository;
        _companieRepository = companieRepository;
        _userGeneralRepository = userGeneralRepository;
        _studentRepository = studentRepository;
        _allergieRepository = allergieRepository;
    }

    [HttpGet]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<IEnumerable<FixedHealth>>> GetAllAsync()
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

        var response = await _fixedHealthRepository.GetAllAsync(userCompanie.Name);
        return Ok(response);
    }

    [HttpGet("{fixedHealthId:int}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<FixedHealth>> GetByIdAsync(int fixedHealthId)
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

        var user = await _fixedHealthRepository.GetByIdAsync(userCompanie.Name, fixedHealthId);
        return Ok(user);
    }

    [HttpPost]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<FixedHealth>> CreateAsync(CreateFixedHealthViewModel request)
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

        var student = await _studentRepository.GetbyIdAsync(userCompanie.Name, request.StudentId);

        if (student == null)
        {
            return NotFound("Estudante não encontrado no banco de dados!");
        }

        if (student.FixedHealth != null)
        {
            return BadRequest("Este Estudante já tem uma fixa de saúde cadastrada!");
        }

        List<Allergy> allergies = new List<Allergy>();
        foreach (int allergieId in request.AllergiesId)
        {
            allergies.Add(await _allergieRepository.GetByIdAsync(userCompanie.Name, allergieId));
        }

        var fixedHealth = new FixedHealth
        {
            StudentId = request.StudentId,
            BloodGroup = request.BloodGroup,
            QuantityBrothers = request.QuantityBrothers,
            ToGoOutAuthorization = request.ToGoOutAuthorization,
            Allergies = allergies,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = true
        };

        var response = await _fixedHealthRepository.CreateAsync(userCompanie.Name, fixedHealth);
        return StatusCode(201, response);
    }

    [HttpDelete("{fixedHealthId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<IActionResult> DisableAsync(int fixedHealthId)
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

        await _fixedHealthRepository.DisableAsync(userCompanie.Name, fixedHealthId);
        return StatusCode(204);
    }

    [HttpGet("search/{fixedHealthId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult> SearchByStudentId(long fixedHealthId)
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

        var companie = await _fixedHealthRepository.SearchByStudentId(userCompanie.Name, fixedHealthId);
        return Ok(companie);
    }

    [HttpPut]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult> Update(UpdateFixedHealthViewModel request)
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

        FixedHealth fixedHealth = await _fixedHealthRepository.GetByIdAsync(userCompanie.Name, request.StudentId);

        //fixedHealth.Allergies = null;
        //await _fixedHealthRepository.UpdateAsync(userCompanie.Name, fixedHealth);

        //List<Allergie> allergies = new List<Allergie>();

        //foreach (int allergieId in request.AllergiesId)
        //{
        //    var allergie = await _allergieRepository.GetByIdAsync(userCompanie.Name, allergieId);
        //    fixedHealth.Allergies.Add(allergie);
        //}

        fixedHealth.BloodGroup = request.BloodGroup ?? fixedHealth.BloodGroup;
        fixedHealth.QuantityBrothers = request.QuantityBrothers ?? fixedHealth.QuantityBrothers;
        fixedHealth.ToGoOutAuthorization = request.ToGoOutAuthorization ?? fixedHealth.ToGoOutAuthorization;
        fixedHealth.UpdatedAt = DateTime.UtcNow;

        fixedHealth.Allergies = new List<Allergy>();
        foreach (int allergieId in request.AllergiesId)
        {
            var allergie = await _allergieRepository.GetByIdAsync(userCompanie.Name, allergieId);
            fixedHealth.Allergies.Add(allergie);
        }

        await _fixedHealthRepository.UpdateAsync(userCompanie.Name, fixedHealth);
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
