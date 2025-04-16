using EscolaPro.Models;
using EscolaPro.Models.Dtos;

namespace EscolaPro.Repositories.Interfaces;

public interface ICompanieRepository
{
    Task<Companies> CreateAsync(Companies user);
    Task<IEnumerable<Companies>> GetAllAsync();
    Task<Companies> GetByIdAsync(int companieId);
    Task<Companies> GetByNameAsync(string companieName);
    Task<Companies> GetByCnpjAsync(string companieName);
    Task Update(Companies userForUpdate);
    Task Disable(int companieId);
    Task<IEnumerable<Companies>> Search(string param);
}
