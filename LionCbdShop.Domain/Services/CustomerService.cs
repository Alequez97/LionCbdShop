using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Customers;
using LionCbdShop.Persistence;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly LionCbdShopDbContext _dbContext;
    private readonly AutoMapper.IMapper _mapper;

    public CustomerService(ICustomerRepository profileRepository, LionCbdShopDbContext dbContext, AutoMapper.IMapper mapper)
    {
        _customerRepository = profileRepository;
        _dbContext = dbContext;
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
                response.Message = CustomerMessages.Create.CustomerExists;

                return response;
            }

            var customer = _mapper.Map<Customer>(request);
            var customerProvider = await _dbContext.CustomerProviders.FirstOrDefaultAsync(cp => cp.Name == request.CustomerProviderAsString);
            customer.CustomerProvider = customerProvider;

            await _customerRepository.CreateAsync(customer);

            response.IsSuccess = true;
            response.Message = CustomerMessages.Create.Success;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = CustomerMessages.Create.Error("logId");
        }

        return response;
    }
}
