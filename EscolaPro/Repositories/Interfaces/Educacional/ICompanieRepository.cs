using EscolaPro.Models;
using EscolaPro.Models.Dtos;

namespace EscolaPro.Repositories.Interfaces.Educacional;

public interface ICompanieRepository
{
    Task<Company> CreateAsync(Company user);
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company> GetByIdAsync(int companieId);
    Task<Company> GetByNameAsync(string companieName);
    Task<Company> GetByCnpjAsync(string companieName);
    Task Update(Company userForUpdate);
    Task Disable(int companieId);
    Task<IEnumerable<Company>> Search(string param);
}
