using System.Security.Claims;
using AutoMapper;
using myfreelas.Dtos.Dashboard;
using myfreelas.Repositories.Customer;
using myfreelas.Repositories.Freela;

namespace myfreelas.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IFreelaRepository _freelaRepository;

    public DashboardService(ICustomerRepository customerRepository,
        IFreelaRepository freelaRepository)
    {
        _customerRepository = customerRepository;
        _freelaRepository = freelaRepository;
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
