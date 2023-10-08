using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BoardGameNight.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<BlobStorageService> _logger;

    public BlobStorageService(string connectionString, ILogger<BlobStorageService> logger)
    {
        _logger = logger;

        try
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error connecting to blob storage with connection string: {connectionString}");
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