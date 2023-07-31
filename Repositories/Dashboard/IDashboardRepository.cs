namespace myfreelas.Repositories.Dashboard;

public interface IDashboardRepository
{
    Task<int> TotalCustomers(int userId);
    Task<int> TotalRecurrentAsync(int userId);
    Task<int> TotalPFCustomersAsync(int userId);
    Task<int> TotalPJCustomersAsync(int userId);
}
