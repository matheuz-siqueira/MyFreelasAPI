using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Infra.Data.Context;

namespace MyFreelas.Infra.Data.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;
    public DashboardRepository(AppDbContext context)
    {
        _context = context;
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
                .CountAsync(c => c.Type == Domain.Entities.Enums.CustomerEnum.PF);
    }

    public async Task<int> TotalPJCustomersAsync(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .Where(c => c.UserId == userId).CountAsync(c => c.Type == Domain.Entities.Enums.CustomerEnum.PJ);
    }

    public async Task<int> TotalRecurrentAsync(int userId)
    {
        return await _context.Customers.AsNoTracking()
            .Where(c => c.UserId == userId).CountAsync(c => c.Freelas.Count > 1);
    }
}
