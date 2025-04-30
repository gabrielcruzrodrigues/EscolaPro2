using EscolaPro.Models.Educacional;

namespace EscolaPro.Repositories.Interfaces.Educacional;

public interface IFamilyRepository
{
    Task<Family> CreateAsync(string companieName, Family family);
    Task<IEnumerable<Family>> GetAllAsync(string companieName);
    Task<Family> GetByIdAsync(string companieName, long familyId);
    Task<Family> GetByNameAsync(string companieName, string familyName);
    Task<Family> GetByEmailAsync(string companieName, string familyEmail);
    Task<Family> GetByRgAsync(string companieName, string familyRg);
    Task<Family> GetByCpfAsync(string companieName, string familyCpf);
    Task<Family> GetByPhoneAsync(string companieName, string familyPhone);
    Task Update(string companieName, Family familyForUpdate);
    Task Disable(string companieName, long familyId);
    Task<IEnumerable<Family>> Search(string companieName, string param);
}
