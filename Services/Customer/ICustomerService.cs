using System.Security.Claims;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;
using myfreelas.Pagination;

namespace myfreelas.Services.Customer;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request, ClaimsPrincipal logged);
    Task<ResponseCustomerJson> GetByIdAsync(ClaimsPrincipal logged, string cHashId);

    Task<List<ResponseAllCustomerJson>> GetAllAsync(
        PaginationParameters customerParameters, 
        RequestGetCustomersJson request, ClaimsPrincipal logged);

    Task DeleteAsync(ClaimsPrincipal logged, string cHashId);   

    Task UpdateCustomerAsync(
        ClaimsPrincipal logged, RequestCustomerJson request, string cHashId);
}
