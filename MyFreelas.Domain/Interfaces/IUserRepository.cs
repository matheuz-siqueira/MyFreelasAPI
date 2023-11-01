using MyFreelas.Domain.Entities;

namespace MyFreelas.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
    User GetByEmail(string email);
    Task<User> GetProfileAsync(int id);
    Task UpdatePasswordAsync();
    Task<User> GetByIdAsync(int id);
}
