namespace MyFreelas.Domain.Interfaces;

public interface IDashboardRepository
{
    Task<int> TotalCustomers(int userId);
    Task<int> TotalRecurrentAsync(int userId);
    Task<int> TotalPFCustomersAsync(int userId);
    Task<int> TotalPJCustomersAsync(int userId);
}
