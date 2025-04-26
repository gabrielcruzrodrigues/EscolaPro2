using EscolaPro.Models;

namespace EscolaPro.Repositories.Interfaces;

public interface IAllergieRepository
{
    Task<Allergie> CreateAsync(string companieName, Allergie allergie);
    Task<IEnumerable<Allergie>> GetAllAsync(string companieName);
    Task<Allergie> GetByIdAsync(string companieName, int allergieId);
    Task<Allergie> GetByNameAsync(string companieName, string allergieName);
    Task Update(string companieName, Allergie allergieForUpdate);
    Task Disable(string companieName, int allergieId);
    Task<IEnumerable<Allergie>> Search(string companieName, string param);
}
