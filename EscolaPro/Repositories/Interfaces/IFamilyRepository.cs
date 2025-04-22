using EscolaPro.Models;

namespace EscolaPro.Repositories.Interfaces;

public interface IFamilyRepository
{
    Task<Family> CreateAsync(string companieName, Family family);
    Task<IEnumerable<Family>> GetAllAsync(string companieName);
    Task<Family> GetByIdAsync(string companieName, long familyId);
    Task<Family> GetByNameAsync(string companieName, string familyName);
    Task Update(string companieName, Family familyForUpdate);
    Task Disable(string companieName, long familyId);
    Task<IEnumerable<Family>> Search(string companieName, string param);
}
