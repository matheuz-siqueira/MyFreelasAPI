using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Infra.Data.Context;

namespace MyFreelas.Infra.Data.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    private readonly AppDbContext _context;
    public InstallmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Installment>> GetInstallments(int freelaId)
    {
        return await _context.Installments.Where(i => i.FreelaId == freelaId)
            .ToListAsync();
    }

    public async Task<decimal> MonthlyBillingAsync(int year, int month)
    {
        return await _context.Installments
        .Where(i => i.Month.Year == year && i.Month.Month == month)
            .SumAsync(i => i.Value);
    }

    public async Task RemoveInstallmentsAsync(List<Installment> installments)
    {
        _context.Installments.RemoveRange(installments); 
        await _context.SaveChangesAsync();
    }
}
