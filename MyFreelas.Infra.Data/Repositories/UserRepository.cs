using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Infra.Data.Context;

namespace MyFreelas.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
     private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users?.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public User GetByEmail(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(user => user.Email == email);
    }

    public Task<User> GetByIdAsync(int id)
    {
        return _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> GetProfileAsync(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task UpdatePasswordAsync()
    {
        await _context.SaveChangesAsync();
    }
}
