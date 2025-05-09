using EscolaPro.Enums;
using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Models.Dtos;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.ViewModels.Educacional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace EscolaPro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGeneralController : ControllerBase
{
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly ISaltRepository _saltRepository;
    private readonly ICompanieRepository _companieRepository;

    public UserGeneralController(
        IUsersGeneralRepository userGeneralRepository, 
        ISaltRepository saltRepository,
        ICompanieRepository companieRepository
    )
    {
        _userGeneralRepository = userGeneralRepository;
        _saltRepository = saltRepository;
        _companieRepository = companieRepository;
    }

    [HttpGet]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<IEnumerable<UserGeneralDto>>> GetAllAsync()
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userIdVerify = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userIdVerify, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var response = await _userGeneralRepository.GetAllUsersAsync();
        return Ok(response);
    }

    [HttpGet("{userId:long}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<UserGeneralDto>> GetByIdAsync(long userId)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userIdVerify = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userIdVerify, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var user = await _userGeneralRepository.GetByIdAsync(userId);
        return Ok(user);
    }

    [HttpGet("last-active-users/{companieId:int}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<UserGeneralDto>> GetLast5ActiveUsers(int companieId)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userIdVerify = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userIdVerify, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var user = await _userGeneralRepository.GetLast5ActiveUsers(companieId);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous] //ADMINISTRACAO
    public async Task<ActionResult<UserGeneralDto>> CreateAsync(CreateUserGeneralViewModel request)
    {
        var emailVerify = await _userGeneralRepository.GetByEmailAsync(request.Email);
        if (emailVerify != null)
        {
            return Conflict(new { message = "Esse email já foi cadastrado", type = "email", code = 409 });
        }

        if (await _userGeneralRepository.GetByNameAsync(request.Name) != null)
        {
            return Conflict(new { message = "Esse nome já foi cadastrado", type = "name", code = 409 });
        }

        _ = await _companieRepository.GetByIdAsync(request.CompanieId);
        
        var user = new UserGeneral
        {
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastAccess = DateTime.UtcNow,
            Password = "passwordHashed",
            CompanieId = request.CompanieId,
            Status = true
        };

        var (hash, salt) = PasswordHashManager.HashGenerate(request.Password);
        user.Password = hash;

        var response = await _userGeneralRepository.CreateAsync(user);

        var saltObj = new Salt()
        {
            UserGeneralId = user.Id,
            SaltHash = salt
        };

        await _saltRepository.Create(saltObj);

        return StatusCode(201, response);
    }

    [HttpDelete("{userId:long}")]
    [Authorize(policy: Policies.ADMIN_MASTER)]
    public async Task<IActionResult> DisableAsync(long userId)
    {
        await _userGeneralRepository.Disable(userId);
        return StatusCode(204);
    }

    [HttpGet("search/{param}")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult> Search(string param)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userIdVerify = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userIdVerify, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var users = await _userGeneralRepository.Search(param);
        return Ok(users);
    }

    [HttpGet("roles")]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
    {
        var roles = Enum.GetValues(typeof(RolesEnum))
                    .Cast<RolesEnum>()
                    .Select(r => new RoleDto
                    {
                        Name = r.ToString(),
                        Value = (int)r
                    }).ToList();

        return Ok(roles);
    }


    [HttpPut]
    [Authorize(policy: Policies.ADMINISTRACAO)]
    public async Task<IActionResult> UpdateAsync(UpdateUserGeneralViewModel request)
    {
        // ============= Início validação de empresa e adquirimento do nome da empresa =============

        var userIdVerify = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

        if (!await CompanieValidation(userIdVerify, userCompanieId))
        {
            return BadRequest("Você não tem acesso a essa empresa!");
        }
        var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

        // ============= Fim validação de empresa e adquirimento do nome da empresa =============

        var user = await _userGeneralRepository.GetByIdWithTrackingAsync(request.Id.Value);
        var emailVerify = await _userGeneralRepository.GetByEmailAsync(request.Email);
        var nameVerify = await _userGeneralRepository.GetByNameAsync(request.Name);

        if (emailVerify != null && emailVerify.Email != user.Email)
            return Conflict("Este email já esta cadastrado, tente um email diferente!");

        if (nameVerify != null && nameVerify.Name != user.Name)
            return Conflict("Este nome já esta cadastrado, tente um nome diferente!");

        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;

        if (request.CompanieId != null)
        {
            var companie = await _companieRepository.GetByIdAsync(request.CompanieId.Value);
            user.CompanieId = companie.Id;
        }

        if (request.Role != null)
        {
            if (request.Role == 0)
            {
                user.Role = RolesEnum.ADMIN_MASTER;
            }
            if (request.Role == 2)
            {
                user.Role = RolesEnum.MODERADOR_MASTER;
            }
        }

        user.LastUpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrEmpty(request.Password))
        {
            var salt = await _saltRepository.GetByUserId(user.Id);
            var (hash, saltHash) = PasswordHashManager.HashGenerate(request.Password);
            user.Password = hash;
            await _saltRepository.Update(user.Id, saltHash);
            await _userGeneralRepository.Update(user);
        }
        else
        {
            await _userGeneralRepository.Update(user);
        }

        return StatusCode(204);
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
