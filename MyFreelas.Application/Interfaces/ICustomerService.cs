using MyFreelas.Application.Dtos.Customer;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Application.Interfaces;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request);
    Task<ResponseCustomerJson> GetByIdAsync(string cHashId);

    Task<List<ResponseAllCustomerJson>> GetAllAsync(
        PaginationParameters customerParameters, 
        RequestGetCustomersJson request);

    Task DeleteAsync(string cHashId);   

    Task UpdateCustomerAsync(RequestCustomerJson request, string cHashId);
}
