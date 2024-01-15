using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BoardGameNight.Configurations;
using Microsoft.Extensions.Options;

namespace BoardGameNight.Services;

public class LocalFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IWebHostEnvironment env, ILogger<LocalFileStorageService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subDirectory)
    {
        string wwwRootPath = _env.WebRootPath;
        string directoryPath = Path.Combine(wwwRootPath, subDirectory);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, file.FileName);

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path
            return $"/{subDirectory}/{file.FileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving file");
            throw;
        }
    }
}

