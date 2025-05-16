using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EscolaPro.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageInDatabaseAndReturnUrlAsync(IFormFile file, HttpRequest httpRequest);
    }
}
