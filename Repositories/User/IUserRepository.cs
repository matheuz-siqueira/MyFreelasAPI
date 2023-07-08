
namespace myfreelas.Repositories.User;

public interface IUserRepository
{
    Task CreateUserAsync(Models.User user); 
    Models.User GetByEmail(string email); 
    Task<Models.User> GetProfileAsync(int id); 
}
