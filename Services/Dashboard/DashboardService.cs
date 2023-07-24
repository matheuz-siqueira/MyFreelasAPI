using System.Security.Claims;
using AutoMapper;
using myfreelas.Dtos.Dashboard;
using myfreelas.Repositories.Customer;

namespace myfreelas.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DashboardService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
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

    private int GetCurrentUserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
