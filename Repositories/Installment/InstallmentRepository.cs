using myfreelas.Data;
using Microsoft.EntityFrameworkCore;
namespace myfreelas.Repositories.Installment;

public class InstallmentRepository : IInstallmentRepository
{
    private readonly Context _context;
    public InstallmentRepository(Context context)
    {
        _context = context;
    }

    public async Task<decimal> MonthlyBillingAsync(int year, int month)
    {
        return await _context.Installments
        .Where(i => i.Month.Year == year && i.Month.Month == month)
            .SumAsync(i => i.Value);
    }
}
