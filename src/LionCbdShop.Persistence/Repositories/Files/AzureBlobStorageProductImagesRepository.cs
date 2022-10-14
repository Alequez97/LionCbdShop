using Azure.Storage.Blobs;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LionCbdShop.Persistence.Repositories.Files;

public class AzureBlobStorageProductImagesRepository : IProductImagesRepository
{
    private readonly BlobContainerClient _blobContainer;

    public AzureBlobStorageProductImagesRepository(IConfiguration configuration)
    {
        var connectionString = configuration["AzureStorage:StorageAccountConnectionString"];
        var blobContainerName = configuration["AzureStorage:ProductImagesContainerName"];
        
        var blobServiceClient = new BlobServiceClient(connectionString);
        _blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
        _blobContainer.CreateIfNotExists();
    }

    public async Task<string> SaveAsync(IFormFile image, CancellationToken cancellationToken = default)
    {
        var imageName = $"{Guid.NewGuid().ToString()}.png";
        var blobClient = _blobContainer.GetBlobClient(imageName);

        await using var imageStream = image.OpenReadStream();
        await blobClient.UploadAsync(imageStream, cancellationToken);

        return imageName;
    }

    public async Task DeleteAsync(string id)
    {
        await _blobContainer.DeleteBlobIfExistsAsync(id);
    }
}