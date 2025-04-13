using EscolaPro.Models.Dtos;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto> LoginAsync(LoginViewModel loginViewModel);
    }
}
