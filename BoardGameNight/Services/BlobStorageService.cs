// BoardGameNight/Services/BlobStorageService.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BoardGameNight.Services;

public class BlobStorageService
{
    private readonly string _storagePath;
    private readonly ILogger<BlobStorageService> _logger;

    public BlobStorageService(IWebHostEnvironment env, ILogger<BlobStorageService> logger)
    {
        _logger = logger;
        _storagePath = Path.Combine(env.WebRootPath, "images"); // Folder for images in wwwroot

        // Ensure the storage directory exists
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("File cannot be null or empty.", nameof(image));
        }


        // image content type validation
        string[] permittedTypes = { "image/jpeg", "image/png", "image/gif" };
        if (!permittedTypes.Contains(image.ContentType))
        {
            _logger.LogError("Invalid image content type.");
            throw new ArgumentException("Invalid image content type.", nameof(image.ContentType));
        }

        // image size validation (limit to 2MB in this example)
        const int maxSize = 2 * 1024 * 1024;
        if (image.Length > maxSize)
        {
            _logger.LogError("Image size is too large.");
            throw new ArgumentException("Image size is too large.", nameof(image.Length));
        }

        string filePath = Path.Combine(_storagePath, image.FileName);
        
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Return a relative URL to the file
            return $"/images/{image.FileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading image to local storage");
            throw;
        }
    }
}