using EscolaPro.Models.Educacional;

namespace EscolaPro.Repositories.Interfaces.Educacional;

public interface IAllergieRepository
{
    Task<Allergy> CreateAsync(string companieName, Allergy allergie);
    Task<IEnumerable<Allergy>> GetAllAsync(string companieName);
    Task<Allergy> GetByIdAsync(string companieName, int allergieId);
    Task<Allergy> GetByNameAsync(string companieName, string allergieName);
    Task Update(string companieName, Allergy allergieForUpdate);
    Task Disable(string companieName, int allergieId);
    Task<IEnumerable<Allergy>> Search(string companieName, string param);
}
