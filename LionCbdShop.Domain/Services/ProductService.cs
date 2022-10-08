using AutoMapper;
using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Products;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;

namespace LionCbdShop.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductImagesRepository _productImagesRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IProductImagesRepository productImagesRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _productImagesRepository = productImagesRepository;
        _mapper = mapper;
    }

    public async Task<Response<ProductDto>> GetAsync(Guid id)
    {
        var response = new Response<ProductDto>();

        try
        {
            var product = await _productRepository.GetAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            response.IsSuccess = true;
            response.ResponseObject = productDto;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ProductMessages.Get.Error;
        }

        return response;
    }

    public async Task<Response<IEnumerable<ProductDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<ProductDto>>();

        try
        {
            var products = await _productRepository.GetAllAsync();

            var getProductsResponse = _mapper.Map<List<ProductDto>>(products);

            response.IsSuccess = true;
            response.ResponseObject = getProductsResponse;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ProductMessages.Get.Error;
        }

        return response;
    }

    public async Task<Response> CreateAsync(CreateProductRequest createProductRequest)
    {
        var response = new Response();

        try
        {
            var storedImageName = await _productImagesRepository.SaveAsync(createProductRequest.ProductImage, CancellationToken.None);

            var product = _mapper.Map<Product>(createProductRequest);
            product.ImageName = storedImageName;

            await _productRepository.CreateAsync(product);

            response.IsSuccess = true;
            response.Message = ProductMessages.Create.Success;
        }
        catch
        {
            response.IsSuccess = false;
            response.Message = ProductMessages.Create.Error;
        }

        return response;
    }

    public async Task<Response> UpdateAsync(UpdateProductRequest updateProductRequest)
    {
        var response = new Response();

        try
        {
            var existingProduct = await _productRepository.GetAsync(updateProductRequest.Id);

            if (existingProduct == null)
            {
                response.IsSuccess = false;
                response.Message = ProductMessages.Common.NotFound(updateProductRequest.Id);
                return response;
            }

            var product = _mapper.Map<Product>(updateProductRequest);

            var newImageName = string.Empty;
            if (updateProductRequest.ProductImage != null)
            {
                await _productImagesRepository.DeleteAsync(existingProduct.ImageName);
                newImageName = await _productImagesRepository.SaveAsync(updateProductRequest.ProductImage, CancellationToken.None);
            }

            product.ImageName = newImageName;
            await _productRepository.UpdateAsync(product);

            response.IsSuccess = true;
            response.Message = ProductMessages.Update.Success;
        }
        catch
        {
            response.IsSuccess = false;
            response.Message = ProductMessages.Update.Error;
        }

        return response;
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();

        try
        {
            var existingProduct = await _productRepository.GetAsync(id);

            if (existingProduct == null)
            {
                response.IsSuccess = false;
                response.Message = ProductMessages.Common.NotFound(id);
                return response;
            }

            await _productRepository.DeleteAsync(id);
            await _productImagesRepository.DeleteAsync(existingProduct.ImageName);

            response.IsSuccess = true;
            response.Message = ProductMessages.Delete.Success;
        }
        catch
        {
            response.IsSuccess = false;
            response.Message = ProductMessages.Delete.Error;
        }

        return response;
    }
}