using myfreelas.Dtos.Customer;

namespace myfreelas.Services.Customer;

public interface ICustomerService
{
    Task<ResponseRegisterCustomerJson> RegisterCustomerAsync(RequestRegisterCustomerJson request);
}