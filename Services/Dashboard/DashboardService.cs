using System.Security.Claims;
using myfreelas.Dtos.Dashboard;
using myfreelas.Repositories.Dashboard;
using myfreelas.Repositories.Freela;
using myfreelas.Repositories.Installment;

namespace myfreelas.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IFreelaRepository _freelaRepository;

    public DashboardService(IDashboardRepository dashboardRepository,
        IFreelaRepository freelaRepository,
        IInstallmentRepository installmentRepository)
    {
        _dashboardRepository = dashboardRepository;
        _freelaRepository = freelaRepository;
        _installmentRepository = installmentRepository;
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

    public async Task<ResponseTotalCustomersJson> TotalCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _dashboardRepository.TotalCustomers(userId);
        return new ResponseTotalCustomersJson
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

    public async Task<ResponseTotalPFCustomersJson> TotalPFCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customersPF = await _dashboardRepository.TotalPFCustomersAsync(userId);
        return new ResponseTotalPFCustomersJson
        {
            TotalPFCustomers = customersPF
        };
    }

    public async Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _dashboardRepository.TotalPJCustomersAsync(userId);
        return new ResponseTotalPJCustomersJson
        {
            TotalPJCustomers = customers
        };
    }

    public async Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync(ClaimsPrincipal logged)
    {
        var userId = GetCurrentUserId(logged);
        var customers = await _dashboardRepository.TotalRecurrentAsync(userId);
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
