using EscolaPro.Models;
using EscolaPro.Models.Dtos;

namespace EscolaPro.Repositories.Interfaces;

public interface ICompanieRepository
{
    Task<Companie> CreateAsync(Companie user);
    Task<IEnumerable<Companie>> GetAllAsync();
    Task<Companie> GetByIdAsync(int companieId);
    Task<Companie> GetByNameAsync(string companieName);
    Task<Companie> GetByCnpjAsync(string companieName);
    Task Update(Companie userForUpdate);
    Task Disable(int companieId);
    Task<IEnumerable<Companie>> Search(string param);
}
