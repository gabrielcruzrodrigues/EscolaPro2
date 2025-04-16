namespace EscolaPro.Repositories;


using Microsoft.EntityFrameworkCore;
using EscolaPro.Database;
using EscolaPro.Extensions;
using EscolaPro.Models.Dtos;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using EscolaPro.ViewModels;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

public class AuthRepository : IAuthRepository
{
    private readonly IUsersGeneralRepository _userGeneralRepository;
    private readonly ILogger<AuthRepository> _logger;
    private readonly ITokenService _tokenService;
    private readonly GeneralDbContext _context;

    public AuthRepository(
        IUsersGeneralRepository userGeneralRepository,
        ILogger<AuthRepository> logger,
        ITokenService tokenService,
        GeneralDbContext context
    )
    {
        _userGeneralRepository = userGeneralRepository;
        _logger = logger;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginViewModel loginViewModel)
    {
        var emailVerify = await _userGeneralRepository.GetByEmailAsync(loginViewModel.Login);
        var userNameVerify = await _userGeneralRepository.GetByNameAsync(loginViewModel.Login);

        if (emailVerify is null && userNameVerify is null)
        {
            throw new HttpResponseException(404, "Usuário não encontrado!");
        }

        string? salt = null;
        try
        {
            if (emailVerify is not null)
            {
                salt = await _context.Salts
                    .AsNoTracking()
                    .Where(s => s.UserGeneralId.Equals(emailVerify.Id))
                    .Select(s => s.SaltHash)
                    .FirstAsync();
            }
            else if (userNameVerify is not null)
            {
                salt = await _context.Salts
                    .AsNoTracking()
                    .Where(s => s.UserGeneralId.Equals(userNameVerify.Id))
                    .Select(s => s.SaltHash)
                    .FirstAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar buscar o salt no banco de dados! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar buscar o salt no banco de dados!");
        }

        if (emailVerify is not null)
        {
            if (!PasswordHashManager.VerifyPassword(loginViewModel.Password, emailVerify.Password, salt))
            {
                throw new HttpResponseException(400, "Senha incorreta!");
            }

            var userRole = emailVerify.Role;
            IEnumerable<Claim> authClaims = new List<Claim>();

            if (userRole.Equals(Roles.ADMIN))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emailVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, emailVerify.Name!),
                new Claim(ClaimTypes.Email, emailVerify.Email!),
                new Claim("Companie", emailVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.ADMIN.ToString().ToLower()),
                new Claim("Role", Roles.MODERADOR.ToString().ToLower()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            if (userRole.Equals(Roles.MODERADOR))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emailVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, emailVerify.Name!),
                new Claim(ClaimTypes.Email, emailVerify.Email!),
                new Claim("Companie", emailVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.MODERADOR.ToString().ToLower()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            if (userRole.Equals(Roles.USER))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emailVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, emailVerify.Name!),
                new Claim(ClaimTypes.Email, emailVerify.Email!),
                new Claim("Companie", emailVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            var token = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            var verify = int.TryParse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES"),
                out int refreshTokenValidityInMinutes);
            if (!verify)
            {
                throw new InvalidOperationException("Valor inválido para JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES");
            }

            emailVerify.RefreshToken = refreshToken;
            emailVerify.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            try
            {
                _context.Entry(emailVerify).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Um erro aconteceu ao tentar criar os tokens de acesso! err: {ex.Message}");
                throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar os tokens de acesso!");
            }

            return new LoginResponseDto
            {
                UserId = emailVerify.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                Role = userRole,
                Name = emailVerify.Name
            };
        }
        else
        {
            if (!PasswordHashManager.VerifyPassword(loginViewModel.Password, userNameVerify.Password, salt))
            {
                throw new HttpResponseException(400, "Senha incorreta!");
            }

            var userRole = userNameVerify.Role;
            IEnumerable<Claim> authClaims = new List<Claim>();

            if (userRole.Equals(Roles.ADMIN))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userNameVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, userNameVerify.Name!),
                new Claim(ClaimTypes.Email, userNameVerify.Email!),
                new Claim("Companie", userNameVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.ADMIN.ToString().ToLower()),
                new Claim("Role", Roles.MODERADOR.ToString().ToLower()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            if (userRole.Equals(Roles.MODERADOR))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userNameVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, userNameVerify.Name!),
                new Claim(ClaimTypes.Email, userNameVerify.Email!),
                new Claim("Companie", userNameVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.MODERADOR.ToString().ToLower()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            if (userRole.Equals(Roles.USER))
            {
                authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userNameVerify.Id.ToString()),
                new Claim(ClaimTypes.Name, userNameVerify.Name!),
                new Claim(ClaimTypes.Email, userNameVerify.Email!),
                new Claim("Companie", userNameVerify.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", Roles.USER.ToString().ToLower()),
            };
            }

            var token = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            var verify = int.TryParse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES"),
                out int refreshTokenValidityInMinutes);
            if (!verify)
            {
                throw new InvalidOperationException("Valor inválido para JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES");
            }

            userNameVerify.RefreshToken = refreshToken;
            userNameVerify.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            try
            {
                _context.Entry(userNameVerify).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Um erro aconteceu ao tentar criar os tokens de acesso! err: {ex.Message}");
                throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar os tokens de acesso!");
            }

            return new LoginResponseDto
            {
                UserId = userNameVerify.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                Role = userRole,
                Name = userNameVerify.Name
            };
        }
    }
}
