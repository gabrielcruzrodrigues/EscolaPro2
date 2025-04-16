using AutoMapper;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Database;
using EscolaPro.Extensions;
using Microsoft.EntityFrameworkCore;
using EscolaPro.Models.Dtos;

namespace EscolaPro.Repositories;

public class CompanieRepository : ICompanieRepository
{
    private readonly GeneralDbContext _context;
    private readonly ILogger<CompanieRepository> _logger;

    public CompanieRepository(GeneralDbContext context, ILogger<CompanieRepository> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public async Task<Companies> CreateAsync(Companies companie)
    {
        try
        {
            _ = await _context.Companies.AddAsync(companie);
            await _context.SaveChangesAsync();
            return companie;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar criar uma empresa! Err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar uma empresa!");
        }
    }

    public async Task Disable(int companieId)
    {
        var companie = await GetByIdAsync(companieId);
        companie.Status = false;

        try
        {
            _context.Companies.Entry(companie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar desativar uma empresa! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar uma empresa!");
        }
    }

    public async Task<IEnumerable<Companies>> GetAllAsync()
    {
        return await _context.Companies
                .AsNoTracking()
                .Where(u => u.Status.Equals(true))
                .ToListAsync();
    }

    public async Task<Companies> GetByIdAsync(int companieId)
    {
        var companie = await _context.Companies
                .Where(u => u.Status.Equals(true) && u.Id.Equals(companieId))
                .FirstOrDefaultAsync();

        if (companie is null)
        {
            throw new HttpResponseException(404, $"Empresa não encontrada!");
        }

        return companie;
    }

    public async Task<Companies> GetByNameAsync(string companieName)
    {
        var companie = await _context.Companies
                .Where(u => u.Status.Equals(true) && u.Name.Equals(companieName))
                .FirstOrDefaultAsync();

        return companie;
    }

    public async Task<Companies> GetByCnpjAsync(string companieCnpj)
    {
        var companie = await _context.Companies
                .Where(u => u.Status.Equals(true) && u.CNPJ.Equals(companieCnpj))
                .FirstOrDefaultAsync();

        return companie;
    }

    public async Task Update(Companies companieForUpdate)
    {
        try
        {
            _context.Companies.Entry(companieForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Um erro aconteceu ao tentar atualizar uma empresa! err: {ex.Message}");
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar uma empresa!");
        }
    }

    public async Task<IEnumerable<Companies>> Search(string param)
    {
        var companie = await _context.Companies
                .Where(u => u.Name.Contains(param) && u.Status.Equals(true) || u.CNPJ.Contains(param) && u.Status.Equals(true))
                .ToListAsync();

        return companie;
    }
}
