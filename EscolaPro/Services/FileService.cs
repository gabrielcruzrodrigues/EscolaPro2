using EscolaPro.Services.Interfaces;
using EscolaPro.Extensions;

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

        public async Task<string> UpdateESaveANewFileInDatabaseAndDeleteTheLastFileAndReturnTheNewUrlAsync(IFormFile file, HttpRequest httpRequest, string lastFilePath)
        {
            //string filename = GetFileNameFromFullUrlPath(lastFilePath);
            return  await SaveFileInDatabaseAndReturnUrlAsync(file, httpRequest);
        }

        public string GetFileNameFromFullUrlPath(string fullUrlPath)
        {
            string[] parts = fullUrlPath.Split("/uploads/");
            string fileName = "";

            if (parts.Length > 1)
            {
                fileName = parts[1];
            }

            return fileName;
        }

        public bool DeleteFileFromFileName(string fileName)
        {
            string filePath = Path.Combine(_env.ContentRootPath, "uploads", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
