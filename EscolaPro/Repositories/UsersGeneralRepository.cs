using AutoMapper;
using EscolaPro.Extensions;
using EscolaPro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using EscolaPro.Database;
using EscolaPro.Models.Dtos;
using EscolaPro.Models;
namespace EscolaPro.Repositories;

public class UsersGeneralRepository : IUsersGeneralRepository
{
    private readonly GeneralDbContext _context;
    private readonly ILogger<UsersGeneralRepository> _logger;
    private readonly IMapper _mapper;

    public UsersGeneralRepository(GeneralDbContext context, ILogger<UsersGeneralRepository> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserGeneralDto> CreateAsync(UserGeneral user)
    {
        try
        {
            _ = await _context.UsersGeneral.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserGeneralDto>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar criar um usuário! Err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar um usuário!");
        }
    }

    public async Task<IEnumerable<UserGeneralDto>> Search(string param)
    {
        var users = await _context.UsersGeneral
                .Where(u => u.Name.Contains(param) && u.Status.Equals(true) || u.Email.Contains(param) && u.Status.Equals(true))
                .ToListAsync();

        return _mapper.Map<IEnumerable<UserGeneralDto>>(users);
    }

    public async Task Disable(long userId)
    {
        var user = await GetByIdWithTrackingAsync(userId);
        user.Status = false;

        try
        {
            _context.UsersGeneral.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar desativar um usuário! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar um usuário!");
        }
    }

    public async Task<IEnumerable<UserGeneralDto>> GetAllUsersAsync()
    {
        var users = await _context.UsersGeneral
            .AsNoTracking()
            .Where(u => u.Status.Equals(true))
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserGeneralDto>>(users);
    }

    public async Task<UserGeneralDto> GetByIdAsync(long userId)
    {
        var user = await _context.UsersGeneral
            .AsNoTracking()
            .Where(u => u.Status.Equals(true) && u.Id.Equals(userId))
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new HttpResponseException(404, $"Usuário não encontrado!");
        }

        return _mapper.Map<UserGeneralDto>(user);
    }

    public async Task<UserGeneral> GetByIdWithTrackingAsync(long userId)
    {
        var user = await _context.UsersGeneral
            .Where(u => u.Status.Equals(true) && u.Id.Equals(userId))
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new HttpResponseException(404, $"Usuário não encontrado!");
        }

        return user;
    }

    public async Task<UserGeneral> GetByEmailAsync(string email)
    {
        var user = await _context.UsersGeneral
            .AsNoTracking()
            .Where(u => u.Email.Equals(email))
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<UserGeneral> GetByNameAsync(string name)
    {
        var user = await _context.UsersGeneral
            .AsNoTracking()
            .Where(u => u.Name.Equals(name))
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task Update(UserGeneral userForUpdate)
    {
        try
        {
            _context.UsersGeneral.Entry(userForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar atualizar um usuário! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar um usuário!");
        }
    }
}
