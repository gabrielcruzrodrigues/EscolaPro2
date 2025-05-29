namespace EscolaPro.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileInDatabaseAndReturnUrlAsync(IFormFile file, HttpRequest httpRequest);
        Task<string> UpdateESaveANewFileInDatabaseAndDeleteTheLastFileAndReturnTheNewUrlAsync(IFormFile file, HttpRequest httpRequest, string lastFilePath);
        string GetFileNameFromFullUrlPath(string fullUrlPath);
        bool DeleteFileFromFileName(string fileName);
    }
}
