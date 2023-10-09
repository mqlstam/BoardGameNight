using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BoardGameNight.Configurations;
using Microsoft.Extensions.Options;

namespace BoardGameNight.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<BlobStorageService> _logger;

    public BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettings, ILogger<BlobStorageService> logger)
    {
        _logger = logger;

        try
        {
            _blobServiceClient = new BlobServiceClient(blobStorageSettings.Value.ConnectionString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error connecting to blob storage with connection string: {blobStorageSettings.Value.ConnectionString}");
            throw;
        }
    }
    public async Task<string> UploadImage(IFormFile image, string containerName)
    {
        if (image == null || image.Length == 0)
        {
            _logger.LogError("Uploaded file is null or empty.");
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

        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (containerClient == null)
            {
                _logger.LogError($"Container client could not be created for container: {containerName}");
                throw new Exception($"Container client could not be created for container: {containerName}");
            }

            var blobClient = containerClient.GetBlobClient(image.FileName);

            using (var stream = image.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = image.ContentType });
            }

            return blobClient.Uri.AbsoluteUri;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error uploading image to blob storage for container: {containerName}");
            throw;
        }
    }
}