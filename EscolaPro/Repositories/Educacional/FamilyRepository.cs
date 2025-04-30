namespace EscolaPro.Repositories.Educacional;

using EscolaPro.Database.Interfaces;
using EscolaPro.Extensions;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FamilyRepository : IFamilyRepository
{
    private readonly IDbContextFactory _contextFactory;
    private readonly ILogger<FamilyRepository> _logger;

    public FamilyRepository(IDbContextFactory contextFactory, ILogger<FamilyRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<Family> CreateAsync(string companieName, Family family)
    {
        try
        {
            using var context = _contextFactory.Create(companieName);
            await context.Families.AddAsync(family);
            await context.SaveChangesAsync();
            return family;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar familiar. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar um familiar!");
        }
    }

    public async Task Disable(string companieName, long familyId)
    {
        var family = await GetByIdAsync(companieName, familyId);
        family.Status = false;

        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Families.Entry(family).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar desativar um familiar! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar um familiar!");
        }
    }

    public async Task<IEnumerable<Family>> GetAllAsync(string companieName)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Families
                .AsNoTracking()
                .Where(u => u.Status.Equals(true))
                .ToListAsync();
    }

    public async Task<Family> GetByIdAsync(string companieName, long familyId)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Id.Equals(familyId))
                .FirstOrDefaultAsync();

        if (family is null)
        {
            throw new HttpResponseException(404, $"familiar não encontrado com esse nome!");
        }

        return family;
    }

    public async Task<Family> GetByNameAsync(string companieName, string familyName)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Name.Equals(familyName))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<Family> GetByEmailAsync(string companieName, string familyEmail)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Email.Equals(familyEmail))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<Family> GetByRgAsync(string companieName, string familyRg)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Rg.Equals(familyRg))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<Family> GetByCpfAsync(string companieName, string familyCpf)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Cpf.Equals(familyCpf))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<Family> GetByPhoneAsync(string companieName, string familyPhone)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Status.Equals(true) && u.Phone.Equals(familyPhone))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<IEnumerable<Family>> Search(string companieName, string param)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Families
                .Where(u => u.Name.Contains(param) && u.Status.Equals(true))
                .ToListAsync();

        return family;
    }

    public async Task Update(string companieName, Family familyForUpdate)
    {
        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Families.Entry(familyForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar atualizar um familiar! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar um familiar!");
        }
    }
}
