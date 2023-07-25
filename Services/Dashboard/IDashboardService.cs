using System.Security.Claims;
using myfreelas.Dtos.Dashboard;

namespace myfreelas.Services.Dashboard;

public interface IDashboardService
{
    Task<ResponseTotalCustomers> TotalCustomersAsync(ClaimsPrincipal logged);
    Task<ResponseTotalFreelasJson> TotalFreelasAsync(ClaimsPrincipal logged);
    Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync(ClaimsPrincipal logged);
    Task<ResponseTotalPFCustomers> TotalPFCustomersAsync(ClaimsPrincipal logged);
    Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync(ClaimsPrincipal logged);
}
