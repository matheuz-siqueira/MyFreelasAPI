using System.Security.Claims;
using myfreelas.Dtos.Dashboard;

namespace myfreelas.Services.Dashboard;

public interface IDashboardService
{
    Task<ResponseTotalCustomersJson> TotalCustomersAsync(ClaimsPrincipal logged);
    Task<ResponseTotalFreelasJson> TotalFreelasAsync(ClaimsPrincipal logged);
    Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync(ClaimsPrincipal logged);
    Task<ResponseTotalPFCustomersJson> TotalPFCustomersAsync(ClaimsPrincipal logged);
    Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync(ClaimsPrincipal logged);
    Task<ResponseMonthlyBillingJson> MonthlyBillingAsync(RequestGetMonthlyBillingJson request);
}
