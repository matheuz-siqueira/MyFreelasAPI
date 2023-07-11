using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.Customer;
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
    public async Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(RequestRegisterCustomerJson request)
    {
        var exists = _repository.GetByEmail(request.Email);
        if(exists is not null)
        {
            throw new BadHttpRequestException("Cliente já cadastrado"); 
        }

        var customer = _mapper.Map<Models.Customer>(request); 
        await _repository.RegistesrCustomerAsync(customer); 
        var response = _mapper.Map<ResponseRegisterCustomerJson>(customer);
        return response; 
    }
}