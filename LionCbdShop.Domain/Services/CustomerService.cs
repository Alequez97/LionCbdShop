using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Customers;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;

namespace LionCbdShop.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly AutoMapper.IMapper _mapper;

    public CustomerService(ICustomerRepository profileRepository, AutoMapper.IMapper mapper)
    {
        _customerRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateCustomerRequest request)
    {
        var response = new Response();

        try
        {
            var existingProfile = await _customerRepository.GetByUsernameAsync(request.Username);
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
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Create.Error(ResponseMessageEntity.Customer);
        }

        return response;
    }
}
