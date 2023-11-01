using AutoMapper;
using HashidsNet;
using MyFreelas.Application.Dtos.Customer;
using MyFreelas.Application.Exceptions.BaseException;
using MyFreelas.Application.Extension;
using MyFreelas.Application.Interfaces;
using MyFreelas.Domain.Entities.Enums;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IHashidService _hashids;
    private readonly IMapper _mapper;
    private readonly ILoggedService _logged;
    public CustomerService(
        ICustomerRepository repository,
        IMapper mapper,
        IHashidService hashids, 
        ILoggedService logged)
    {
        _repository = repository;
        _mapper = mapper;
        _hashids = hashids;
        _logged = logged; 
    }

    public async Task<List<ResponseAllCustomerJson>> GetAllAsync(
        PaginationParameters customerParameters,
        RequestGetCustomersJson request)
    {
        var userId = _logged.GetCurrentUserId();
        var customers = await _repository.GetAllAsync(userId, customerParameters);
        customers = Filter(request, customers);
        var response = _mapper.Map<List<ResponseAllCustomerJson>>(customers);
        return response;
    }

    public async Task<ResponseCustomerJson> GetByIdAsync(string cHashId)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(cHashId);
        var customerId = _hashids.Decode(cHashId);
        var customer = await _repository.GetByIdAsync(customerId, userId);
        if (customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        var response = _mapper.Map<ResponseCustomerJson>(customer);
        response.Type = TypeCustomer(customer);
        return response;
    }

    public async Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request)
    {
        var exists = _repository.GetByEmail(request.Email);
        if (exists is not null)
        {
            throw new CustomerAlreadyExistsException("Cliente já cadastrado");
        }

        var customer = _mapper.Map<Domain.Entities.Customer>(request);
        customer.UserId = _logged.GetCurrentUserId();

        await _repository.RegistesrCustomerAsync(customer);

        var response = _mapper.Map<ResponseRegisterCustomerJson>(customer);
        response.Type = TypeCustomer(customer);
        return response;
    }

    public async Task UpdateCustomerAsync(RequestCustomerJson request, string cHashId)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(cHashId);
        var customerId = _hashids.Decode(cHashId);
        var customer = await _repository.GetByIdUpdateAsync(customerId, userId);
        if (customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        _mapper.Map(request, customer);
        await _repository.UpdateAsync();
    }
    public async Task DeleteAsync(string cHashId)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(cHashId);
        var customerId = _hashids.Decode(cHashId);
        var customer = await _repository.GetByIdAsync(customerId, userId);
        if (customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        await _repository.RemoveAsync(customerId);
    }
    private static List<Domain.Entities.Customer> Filter(RequestGetCustomersJson request, List<Domain.Entities.Customer> customers)
    {
        var filters = customers;
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            filters = customers.Where(c => c.Name.CompareWithIgnoreCase(request.Name)).ToList();
        }
        return filters.OrderBy(c => c.Name).ToList();
    }

    private string TypeCustomer(Domain.Entities.Customer customer)
    {
        if (customer.Type == CustomerEnum.PJ)
        {
            return "PJ";
        }
        else
        {
            return "PF";
        }
    }
}
