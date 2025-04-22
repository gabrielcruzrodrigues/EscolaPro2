using EscolaPro.Models;
using EscolaPro.Repositories;
using EscolaPro.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly IFamilyRepository _familyRepository;

    public FamilyController(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    [HttpGet]
    [Authorize(policy: "admin")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Companie>>> GetAllAsync()
    {
        var response = await _companieRepository.GetAllAsync();
        return Ok(response);
    }

}
