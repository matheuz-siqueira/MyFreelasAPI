using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myfreelas.Data;
using myfreelas.Dtos.Customer;

namespace myfreelas.Repositories.Customer;

public class CustomerRepository : ICustomerRepository
{
    private readonly Context _context;
    public CustomerRepository([FromServices] Context context)
    {
        _context = context;
    }

    public async Task DeleteAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId); 
        _context.Remove(customer); 
        await _context.SaveChangesAsync();    
    }

    public async Task<List<Models.Customer>> GetAllAsync(int userId)
    {
        return await _context.Customers
            .AsNoTracking()
                .Where(c => c.UserId == userId).ToListAsync(); 
    }

    public Models.Customer GetByEmail(string email)
    {
        return _context.Customers.AsNoTracking().FirstOrDefault(c => c.Email == email);  
    }

    public async Task<Models.Customer> GetByIdAsync(int id, int userId)
    {   
        return await _context.Customers
            .AsNoTracking()
                .Where(c => c.UserId == userId)
                    .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Models.Customer> RegistesrCustomerAsync(Models.Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer; 
    }
}
