using EscolaPro.Database;
using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Repositories;

public class SaltRepository : ISaltRepository
{
    private readonly GeneralDbContext _context;
    private readonly ILogger<SaltRepository> _logger;

    public SaltRepository(GeneralDbContext context, ILogger<SaltRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Create(Salts salt)
    {
        try
        {
            await _context.Salts.AddAsync(salt);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar salvar o Salts no banco de dados! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar salvar o Salts no banco de dados!");
        }
    }

    public async Task<Salts> GetByUserId(long userId)
    {
        var salt = await _context.Salts
            .Where(s => s.UserGeneralId.Equals(userId))
            .FirstOrDefaultAsync();

        if (salt is null)
        {
            throw new HttpResponseException(404, "Salt de usuário não encontrado!");
        }

        return salt;
    }

    public async Task Update(long userId, string newSalt)
    {
        var salt = await GetByUserId(userId);
        salt.SaltHash = newSalt;

        try
        {
            _context.Salts.Entry(salt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar atualizar o Salts no banco de dados! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar o Salts no banco de dados!");
        }
    }
}
