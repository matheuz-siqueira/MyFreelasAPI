using System.Security.Claims;
using AutoMapper;
using HashidsNet;
using myfreelas.Dtos.Freela;
using myfreelas.Extension;
using myfreelas.Repositories.Customer;
using myfreelas.Repositories.Freela;

namespace myfreelas.Services.Freela;

public class FreelaService : IFreelaService
{   
    private readonly IFreelaRepository _freelaRpository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;

    public FreelaService(IFreelaRepository freelaRepository, 
        ICustomerRepository customerRepository, 
        IMapper mapper, 
        IHashids hashids)
    {
        _freelaRpository = freelaRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _hashids = hashids;   
    }
    public async Task<ResponseFreelaJson> GetByIdAsync(
        ClaimsPrincipal logged, string fHashId)
    {
        var userId = GetCurrentUserId(logged); 
        var isHash = _hashids.TryDecodeSingle(fHashId, out int number);
        if (!isHash)
        {
            throw new BadHttpRequestException("ID do projeto inválido"); 
        }  
        var freelaId = _hashids.DecodeSingle(fHashId); 
        var freela = await _freelaRpository.GetByIdAsync(userId, freelaId); 
        if(freela is null)
        {
            throw new SystemException("Projeto não encontrado"); 
        }
        return _mapper.Map<ResponseFreelaJson>(freela); 
    }

    public async Task<List<ResponseAllFreelasJson>> GetAllAsync(ClaimsPrincipal logged, RequestGetFreelaJson request)
    {
        var userId = GetCurrentUserId(logged); 
        var freelas = await _freelaRpository.GetAllAsync(userId); 
        var filters = Filter(request, freelas); 
        return _mapper.Map<List<ResponseAllFreelasJson>>(filters); 

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
        await _freelaRpository.RegisterFreelaAsync(freela); 
        return _mapper.Map<ResponseFreelaJson>(freela); 
    }

    private int GetCurrentUserId(ClaimsPrincipal logged) 
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    private static List<Models.Freela> Filter(RequestGetFreelaJson request, List<Models.Freela> freelas)
    {
        var filters = freelas;
        if(!string.IsNullOrWhiteSpace(request.Name))
        {
            filters = freelas.Where(c => c.Name.CompareWithIgnoreCase(request.Name)).ToList();
        }
        return filters.OrderBy(c => c.Name).ToList();
    }
}
