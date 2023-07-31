using System.Security.Claims;
using AutoMapper;
using myfreelas.Dtos.Dashboard;
using myfreelas.Repositories.Customer;
using myfreelas.Repositories.Freela;
using myfreelas.Repositories.Installment;

namespace myfreelas.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IFreelaRepository _freelaRepository;

    public DashboardService(ICustomerRepository customerRepository,
        IFreelaRepository freelaRepository,
        IInstallmentRepository installmentRepository)
    {
        _customerRepository = customerRepository;
        _freelaRepository = freelaRepository;
        _installmentRepository = installmentRepository;
    }

    public async Task<ResponseMonthlyBillingJson> MonthlyBillingAsync(RequestGetMonthlyBillingJson request)
    {
        var date = request.Date;
        var result = await _installmentRepository.MonthlyBillingAsync(date);
        return new ResponseMonthlyBillingJson
        {
            MonthlyBilling = result
        };
    }

    public async Task<ResponseTotalCustomers> TotalCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _customerRepository.TotalCustomers(userId);
        return new ResponseTotalCustomers
        {
            TotalCustomers = customers
        };
    }

    public async Task<ResponseTotalFreelasJson> TotalFreelasAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var freelas = await _freelaRepository.TotalFreelasAsync(userId);
        return new ResponseTotalFreelasJson
        {
            TotalFreelas = freelas
        };
    }

    public async Task<ResponseTotalPFCustomers> TotalPFCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customersPF = await _customerRepository.TotalPFCustomersAsync(userId);
        return new ResponseTotalPFCustomers
        {
            TotalPFCustomers = customersPF
        };
    }

    public async Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _customerRepository.TotalPJCustomersAsync(userId);
        return new ResponseTotalPJCustomersJson
        {
            TotalPJCustomers = customers
        };
    }

    public async Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _customerRepository.TotalRecurrentAsync(userId);
        return new ResponseRecurrentCustomerJson
        {
            TotalRecurrent = customers
        };
    }

    private int GetCurrentUserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
