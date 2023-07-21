using System.Security.Claims;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;

namespace myfreelas.Services.Customer;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request, ClaimsPrincipal logged);
    Task<ResponseCustomerJson> GetByIdAsync(ClaimsPrincipal logged, string cHashId);

    Task<List<ResponseCustomerJson>> GetAllAsync(
        RequestGetCustomersJson request, ClaimsPrincipal logged);

    Task DeleteAsync(ClaimsPrincipal logged, string cHashId);   

    Task UpdateCustomerAsync(
        ClaimsPrincipal logged, RequestCustomerJson request, string cHashId);
}
