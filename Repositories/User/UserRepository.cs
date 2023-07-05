using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myfreelas.Data;
using myfreelas.Dtos.User;

namespace myfreelas.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly Context _context;
    public UserRepository([FromServices] Context context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(Models.User user)
    {
        _context.Users?.AddAsync(user);
        await _context.SaveChangesAsync();  
    }

    public Models.User GetByEmail(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(user => user.Email == email);  
    }
}
