using System.Security.Claims;
using AutoMapper;
using HashidsNet;
using myfreelas.Dtos.Freela;
using myfreelas.Repositories.Customer;
using myfreelas.Repositories.Freela;

namespace myfreelas.Services.Freela;

public class FreelaService : IFreelaService
{   
    private readonly IFreelaRepository _freelarRpository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;

    public FreelaService(IFreelaRepository freelaRepository, 
        ICustomerRepository customerRepository, 
        IMapper mapper, 
        IHashids hashids)
    {
        _freelarRpository = freelaRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _hashids = hashids;   
    }
    public async Task<ResponseFreelaJson> RegisterFreelaAsync(ClaimsPrincipal logged, 
        RequestRegisterFreelaJson request)
    {
        var userId = GetCurrentUserId(logged);
        var isHash = _hashids.TryDecodeSingle(request.CustomerId, out int number); 
        if(!isHash)
        {
            throw new BadHttpRequestException("ID de cliente inválido"); 
        }
        var customerId = _hashids.DecodeSingle(request.CustomerId);
        var customer = await _customerRepository.GetByIdAsync(customerId, userId);
        if(customer is null)
        {
            throw new BadHttpRequestException("Cliente não encontrado"); 
        }
        var freela = _mapper.Map<Models.Freela>(request);
        freela.UserId = userId; 
        await _freelarRpository.RegisterFreelaAsync(freela); 
        return _mapper.Map<ResponseFreelaJson>(freela); 
    }

    private int GetCurrentUserId(ClaimsPrincipal logged) 
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
