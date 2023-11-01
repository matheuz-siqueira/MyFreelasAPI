using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> RegistesrCustomerAsync(Customer customer);
    Customer GetByEmail(string email);
    Task<Customer> GetByIdAsync(int id, int userId);
    Task<List<Customer>> GetAllAsync(int userId, PaginationParameters customerParameters);
    Task RemoveAsync(int customerId);
    Task UpdateAsync();
    Task<Customer> GetByIdUpdateAsync(int customerId, int userId);
}
