using EscolaPro.Models;

namespace EscolaPro.Repositories.Interfaces
{
    public interface ISaltRepository
    {
        Task Create(Salts salt);
        Task<Salts> GetByUserId(long userId);
        Task Update(long userId, string newSalt);
    }
}
