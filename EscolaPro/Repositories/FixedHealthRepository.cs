namespace EscolaPro.Repositories;

using EscolaPro.Database.Interfaces;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaPro.Extensions;
using Microsoft.EntityFrameworkCore;

public class FixedHealthRepository : IFixedHealthRepository
{
    private readonly IDbContextFactory _contextFactory;
    private readonly ILogger<FixedHealth> _logger;

    public FixedHealthRepository(IDbContextFactory dbContextFactory, ILogger<FixedHealth> logger)
    {
        _contextFactory = dbContextFactory;
        _logger = logger;
    }
    
    public async Task<FixedHealth> CreateAsync(string companieName, FixedHealth fixedHealth)
    {
        try
        {
            using var context = _contextFactory.Create(companieName);

            foreach (var allergie in fixedHealth.Allergies)
            {
                context.Entry(allergie).State = EntityState.Unchanged;
            }

            await context.FixedHealths.AddAsync(fixedHealth);
            await context.SaveChangesAsync();
            return fixedHealth;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar uma Fixa de saúde. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar uma Fixa de saúde!");
        }
    }

    public async Task DisableAsync(string companieName, long fixedHealthId)
    {
        var fixedHealth = await GetByIdAsync(companieName, fixedHealthId);
        fixedHealth.Status = false;

        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.FixedHealths.Entry(fixedHealth).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desativar Fixa de saúde. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar uma Fixa de saúde!");
        }
    }

    public async Task<IEnumerable<FixedHealth>> GetAllAsync(string companieName)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.FixedHealths
                .AsNoTracking()
                .Where(u => u.Status.Equals(true))
                .Include(u => u.Allergies)
                .ToListAsync();
    }

    public async Task<FixedHealth> GetByIdAsync(string companieName, long fixedHealthId)
    {
        using var _context = _contextFactory.Create(companieName);

        var fixedHealth = await _context.FixedHealths
                .Where(a => a.Status.Equals(true) && a.StudentId.Equals(fixedHealthId))
                .Include(u => u.Allergies)
                .FirstOrDefaultAsync();

        if (fixedHealth is null)
        {
            throw new HttpResponseException(404, $"Fixa de saúde não encontrada com esse Id!");
        }

        return fixedHealth!;
    }

    public async Task<IEnumerable<FixedHealth>> SearchByStudentId(string companieName, long studentId)
    {
        using var _context = _contextFactory.Create(companieName);

        var fixedHealt = await _context.FixedHealths
                .Include(u => u.Allergies)
                .Where(u => u.StudentId.Equals(studentId) && u.Status.Equals(true))
                .ToListAsync();

        return fixedHealt;
    }

    public async Task UpdateAsync(string companieName, FixedHealth fixedHealthForUpdate)
    {
        try
        {
            using var context = _contextFactory.Create(companieName);


            var existingFixedHealth = await context.FixedHealths
            .Include(fh => fh.Allergies)
            .FirstOrDefaultAsync(fh => fh.StudentId == fixedHealthForUpdate.StudentId);

            if (existingFixedHealth == null)
                throw new HttpResponseException(404, "Fixa de saúde não encontrada.");

            // Atualiza os campos simples
            context.Entry(existingFixedHealth).CurrentValues.SetValues(fixedHealthForUpdate);

            // Atualiza as alergias (clear + add)
            existingFixedHealth.Allergies.Clear();
            foreach (var allergie in fixedHealthForUpdate.Allergies)
            {
                context.Attach(allergie); // Evita recriação da entidade
                existingFixedHealth.Allergies.Add(allergie);
            }

            await context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar uma Fixa de saúde. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar uma Fixa de saúde!");
        }
    }
}
