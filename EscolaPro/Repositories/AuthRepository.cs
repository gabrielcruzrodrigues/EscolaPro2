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
using EscolaPro.Models;
using EscolaPro.Enums;

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
        var userVerifyWithEmail = await _userGeneralRepository.GetByEmailAsync(loginViewModel.Login);
        var userVerifyWithName = await _userGeneralRepository.GetByNameAsync(loginViewModel.Login);

        if (userVerifyWithEmail is null && userVerifyWithName is null)
        {
            throw new HttpResponseException(404, "Usuário não encontrado!");
        }

        string? salt = null;
        try
        {
            if (userVerifyWithEmail is not null)
            {
                salt = await _context.Salts
                    .AsNoTracking()
                    .Where(s => s.UserGeneralId.Equals(userVerifyWithEmail.Id))
                    .Select(s => s.SaltHash)
                    .FirstAsync();
            }
            else if (userVerifyWithName is not null)
            {
                salt = await _context.Salts
                    .AsNoTracking()
                    .Where(s => s.UserGeneralId.Equals(userVerifyWithName.Id))
                    .Select(s => s.SaltHash)
                    .FirstAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar buscar o salt no banco de dados! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar buscar o salt no banco de dados!");
        }

        if (userVerifyWithEmail is not null)
        {
            if (!PasswordHashManager.VerifyPassword(loginViewModel.Password, userVerifyWithEmail.Password, salt))
            {
                throw new HttpResponseException(400, "Senha incorreta!");
            }

            var userRole = userVerifyWithEmail.Role;

            IEnumerable<Claim> authClaims = new List<Claim>();
            authClaims = GetClaims(userRole, userVerifyWithEmail);

            var token = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            var verify = int.TryParse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES"),
                out int refreshTokenValidityInMinutes);
            if (!verify)
            {
                throw new InvalidOperationException("Valor inválido para JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES");
            }

            userVerifyWithEmail.RefreshToken = refreshToken;
            userVerifyWithEmail.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
            userVerifyWithEmail.LastAccess = DateTime.UtcNow;

            try
            {
                _context.Entry(userVerifyWithEmail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Um erro aconteceu ao tentar criar os tokens de acesso! err: {ex.Message}");
                throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar os tokens de acesso!");
            }

            return new LoginResponseDto
            {
                UserId = userVerifyWithEmail.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                Role = userRole,
                Name = userVerifyWithEmail.Name,
                CompanieId = userVerifyWithEmail.CompanieId
            };
        }
        else
        {
            if (!PasswordHashManager.VerifyPassword(loginViewModel.Password, userVerifyWithName.Password, salt))
            {
                throw new HttpResponseException(400, "Senha incorreta!");
            }

            var userRole = userVerifyWithName.Role;

            IEnumerable<Claim> authClaims = new List<Claim>();
            authClaims = GetClaims(userRole, userVerifyWithName);

            var token = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var verify = int.TryParse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES"),
                out int refreshTokenValidityInMinutes);
            if (!verify)
            {
                throw new InvalidOperationException("Valor inválido para JWT_REFRESH_TOKEN_VALIDITY_IN_MINUTES");
            }

            userVerifyWithName.RefreshToken = refreshToken;
            userVerifyWithName.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
            userVerifyWithName.LastAccess = DateTime.UtcNow;

            try
            {
                _context.Entry(userVerifyWithName).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Um erro aconteceu ao tentar criar os tokens de acesso! err: {ex.Message}");
                throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar os tokens de acesso!");
            }

            return new LoginResponseDto
            {
                UserId = userVerifyWithName.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                Role = userRole,
                Name = userVerifyWithName.Name,
                CompanieId = userVerifyWithName.CompanieId
            };
        }
    }

    private IEnumerable<Claim> GetClaims(RolesEnum userRole, UserGeneral userGeneral)
    {
        IEnumerable<Claim> authClaims = new List<Claim>();

        if (userRole.Equals(RolesEnum.ADMIN))
        {
            authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userGeneral.Id.ToString()),
                new Claim(ClaimTypes.Name, userGeneral.Name!),
                new Claim(ClaimTypes.Email, userGeneral.Email!),
                new Claim("CompanieId", userGeneral.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", RolesEnum.ADMIN.ToString().ToLower()),
                new Claim("Role", RolesEnum.MODERADOR.ToString().ToLower()),
                new Claim("Role", RolesEnum.USER.ToString().ToLower()),
                new Claim("Role", RolesEnum.ADMIN_INTERNAL.ToString().ToLower()),
            };
        }

        if (userRole.Equals(RolesEnum.MODERADOR))
        {
            authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userGeneral.Id.ToString()),
                new Claim(ClaimTypes.Name, userGeneral.Name!),
                new Claim(ClaimTypes.Email, userGeneral.Email!),
                new Claim("CompanieId", userGeneral.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", RolesEnum.MODERADOR.ToString().ToLower()),
                new Claim("Role", RolesEnum.USER.ToString().ToLower()),
            };
        }

        if (userRole.Equals(RolesEnum.USER))
        {
            authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userGeneral.Id.ToString()),
                new Claim(ClaimTypes.Name, userGeneral.Name!),
                new Claim(ClaimTypes.Email, userGeneral.Email!),
                new Claim("CompanieId", userGeneral.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", RolesEnum.USER.ToString().ToLower()),
            };
        }

        //Admin geral de cada empresa
        if (userRole.Equals(RolesEnum.ADMIN_INTERNAL))
        {
            authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userGeneral.Id.ToString()),
                new Claim(ClaimTypes.Name, userGeneral.Name!),
                new Claim(ClaimTypes.Email, userGeneral.Email!),
                new Claim("CompanieId", userGeneral.CompanieId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", RolesEnum.ADMIN_INTERNAL.ToString().ToLower()),
            };
        }

        return authClaims;
    }
}
