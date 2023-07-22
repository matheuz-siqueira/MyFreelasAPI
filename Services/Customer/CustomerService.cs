using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;
using myfreelas.Exceptions.BaseException;
using myfreelas.Extension;
using myfreelas.Pagination;
using myfreelas.Repositories.Customer;

namespace myfreelas.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IHashids _hashids;
    private readonly IMapper _mapper;
    public CustomerService(
        [FromServices] ICustomerRepository repository,
        [FromServices] IMapper mapper, 
        [FromServices] IHashids hashids)
    {
        _repository = repository; 
        _mapper = mapper; 
        _hashids = hashids; 
    }

    public async Task<List<ResponseAllCustomerJson>> GetAllAsync(
        CustomerParameters customerParameters,
        RequestGetCustomersJson request, ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged); 
        var customers = await _repository.GetAllAsync(userId, customerParameters);
        customers = Filter(request, customers);
        return _mapper.Map<List<ResponseAllCustomerJson>>(customers); 
    
    }

    public async Task<ResponseCustomerJson> GetByIdAsync(ClaimsPrincipal logged, string cHashId)
    {
        var userId = GetCurrentUserId(logged);
        IsHash(cHashId);
        var customerId = _hashids.DecodeSingle(cHashId); 
        var customer = await _repository.GetByIdAsync(customerId, userId); 
        if(customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        return _mapper.Map<ResponseCustomerJson>(customer); 
    }

    public async Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request, ClaimsPrincipal logged)
    {
        var exists = _repository.GetByEmail(request.Email);
        if(exists is not null)
        {
            throw new CustomerAlreadyExistsException("Cliente já cadastrado"); 
        }

        var customer = _mapper.Map<Models.Customer>(request); 
        customer.UserId = GetCurrentUserId(logged); 

        await _repository.RegistesrCustomerAsync(customer); 
        var response = _mapper.Map<ResponseRegisterCustomerJson>(customer);
        return response; 
    }

    public async Task UpdateCustomerAsync(
        ClaimsPrincipal logged, RequestCustomerJson request, string cHashId)
    {
        var userId = GetCurrentUserId(logged);
        IsHash(cHashId);
        var customerId = _hashids.DecodeSingle(cHashId); 
        var customer = await _repository.GetByIdUpdateAsync(customerId, userId); 
        if(customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        _mapper.Map(request, customer);
        await _repository.UpdateAsync();  
    }
    public async Task DeleteAsync(ClaimsPrincipal logged, string cHashId)
    {
        var userId = GetCurrentUserId(logged); 
        IsHash(cHashId);
        var customerId = _hashids.DecodeSingle(cHashId);
        var customer = await _repository.GetByIdAsync(customerId, userId); 
        if(customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado"); 
        }        
        await _repository.DeleteAsync(customerId); 
    }

    private int GetCurrentUserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }
    private void IsHash(string hashid)
    {
        var isHash = _hashids.TryDecodeSingle(hashid, out int id);
        if(!isHash)
        {
            throw new InvalidIDException("ID do cliente inválido");
        }
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
