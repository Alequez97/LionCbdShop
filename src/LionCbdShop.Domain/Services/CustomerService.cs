using AutoMapper;
using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Customers;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace LionCbdShop.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerRepository profileRepository, IMapper mapper, ILogger<CustomerService> logger)
    {
        _customerRepository = profileRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response> CreateAsync(CreateCustomerRequest request)
    {
        var response = new Response();

        try
        {
            var existingProfile = await _customerRepository.GetByIdInCustomerProviderSystemAsync(request.IdInCustomerProviderSystem);
            if (existingProfile != null)
            {
                response.IsSuccess = true;
                response.Message = CustomerResponseMessage.Exists(request.Username);

                return response;
            }

            var customer = _mapper.Map<Customer>(request);

            await _customerRepository.CreateAsync(customer);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Customer);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception during customer creation");
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Create.Error(ResponseMessageEntity.Customer);
        }

        return response;
    }
}
