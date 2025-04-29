using EscolaPro.Models;

namespace EscolaPro.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetbyIdAsync(string companieName, long studentId);
        Task<Student> CreateAsync(string companieName, Student student);
    }
}
