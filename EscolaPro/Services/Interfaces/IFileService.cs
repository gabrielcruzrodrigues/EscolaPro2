namespace EscolaPro.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileInDatabaseAndReturnUrlAsync(IFormFile file);
    }
}
