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
    }
}
