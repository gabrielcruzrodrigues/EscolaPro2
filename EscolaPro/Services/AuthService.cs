using EscolaPro.Models.Dtos;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;

namespace EscolaPro.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginViewModel loginViewModel)
        {
            return await _authRepository.LoginAsync(loginViewModel);
        }
    }
}
