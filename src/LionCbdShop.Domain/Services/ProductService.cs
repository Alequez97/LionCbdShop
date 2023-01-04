using AutoMapper;
using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Products;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LionCbdShop.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductImagesRepository _productImagesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, IProductImagesRepository productImagesRepository, IMapper mapper, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _productImagesRepository = productImagesRepository;
        _mapper = mapper;
        _logger = logger;
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to get all products");
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Product);
        }

        return response;
    }

    public async Task<Response<ProductDto>> GetAsync(Guid id)
    {
        var response = new Response<ProductDto>();

        try
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null)
            {
                response.IsSuccess = false;
                response.Message = CommonResponseMessage.Get.NotFound(ResponseMessageEntity.ProductCategory, id);
                return response;
            }

            var productDto = _mapper.Map<ProductDto>(product);

            response.IsSuccess = true;
            response.ResponseObject = productDto;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to get product with id - {Id}", id);
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Product);
        }

        return response;
    }

    public async Task<Response<ProductCategoryDto>> GetProductCategoryAsync(string name)
    {
        var response = new Response<ProductCategoryDto>();

        try
        {
            var productCategory = await _productRepository.GetProductCategoryAsync(name);

            if (productCategory == null)
            {
                response.IsSuccess = false;
                response.Message = CommonResponseMessage.Get.NotFoundByName(ResponseMessageEntity.ProductCategory, name);
                return response;
            }

            var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);

            response.IsSuccess = true;
            response.ResponseObject = productCategoryDto;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to get product category with name - {Name}", name);
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

            var productCategory = await _productRepository.GetProductCategoryAsync(createProductRequest.ProductCategoryName);
            if (productCategory != null)
            {
                product.Category = productCategory;
            }

            await _productRepository.CreateAsync(product);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Product);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to create product");
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

            existingProduct = _mapper.Map(updateProductRequest, existingProduct);

            if (updateProductRequest.ProductImage != null)
            {
                await _productImagesRepository.DeleteAsync(existingProduct.ImageName);
                var newImageName = await _productImagesRepository.SaveAsync(updateProductRequest.ProductImage, CancellationToken.None);
                existingProduct.ImageName = newImageName;
            }

            await _productRepository.UpdateAsync(existingProduct);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Update.Success(ResponseMessageEntity.Product);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to update product with id - {Id}", updateProductRequest.Id);
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
        catch (DbUpdateException dbUpdateException)
        {
            _logger.LogWarning(dbUpdateException, "Exception: Unable to delete product with id - {Id}", id);
            response.IsSuccess = false;
            response.Message = ProductResponseMessage.CantDeleteBecauseOfExistingReference();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception in {action} action", "delete product");
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Delete.Error(ResponseMessageEntity.Product);
        }

        return response;
    }
}