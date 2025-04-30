using EscolaPro.Models;
using EscolaPro.Repositories;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EscolaPro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly ICompanieRepository _companieRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly IFamilyRepository _familyRepository;
    private readonly IImageService _imageService;
    private readonly IFamilyService _familyService;
    private readonly IFinancialResponsibleService _financialResponsibleService;
    private readonly IFinancialResponsibleRepository _financialResponsibleRepository;

    public StudentController(
        ICompanieRepository companieRepository,
        IStudentRepository studentRepository,
        IUsersGeneralRepository usersGeneralRepository,
        IFamilyRepository familyRepository,
        IImageService imageService,
        IFamilyService familyService,
        IFinancialResponsibleService financialResponsibleService,
        IFinancialResponsibleRepository financialResponsibleRepository
    )
    {
        _companieRepository = companieRepository;
        _studentRepository = studentRepository;
        _userGeneralRepository = usersGeneralRepository;
        _familyRepository = familyRepository;
        _imageService = imageService;
        _familyService = familyService;
        _financialResponsibleService = financialResponsibleService;
        _financialResponsibleRepository = financialResponsibleRepository;
    }

    [HttpGet]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllAsync()
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

        var response = await _studentRepository.GetAllAsync(userCompanie.Name);
        return Ok(response);
    }

    [HttpGet("{studentId:int}")]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<Student>> GetByIdAsync(int studentId)
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

        var user = await _studentRepository.GetbyIdAsync(userCompanie.Name, studentId);
        return Ok(user);
    }

    [HttpPost]
    [Authorize(policy: "admin_internal")]
    public async Task<ActionResult<Allergy>> CreateAsync(CreateStudentViewModel request)
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

        if (await _studentRepository.GetByNameAsync(userCompanie.Name, request.Name) != null)
        {
            return Conflict(new { message = "Esse Nome já foi cadastrado", type = "name", code = 409 });
        }

        if (await _studentRepository.GetByEmailAsync(userCompanie.Name, request.Email) != null)
        {
            return Conflict(new { message = "Esse Email já foi cadastrado", type = "email", code = 409 });
        }

        if (await _studentRepository.GetByRgAsync(userCompanie.Name, request.Rg) != null)
        {
            return Conflict(new { message = "Esse RG já foi cadastrado", type = "rg", code = 409 });
        }

        if (await _studentRepository.GetByCpfAsync(userCompanie.Name, request.Cpf) != null)
        {
            return Conflict(new { message = "Esse CPF já foi cadastrado", type = "cpf", code = 409 });
        }

        if (await _studentRepository.GetByPhoneAsync(userCompanie.Name, request.Phone) != null)
        {
            return Conflict(new { message = "Esse Telefone já foi cadastrado", type = "phone", code = 409 });
        }

        string studentImageUrl = "";
        if (request.Image != null)
        {
            if (request.Image.Length == 0)
            {
                return BadRequest("Arquivo inváido");
            }

            var fileName = await _imageService.SaveImageInDatabaseAndReturnUrlAsync(request.Image);
            studentImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        }

        long financialResponsibleId = 0L;
        string financialResponsibleEmail = string.Empty;
        long fatherId = 0L;
        long motherId = 0L;

        // =================== Verifica se o responsável existe, se não, cria o responsável ===================
        if (request.ResponsibleId.HasValue)
        {
            var financialResponsible = await _financialResponsibleRepository.GetByIdAsync(userCompanie.Name, request.ResponsibleId.Value);
            if (financialResponsible == null)
                return NotFound("O responsável não foi encontrado com este Id.");

            financialResponsibleId = request.ResponsibleId.Value;
            financialResponsibleEmail = financialResponsible.Email;
        }
        else if (request.FinancialResponsible is not null)
        { 
            var financialResponsible = await _financialResponsibleService.CreateAsync(request.FinancialResponsible, Request, userCompanie.Name);
            financialResponsibleId = financialResponsible.Id;
            financialResponsibleEmail = financialResponsible.Email;
        }

        // =================== Verifica se o pai existe, se não, cria o pai ===================
        if (request.FatherId.HasValue)
        {
            var father = await _familyRepository.GetByIdAsync(userCompanie.Name, request.FatherId.Value);
            if (father == null)
                return NotFound("O Familiar não foi encontrado com este Id.");

            fatherId = request.FatherId.Value;
        }
        else if (request.Father is not null)
        {
            var father = await _familyService.CreateAsync(request.Father, Request, userCompanie.Name);
            fatherId = father.Id;
        }

        // =================== Verifica se a mãe existe, se não, cria o mãe ===================
        if (request.MotherId.HasValue)
        {
            var mother = await _familyRepository.GetByIdAsync(userCompanie.Name, request.MotherId.Value);
            if (mother == null)
                return NotFound("O Familiar não foi encontrado com este Id.");

            motherId = request.MotherId.Value;
        }
        else if (request.Mother is not null)
        {
            var mother = await _familyService.CreateAsync(request.Mother, Request, userCompanie.Name);
            motherId = mother.Id;
        }

        if (fatherId == 0 || motherId == 0 || financialResponsibleId == 0)
        {
            return BadRequest("O pai, mãe ou responsável financeiro não foram encontrados ou cadastrados corretamente!");
        }

        var student = new Student
        {
            Image = studentImageUrl,
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
            ResponsibleEmail = financialResponsibleEmail,
            FatherId = fatherId,
            MotherId = motherId,
            FinancialResponsibleId = financialResponsibleId,
            Situation = Enums.StudentSituationEnum.OK
        };

        var response = await _studentRepository.CreateAsync(userCompanie.Name, student);
        return StatusCode(201, response);
    }

    [HttpDelete("{studentId:int}")]
    [Authorize(policy: "admin_internal")]
    public async Task<IActionResult> DisableAsync(int studentId)
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

        await _studentRepository.DisableAsync(userCompanie.Name, studentId);
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

        var companie = await _studentRepository.SearchAsync(userCompanie.Name, param);
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
