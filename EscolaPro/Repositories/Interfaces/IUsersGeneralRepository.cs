using EscolaPro.Models;
using EscolaPro.Models.Dtos;
namespace EscolaPro.Repositories.Interfaces;

public interface IUsersGeneralRepository
{
    Task<UserGeneralDto> CreateAsync(UserGeneral user);
    Task<IEnumerable<UserGeneralDto>> GetAllUsersAsync();
    Task<UserGeneralDto> GetByIdAsync(long userId);
    Task<UserGeneral> GetByEmailAsync(string email);
    Task<UserGeneral> GetByNameAsync(string name);
    Task<UserGeneral> GetByIdWithTrackingAsync(long userId);
    Task Update(UserGeneral userForUpdate);
    Task Disable(long userId);
    Task<IEnumerable<UserGeneralDto>> Search(string param);
}
