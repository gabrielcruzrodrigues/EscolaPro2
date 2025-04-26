﻿namespace EscolaPro.Repositories;

using EscolaPro.Database.Interfaces;
using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AllergieRepository : IAllergieRepository
{
    private readonly IDbContextFactory _contextFactory;
    private readonly ILogger<AllergieRepository> _logger;

    public AllergieRepository(IDbContextFactory contextFactory, ILogger<AllergieRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<Allergie> CreateAsync(string companieName, Allergie allergie)
    {
        try
        {
            using var context = _contextFactory.Create(companieName);
            await context.Allergies.AddAsync(allergie);
            await context.SaveChangesAsync();
            return allergie;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar alergia. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar uma alergia!");
        }
    }

    public async Task Disable(string companieName, int allergieId)
    {
        var allergie = await GetByIdAsync(companieName, allergieId);
        allergie.Status = false;

        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Allergies.Entry(allergie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desativar alergia. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar uma alergia!");
        }
    }

    public async Task<IEnumerable<Allergie>> GetAllAsync(string companieName)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Allergies
                .AsNoTracking()
                .Where(u => u.Status.Equals(true))
                .ToListAsync();
    }

    public async Task<Allergie> GetByIdAsync(string companieName, int allergieId)
    {
        using var _context = _contextFactory.Create(companieName);

        var allergie = await _context.Allergies
                .Where(a => a.Status.Equals(true) && a.Id.Equals(allergieId))
                .FirstOrDefaultAsync();

        if (allergie is null)
        {
            throw new HttpResponseException(404, $"alergia não encontrada com esse Id!");
        }

        return allergie;
    }

    public async Task<Allergie> GetByNameAsync(string companieName, string allergieName)
    {
        using var _context = _contextFactory.Create(companieName);

        var family = await _context.Allergies
                .Where(u => u.Status.Equals(true) && u.Name.Equals(allergieName))
                .FirstOrDefaultAsync();

        return family;
    }

    public async Task<IEnumerable<Allergie>> Search(string companieName, string param)
    {
        using var _context = _contextFactory.Create(companieName);

        var allergie = await _context.Allergies
                .Where(u => u.Name.Contains(param) && u.Status.Equals(true))
                .ToListAsync();

        return allergie;
    }

    public async Task Update(string companieName, Allergie allergieForUpdate)
    {
        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Allergies.Entry(allergieForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar alergia. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar uma alergia!");
        }
    }
}
