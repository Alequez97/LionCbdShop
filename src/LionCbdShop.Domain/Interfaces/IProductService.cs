using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Products;

namespace LionCbdShop.Domain.Interfaces;

public interface IProductService
{
    Task<Response<ProductDto>> GetAsync(Guid id);

    Task<Response<IEnumerable<ProductDto>>> GetAllAsync();
    
    Task<Response<ProductCategoryDto>> GetCategoryAsync(string name);
    
    Task<Response<IEnumerable<ProductCategoryDto>>> GetAllCategoriesAsync();

    Task<Response> CreateAsync(CreateProductRequest request);
    
    Task<Response> CreateCategoryAsync(CreateProductCategoryRequest request);

    Task<Response> UpdateAsync(UpdateProductRequest request);
    
    Task<Response> DeleteAsync(Guid id);
    
    Task<Response> DeleteCategoryAsync(string name);
}