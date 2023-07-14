using System.Security.Claims;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;

namespace myfreelas.Services.Customer;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestCustomerJson request, ClaimsPrincipal logged);
    Task<ResponseCustomerJson> GetByIdAsync(int id, ClaimsPrincipal logged);

    Task<List<ResponseCustomerJson>> GetAllAsync(
        RequestGetCustomersJson request, ClaimsPrincipal logged);

    Task DeleteAsync(int customerId, ClaimsPrincipal logged);   

    Task UpdateCustomerAsync(
        RequestCustomerJson request, int customerId, ClaimsPrincipal logged);
}
