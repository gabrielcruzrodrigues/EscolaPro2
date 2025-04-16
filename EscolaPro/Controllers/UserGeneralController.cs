using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Models.Dtos;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
    [Authorize(policy: "admin")]
    public async Task<ActionResult<IEnumerable<UserGeneralDto>>> GetAllAsync()
    {
        var response = await _userGeneralRepository.GetAllUsersAsync();
        return Ok(response);
    }

    [HttpGet("{userId:long}")]
    [Authorize(policy: "admin")]
    public async Task<ActionResult<UserGeneralDto>> GetByIdAsync(long userId)
    {
        var user = await _userGeneralRepository.GetByIdAsync(userId);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
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
            Password = "passwordHashed",
            CompanieId = request.CompanieId,
            Status = true
        };

        var (hash, salt) = PasswordHashManager.HashGenerate(request.Password);
        user.Password = hash;

        var response = await _userGeneralRepository.CreateAsync(user);

        var saltObj = new Salts()
        {
            UserGeneralId = user.Id,
            SaltHash = salt
        };

        await _saltRepository.Create(saltObj);

        return StatusCode(201, response);
    }

    [HttpDelete("{userId:long}")]
    [Authorize(policy: "admin")]
    public async Task<IActionResult> DisableAsync(long userId)
    {
        await _userGeneralRepository.Disable(userId);
        return StatusCode(204);
    }

    [HttpGet("search/{param}")]
    [Authorize(policy: "admin")]
    public async Task<ActionResult> Search(string param)
    {
        var users = await _userGeneralRepository.Search(param);
        return Ok(users);
    }

    [HttpPut]
    [Authorize(policy: "admin")]
    public async Task<IActionResult> UpdateAsync(UpdateUserGeneralViewModel request)
    {
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
                user.Role = Roles.ADMIN;
            }
            if (request.Role == 2)
            {
                user.Role = Roles.MODERADOR;
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
}
