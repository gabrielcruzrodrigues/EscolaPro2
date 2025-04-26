using EscolaPro.Models;

namespace EscolaPro.Repositories.Interfaces;

public interface IFixedHealthRepository
{
    Task<FixedHealth> CreateAsync(string companieName, FixedHealth fixedHealth);
    Task<IEnumerable<FixedHealth>> GetAllAsync(string companieName);
    Task<FixedHealth> GetByIdAsync(string companieName, long fixedHealthId);
    Task UpdateAsync(string companieName, FixedHealth fixedHealthForUpdate);
    Task DisableAsync(string companieName, long fixedHealthId);
    Task<IEnumerable<FixedHealth>> SearchByStudentId(string companieName, long studentId);
}
