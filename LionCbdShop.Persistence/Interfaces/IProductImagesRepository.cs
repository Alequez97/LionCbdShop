using Microsoft.AspNetCore.Http;

namespace LionCbdShop.Persistence.Interfaces;

public interface IProductImagesRepository
{
    public Task<string> SaveAsync(IFormFile image, CancellationToken cancellationToken);
    
    public Task DeleteAsync(string id);
}
