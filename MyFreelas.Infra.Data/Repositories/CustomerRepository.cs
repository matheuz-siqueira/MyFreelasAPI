using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Domain.Pagination;
using MyFreelas.Infra.Data.Context;

namespace MyFreelas.Infra.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RemoveAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        _context.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Customer>> GetAllAsync(int userId, PaginationParameters customerParameters)
    {
        return await _context.Customers
            .AsNoTracking().Where(c => c.UserId == userId)
            .Include(c => c.Freelas)
            .Skip((customerParameters.PageNumber - 1) * customerParameters.PageSize)
            .Take(customerParameters.PageSize)
            .ToListAsync();
    }

    public Customer GetByEmail(string email)
    {
        return _context.Customers.AsNoTracking().FirstOrDefault(c => c.Email == email);
    }

    public async Task<Customer> GetByIdAsync(int id, int userId)
    {
        return await _context.Customers
            .AsNoTracking().Where(c => c.UserId == userId).Include(c => c.Freelas)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer> GetByIdUpdateAsync(int customerId, int userId)
    {
        return await _context.Customers.Where(c => c.UserId == userId)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }


    public async Task<Customer> RegistesrCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
