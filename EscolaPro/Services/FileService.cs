using EscolaPro.Services.Interfaces;

namespace EscolaPro.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(
            IWebHostEnvironment env
        )
        {
            _env = env;
        }

        public async Task<string> SaveFileInDatabaseAndReturnUrlAsync(IFormFile file, HttpRequest httpRequest)
        {
            var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath); // Garante que a pasta exista

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"{httpRequest.Scheme}://{httpRequest.Host}/uploads/{fileName}";
        }
    }
}
