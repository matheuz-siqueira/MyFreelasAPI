using myfreelas.Dtos.Customer;

namespace myfreelas.Repositories.Customer;

public interface ICustomerRepository
{
    Task<Models.Customer> RegistesrCustomerAsync(Models.Customer customer);
    Models.Customer GetByEmail(string email);

    Task<Models.Customer> GetByIdAsync(int id, int userId); 
}
