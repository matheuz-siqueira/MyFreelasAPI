using MyFreelas.Application.Dtos.Dashboard;
using MyFreelas.Application.Interfaces;
using MyFreelas.Domain.Interfaces;

namespace MyFreelas.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IFreelaRepository _freelaRepository;
    private readonly ILoggedService _logged;
    public DashboardService(IDashboardRepository dashboardRepository,
        IFreelaRepository freelaRepository,
        IInstallmentRepository installmentRepository, 
        ILoggedService logged)
    {
        _dashboardRepository = dashboardRepository;
        _freelaRepository = freelaRepository;
        _installmentRepository = installmentRepository;
        _logged = logged;
    }

    public async Task<ResponseMonthlyBillingJson> MonthlyBillingAsync(RequestGetMonthlyBillingJson request)
    {
        var year = request.DateYear;
        var month = request.DateMonth;
        var result = await _installmentRepository.MonthlyBillingAsync(year, month);
        return new ResponseMonthlyBillingJson
        {
            MonthlyBilling = result
        };
    }

    public async Task<ResponseTotalCustomersJson> TotalCustomersAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var customers = await _dashboardRepository.TotalCustomers(userId);
        return new ResponseTotalCustomersJson
        {
            TotalCustomers = customers
        };
    }

    public async Task<ResponseTotalFreelasJson> TotalFreelasAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var freelas = await _freelaRepository.TotalFreelasAsync(userId);
        return new ResponseTotalFreelasJson
        {
            TotalFreelas = freelas
        };
    }

    public async Task<ResponseTotalPFCustomersJson> TotalPFCustomersAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var customersPF = await _dashboardRepository.TotalPFCustomersAsync(userId);
        return new ResponseTotalPFCustomersJson
        {
            TotalPFCustomers = customersPF
        };
    }

    public async Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var customers = await _dashboardRepository.TotalPJCustomersAsync(userId);
        return new ResponseTotalPJCustomersJson
        {
            TotalPJCustomers = customers
        };
    }

    public async Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var customers = await _dashboardRepository.TotalRecurrentAsync(userId);
        return new ResponseRecurrentCustomerJson
        {
            TotalRecurrent = customers
        };
    }
}
