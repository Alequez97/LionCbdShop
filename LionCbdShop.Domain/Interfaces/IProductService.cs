using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests;

namespace LionCbdShop.Domain.Interfaces;

public interface IProductService
{
    Task<Response<ProductDto>> GetAsync(string id);

    Task<Response<IEnumerable<ProductDto>>> GetAllAsync();

    Task<Response> CreateAsync(CreateProductRequest request);

    Task<Response> UpdateAsync(UpdateProductRequest request);

    Task<Response> DeleteAsync(string id);
}