using myfreelas.Dtos.Customer;
using myfreelas.Pagination;

namespace myfreelas.Repositories.Customer;

public interface ICustomerRepository
{
    Task<Models.Customer> RegistesrCustomerAsync(Models.Customer customer);
    Models.Customer GetByEmail(string email);
    Task<Models.Customer> GetByIdAsync(int id, int userId);
    Task<List<Models.Customer>> GetAllAsync(int userId,
        PaginationParameters customerParameters);
    Task DeleteAsync(int customerId);
    Task UpdateAsync();
    Task<Models.Customer> GetByIdUpdateAsync(int customerId, int userId);
    Task<int> TotalCustomers(int userId);
}
