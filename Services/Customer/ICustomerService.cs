using System.Security.Claims;
using myfreelas.Dtos.Customer;

namespace myfreelas.Services.Customer;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(
        RequestRegisterCustomerJson request, ClaimsPrincipal logged);

    Task<ResponseCustomerJson> GetByIdAsync(int id, ClaimsPrincipal logged); 
}
