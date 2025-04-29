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

    public StudentController(
        ICompanieRepository companieRepository,
        IStudentRepository studentRepository,
        IUsersGeneralRepository usersGeneralRepository,
        IFamilyRepository familyRepository,
        IImageService imageService
    )
    {
        _companieRepository = companieRepository;
        _studentRepository = studentRepository;
        _userGeneralRepository = usersGeneralRepository;
        _familyRepository = familyRepository;
        _imageService = imageService;
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

        //long responsibleId;
        //string responsibleEmail;
        long fatherId;
        long motherId;

        // =================== Verifica se o responsável existe, se não, cria o responsável ===================
        //if (request.ResponsibleId.HasValue)
        //{
        //    var responsible = await _familyRepository.GetByIdAsync(userCompanie.Name, request.ResponsibleId.Value);
        //    if (responsible == null)
        //        return NotFound("O responsável não foi encontrado com este Id.");

        //    responsibleId = request.ResponsibleId.Value;
        //} 
        //else
        //{
        //    if (request.Responsible == null)
        //        return BadRequest("É necessário fornecer o Id do responsável ou os dados para cadastrá-lo.");

        //    var responsible = await _familyRepository.CreateAsync(userCompanie.Name, request.Responsible);
        //    responsibleId = responsible.Id;
        //}

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
            var father = await _familyRepository.CreateAsync(userCompanie.Name, request.Father);
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
            var mother = await _familyRepository.CreateAsync(userCompanie.Name, request.Mother);
            motherId = mother.Id;
        }

        //var student = new Student
        //{
        //    Image = imageFamilyUrl,
        //    Name = request.Name,
        //    Email = request.Email,
        //    Rg = request.Rg,
        //    Cpf = request.Cpf,
        //    DateOfBirth = request.DateOfBirth,
        //    Nationality = request.Nationality,
        //    Naturalness = request.Naturalness,
        //    Sex = request.Sex,
        //    Cep = request.Cep,
        //    Address = request.Address,
        //    Phone = request.Phone,
        //    Neighborhood = request.Neighborhood,
        //    City = request.City,
        //    State = request.State,
        //    Role = request.Role,
        //    Status = true,
        //    CreatedAt = DateTime.UtcNow,
        //    LastUpdatedAt = DateTime.UtcNow,
        //    ResponsibleEmail = 
        //}

        //var response = await _allergieRepository.CreateAsync(userCompanie.Name, allergie);
        return StatusCode(201);
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
