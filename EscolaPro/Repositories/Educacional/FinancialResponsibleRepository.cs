using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.ViewModels;
using EscolaPro.Database.Interfaces;
using EscolaPro.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Repositories.Educacional;

public class FinancialResponsibleRepository : IFinancialResponsibleRepository
{
    private readonly IDbContextFactory _contextFactory;
    private readonly ILogger<FinancialResponsibleRepository> _logger;

    public FinancialResponsibleRepository(IDbContextFactory contextFactory, ILogger<FinancialResponsibleRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<FinancialResponsible> CreateAsync(string companieName, FinancialResponsible financialResponsible)
    {
        try
        {
            using var context = _contextFactory.Create(companieName);
            await context.FinancialResponsibles.AddAsync(financialResponsible);
            await context.SaveChangesAsync();
            return financialResponsible;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar Responsável Financeiro. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar um Responsável Financeiro!");
        }
    }

    public async Task Disable(string companieName, long financialResponsibleId)
    {
        var financialResponsible = await GetByIdAsync(companieName, financialResponsibleId);
        financialResponsible.Status = false;

        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.FinancialResponsibles.Entry(financialResponsible).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desabilitar Responsável Financeiro. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desabilitar um Responsável Financeiro!");
        }
    }

    public async Task<IEnumerable<FinancialResponsible>> GetAllAsync(string companieName)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .AsNoTracking()
                .Where(u => u.Status.Equals(true))
                .ToListAsync();
    }

    public async Task<FinancialResponsible> GetByCpfAsync(string companieName, string financialResponsibleCpf)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Cpf.Equals(financialResponsibleCpf))
                .FirstOrDefaultAsync();
    }

    public async Task<FinancialResponsible> GetByEmailAsync(string companieName, string financialResponsibleEmail)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Email.Equals(financialResponsibleEmail))
                .FirstOrDefaultAsync();
    }

    public async Task<FinancialResponsible> GetByIdAsync(string companieName, long id)
    {
        using var _context = _contextFactory.Create(companieName);

        var financialResponsible = await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Id.Equals(id))
                .FirstOrDefaultAsync();

        if (financialResponsible is null)
        {
            throw new HttpResponseException(404, $"Responsável financeiro não encontrado com esse nome!");
        }

        return financialResponsible;
    }

    public async Task<FinancialResponsible> GetByNameAsync(string companieName, string financialResponsibleName)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Name.Equals(financialResponsibleName))
                .FirstOrDefaultAsync();
    }

    public async Task<FinancialResponsible> GetByPhoneAsync(string companieName, string financialResponsiblePhone)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Phone.Equals(financialResponsiblePhone))
                .FirstOrDefaultAsync();
    }

    public async Task<FinancialResponsible> GetByRgAsync(string companieName, string financialResponsibleRg)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Status.Equals(true) && u.Rg.Equals(financialResponsibleRg))
                .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FinancialResponsible>> Search(string companieName, string param)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FinancialResponsibles
                .Where(u => u.Name.Contains(param) && u.Status.Equals(true))
                .ToListAsync();
    }

    public async Task Update(string companieName, FinancialResponsible financialResponsibleForUpdate)
    {
        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.FinancialResponsibles.Entry(financialResponsibleForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Responsável Financeiro. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar um Responsável Financeiro!");
        }
    }
}
