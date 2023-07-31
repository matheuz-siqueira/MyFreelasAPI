using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myfreelas.Data;
using myfreelas.Pagination;

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

    public async Task<List<Models.Customer>> GetAllAsync(int userId, PaginationParameters customerParameters)
    {
        return await _context.Customers
            .AsNoTracking().Where(c => c.UserId == userId)
            .Include(c => c.Freelas)
            .Skip((customerParameters.PageNumber - 1) * customerParameters.PageSize)
            .Take(customerParameters.PageSize)
            .ToListAsync();
    }

    public Models.Customer GetByEmail(string email)
    {
        return _context.Customers.AsNoTracking().FirstOrDefault(c => c.Email == email);
    }

    public async Task<Models.Customer> GetByIdAsync(int id, int userId)
    {
        return await _context.Customers
            .AsNoTracking().Where(c => c.UserId == userId).Include(c => c.Freelas)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Models.Customer> GetByIdUpdateAsync(int customerId, int userId)
    {
        return await _context.Customers.Where(c => c.UserId == userId)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }


    public async Task<Models.Customer> RegistesrCustomerAsync(Models.Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<int> TotalCustomers(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .CountAsync(c => c.UserId == userId);
    }

    public async Task<int> TotalPFCustomersAsync(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .Where(c => c.UserId == userId)
                .CountAsync(c => c.Type == Models.Enums.CustomerEnum.PF);
    }

    public async Task<int> TotalPJCustomersAsync(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .Where(c => c.UserId == userId).CountAsync(c => c.Type == Models.Enums.CustomerEnum.PJ);
    }

    public async Task<int> TotalRecurrentAsync(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .Where(c => c.UserId == userId).CountAsync(c => c.Freelas.Count > 1);
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
