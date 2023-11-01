using MyFreelas.Application.Dtos.Dashboard;

namespace MyFreelas.Application.Interfaces;

public interface IDashboardService
{
    Task<ResponseTotalCustomersJson> TotalCustomersAsync();
    Task<ResponseTotalFreelasJson> TotalFreelasAsync();
    Task<ResponseRecurrentCustomerJson> TotalRecurrentAsync();
    Task<ResponseTotalPFCustomersJson> TotalPFCustomersAsync();
    Task<ResponseTotalPJCustomersJson> TotalPJCustomersAsync();
    Task<ResponseMonthlyBillingJson> MonthlyBillingAsync(RequestGetMonthlyBillingJson request);
}
