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

    public Models.Customer GetByEmail(string email)
    {
        return _context.Customers.AsNoTracking().FirstOrDefault(c => c.Email == email);  
    }

    public async Task<Models.Customer> RegistesrCustomerAsync(Models.Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer; 
    }
}
