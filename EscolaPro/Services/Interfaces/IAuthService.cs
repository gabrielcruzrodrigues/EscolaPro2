using EscolaPro.Models.Dtos;
using EscolaPro.ViewModels;

namespace EscolaPro.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginViewModel loginViewModel);
}
