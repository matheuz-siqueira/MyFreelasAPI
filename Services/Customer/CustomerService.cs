using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;
using myfreelas.Extension;
using myfreelas.Repositories.Customer;

namespace myfreelas.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    public CustomerService(
        [FromServices] ICustomerRepository repository,
        [FromServices] IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper; 
    }

    public async Task<List<ResponseCustomerJson>> GetAllAsync(
        RequestGetCustomersJson request, ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged); 
        var customers = await _repository.GetAllAsync(userId);
        customers = Filter(request, customers);
        return _mapper.Map<List<ResponseCustomerJson>>(customers); 
    }

    public async Task<ResponseCustomerJson> GetByIdAsync(int id, ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customer = await _repository.GetByIdAsync(id, userId); 
        if(customer is null)
        {
            throw new BadHttpRequestException("Cliente não encontrado");
        }
        return _mapper.Map<ResponseCustomerJson>(customer); 
    }

    public async Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request, ClaimsPrincipal logged)
    {
        var exists = _repository.GetByEmail(request.Email);
        if(exists is not null)
        {
            throw new BadHttpRequestException("Cliente já cadastrado"); 
        }

        var customer = _mapper.Map<Models.Customer>(request); 
        customer.UserId = GetCurrentUserId(logged); 

        await _repository.RegistesrCustomerAsync(customer); 
        var response = _mapper.Map<ResponseRegisterCustomerJson>(customer);
        return response; 
    }

    public async Task UpdateCustomerAsync(
        RequestCustomerJson request, int customerId, ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customer = await _repository.GetByIdUpdateAsync(customerId, userId); 
        if(customer is null)
        {
            throw new BadHttpRequestException("Cliente não encontrado");
        }
        _mapper.Map(request, customer);
        await _repository.UpdateAsync();  
    }
    public async Task DeleteAsync(int customerId, ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged); 
        var customer = await _repository.GetByIdAsync(customerId, userId); 
        if(customer is null)
        {
            throw new BadHttpRequestException("Cliente não encontrado"); 
        }        
        await _repository.DeleteAsync(customerId); 
    }

    private int GetCurrentUserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }
    private static List<Models.Customer> Filter(RequestGetCustomersJson request, List<Models.Customer> customers)
    {
        var filters = customers;
        if(!string.IsNullOrWhiteSpace(request.Name))
        {
            filters = customers.Where(c => c.Name.CompareWithIgnoreCase(request.Name)).ToList();
        }
        return filters.OrderBy(c => c.Name).ToList();
    }
    
}
