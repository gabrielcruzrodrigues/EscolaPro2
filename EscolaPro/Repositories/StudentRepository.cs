using EscolaPro.Database.Interfaces;
using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EscolaPro.Repositories
{
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
    }
}
