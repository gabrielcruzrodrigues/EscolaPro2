using EscolaPro.Models.Educacional;

namespace EscolaPro.Repositories.Interfaces.Educacional
{
    public interface IStudentRepository
    {
        Task<Student> GetbyIdAsync(string companieName, long studentId);
        Task<Student> CreateAsync(string companieName, Student student);
        Task<IEnumerable<Student>> GetAllAsync(string companieName);
        Task<Student> GetByNameAsync(string companieName, string studentName);
        Task UpdateAsync(string companieName, Student studentForUpdate);
        Task DisableAsync(string companieName, int studentId);
        Task<IEnumerable<Student>> SearchAsync(string companieName, string param);
        Task<Student> GetByEmailAsync(string companieName, string familyEmail);
        Task<Student> GetByRgAsync(string companieName, string familyRg);
        Task<Student> GetByCpfAsync(string companieName, string familyCpf);
        Task<Student> GetByPhoneAsync(string companieName, string familyPhone);
    }
}
