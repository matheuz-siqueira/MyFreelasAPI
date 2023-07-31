using Microsoft.EntityFrameworkCore;
using myfreelas.Data;

namespace myfreelas.Repositories.Dashboard;

public class DashboardRepository : IDashboardRepository
{
    private readonly Context _context;
    public DashboardRepository(Context context)
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
}
