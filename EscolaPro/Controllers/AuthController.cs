using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel request)
    {
        var credentials = await _authService.LoginAsync(request);
        return Ok(credentials);
    }

    [HttpPost("logout")]
    [Authorize(policy: "user")]
    public async Task<ActionResult> Logout()
    {
        return Ok(new { message = "Logout realizado com sucesso!" });
    }
}
