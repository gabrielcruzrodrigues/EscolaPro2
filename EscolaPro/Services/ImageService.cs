using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;

namespace EscolaPro.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _env;

    public ImageService(
        IWebHostEnvironment env
    )
    {
        _env = env;
    }

    public async Task<string> SaveImageInDatabaseAndReturnUrlAsync(IFormFile file)
    {
        var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");
        Directory.CreateDirectory(uploadsPath); // Garante que a pasta exista

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }
}
