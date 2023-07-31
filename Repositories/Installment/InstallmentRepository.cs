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

    public async Task<decimal> MonthlyBillingAsync(DateTime date)
    {
        return await _context.Installments.Where(i => i.Month.Year == date.Year && i.Month.Month == date.Month)
            .SumAsync(i => i.Value);
    }
}
