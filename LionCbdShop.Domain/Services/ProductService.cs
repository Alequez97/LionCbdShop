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
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Product);
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
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Product);
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
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Product);
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Create.Error(ResponseMessageEntity.Product);
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
                response.Message = CommonResponseMessage.Get.NotFound(ResponseMessageEntity.Product, updateProductRequest.Id);
                return response;
            }

            var product = _mapper.Map<Product>(updateProductRequest);

            if (updateProductRequest.ProductImage != null)
            {
                await _productImagesRepository.DeleteAsync(existingProduct.ImageName);
                var newImageName = await _productImagesRepository.SaveAsync(updateProductRequest.ProductImage, CancellationToken.None);
                product.ImageName = newImageName;
            }
            
            await _productRepository.UpdateAsync(product);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Update.Success(ResponseMessageEntity.Product);
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Update.Error(ResponseMessageEntity.Product);
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
                response.Message = CommonResponseMessage.Get.NotFound(ResponseMessageEntity.Product, id);
                return response;
            }

            await _productRepository.DeleteAsync(id);
            await _productImagesRepository.DeleteAsync(existingProduct.ImageName);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Delete.Success(ResponseMessageEntity.Product);
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Delete.Error(ResponseMessageEntity.Product);
        }

        return response;
    }
}