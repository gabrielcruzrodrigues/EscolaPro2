namespace EscolaPro.Services.Interfaces
{
    public interface IDatabaseService
    {
        void CreateDatabase(string connectionString);
        Task UpdateDatabase(string companyName);
    }
}
