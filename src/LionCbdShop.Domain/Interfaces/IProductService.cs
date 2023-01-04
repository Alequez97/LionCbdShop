using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Products;

namespace LionCbdShop.Domain.Interfaces;

public interface IProductService
{
    Task<Response<ProductDto>> GetAsync(Guid id);

    Task<Response<IEnumerable<ProductDto>>> GetAllAsync();

    Task<Response<ProductCategoryDto>> GetProductCategoryAsync(string name);

    Task<Response> CreateAsync(CreateProductRequest request);

    Task<Response> UpdateAsync(UpdateProductRequest request);

    Task<Response> DeleteAsync(Guid id);
}