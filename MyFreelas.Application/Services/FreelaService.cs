using AutoMapper;
using HashidsNet;
using MyFreelas.Application.Exceptions.BaseException;
using MyFreelas.Application.Extension;
using MyFreelas.Application.Dtos.Freela ;
using MyFreelas.Application.Interfaces;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Application.Services;

public class FreelaService : IFreelaService
{
    private readonly IFreelaRepository _freelaRpository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IMapper _mapper;
    private readonly IHashidService _hashids;
    private readonly ILoggedService _logged;

    public FreelaService(IFreelaRepository freelaRepository,
        ICustomerRepository customerRepository,
        IInstallmentRepository installmentRepository,
        IMapper mapper,
        IHashidService hashids, 
        ILoggedService logged)
    {
        _freelaRpository = freelaRepository;
        _customerRepository = customerRepository;
        _installmentRepository = installmentRepository;
        _mapper = mapper;
        _hashids = hashids;
        _logged = logged;
    }
    public async Task<ResponseFreelaJson> GetByIdAsync(
        string fHashId)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(fHashId); 
        var freelaId = _hashids.Decode(fHashId);
        var freela = await _freelaRpository.GetByIdAsync(userId, freelaId);
        if (freela is null)
        {
            throw new ProjectNotFoundException("Projeto não encontrado");
        }
        return _mapper.Map<ResponseFreelaJson>(freela);
    }

    public async Task<List<ResponseAllFreelasJson>> GetAllAsync(
    RequestGetFreelaJson request, PaginationParameters paginationParameters)
    {
        var userId = _logged.GetCurrentUserId();
        var freelas = await _freelaRpository.GetAllAsync(userId, paginationParameters);
        var filters = Filter(request, freelas);
        return _mapper.Map<List<ResponseAllFreelasJson>>(filters);

    }

    public async Task<ResponseFreelaJson> RegisterFreelaAsync(
        RequestRegisterFreelaJson request)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(request.CustomerId);
        var customerId = _hashids.Decode(request.CustomerId); 
        var customer = await _customerRepository.GetByIdAsync(customerId, userId);
        if (customer is null)
        {
            throw new CustomerNotFoundException("Cliente não encontrado");
        }
        var freela = _mapper.Map<Domain.Entities.Freela>(request);
        freela.UserId = userId;
        var priceInstallment = freela.Price / freela.PaymentInstallment;
        var paymentMonth = freela.StartPayment;
        var finish = freela.PaymentInstallment;

        for (int i = 0; i < finish; i++)
        {
            var item = new Domain.Entities.Installment
            {
                FreelaId = freela.Id,
                Month = paymentMonth.AddMonths(i),
                Value = priceInstallment
            };
            freela.Installments.Add(item);
        }
        await _freelaRpository.RegisterFreelaAsync(freela);
        return _mapper.Map<ResponseFreelaJson>(freela);


    }
    
    public async Task UpdateAsync(string fHashId, RequestUpdateFreelaJson request)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(fHashId);
        var freelaId = _hashids.Decode(fHashId);
        var freela = await _freelaRpository.GetByIdUpdateAsync(userId, freelaId);
        if (freela is null)
        {
            throw new ProjectNotFoundException("Projeto não encontrado");
        }
        _mapper.Map(request, freela);

        var priceInstallment = freela.Price / freela.PaymentInstallment;
        var paymentMonth = freela.StartPayment;
        var finish = freela.PaymentInstallment;

        await _installmentRepository.GetInstallments(freelaId); 
        await _installmentRepository.RemoveInstallmentsAsync(freela.Installments);
        
        freela.Installments.Clear();
        for (int i = 0; i < finish; i++)
        {
            var item = new Domain.Entities.Installment
            {
                FreelaId = freela.Id,
                Month = paymentMonth.AddMonths(i),
                Value = priceInstallment
            };
            freela.Installments.Add(item);
        }

        await _freelaRpository.UpdateAsync();
    }

    public async Task DeleteAsync(string fHashId)
    {
        var userId = _logged.GetCurrentUserId();
        _hashids.IsHash(fHashId);
        var freelaId = _hashids.Decode(fHashId);
        var freela = await _freelaRpository.GetByIdAsync(userId, freelaId);
        if (freela is null)
        {
            throw new ProjectNotFoundException("Projeto não encontrado");
        }
        await _freelaRpository.DeleteAsync(freela);
    }
    private static List<Domain.Entities.Freela> Filter(RequestGetFreelaJson request, List<Domain.Entities.Freela> freelas)
    {
        var filters = freelas;
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            filters = freelas.Where(c => c.Name.CompareWithIgnoreCase(request.Name)).ToList();
        }
        return filters.OrderBy(c => c.Name).ToList();
    }
}
