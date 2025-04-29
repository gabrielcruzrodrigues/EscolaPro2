using EscolaPro.Database.Interfaces;
using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IDbContextFactory _contextFactory;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(IDbContextFactory contextFactory, ILogger<StudentRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<Student> GetbyIdAsync(string companieName, long studentId)
    {
        using var _context = _contextFactory.Create(companieName);

        var student = await _context.Students
                .Where(u => u.Status.Equals(true) && u.Id.Equals(studentId))
                .FirstOrDefaultAsync();

        return student;
    }

    public async Task<Student> CreateAsync(string companieName, Student student)
    {
        try 
        {
            using var _context = _contextFactory.Create(companieName);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar Estudante. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar criar um Estudante!");
        }
    }

    public async Task<IEnumerable<Student>> GetAllAsync(string companieName)
    {
        using var _context = _contextFactory.Create(companieName);
        return await _context.Students
            .AsNoTracking()
            .Where(s => s.Status.Equals(true))
            .ToListAsync();
    }

    public async Task<Student> GetByNameAsync(string companieName, string studentName)
    {
        using var _context = _contextFactory.Create(companieName);
        return await _context.Students
            .Where(s => s.Status.Equals(true) && s.Name.Equals(studentName))
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(string companieName, Student studentForUpdate)
    {
        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Students.Entry(studentForUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Estudante. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar atualizar Estudante!");
        }
    }

    public async Task DisableAsync(string companieName, int studentId)
    {
        var student = await GetbyIdAsync(companieName, studentId);
        if (student is null)
            throw new HttpResponseException(404, "Estudante não encontrado!");

        student.Status = false;

        try
        {
            using var _context = _contextFactory.Create(companieName);
            _context.Students.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro ao desativar Estudante. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar desativar Estudante!");
        }
    }

    public async Task<IEnumerable<Student>> SearchAsync(string companieName, string param)
    {
        try
        {
            using var _context = _contextFactory.Create(companieName);
            return await _context.Students
                .AsNoTracking()
                .Where(s => s.Status.Equals(true) && s.Name.Equals(param) || s.Email.Equals(param))
                .ToListAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar Estudante. {Message}", ex.InnerException?.Message ?? ex.Message);
            throw new HttpResponseException(500, "Um erro aconteceu ao tentar Buscar Estudante!");
        }
    }

    public async Task<Student> GetByEmailAsync(string companieName, string studentEmail)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Students
                .Where(u => u.Status.Equals(true) && u.Email.Equals(studentEmail))
                .FirstOrDefaultAsync();
    }

    public async Task<Student> GetByRgAsync(string companieName, string studentRg)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Students
                .Where(u => u.Status.Equals(true) && u.Rg.Equals(studentRg))
                .FirstOrDefaultAsync();
    }
    public async Task<Student> GetByCpfAsync(string companieName, string studentCpf)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Students
                .Where(u => u.Status.Equals(true) && u.Cpf.Equals(studentCpf))
                .FirstOrDefaultAsync();
    }

    public async Task<Student> GetByPhoneAsync(string companieName, string studentPhone)
    {
        using var _context = _contextFactory.Create(companieName);

        return await _context.Students
                .Where(u => u.Status.Equals(true) && u.Phone.Equals(studentPhone))
                .FirstOrDefaultAsync();
    }
}
