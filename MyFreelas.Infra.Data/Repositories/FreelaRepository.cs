using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Domain.Pagination;
using MyFreelas.Infra.Data.Context;


namespace MyFreelas.Infra.Data.Repositories;

public class FreelaRepository : IFreelaRepository
{
    private readonly AppDbContext _context;
    public FreelaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Freela freela)
    {
        _context.Remove(freela);
        await _context.SaveChangesAsync();

    }

    public async Task<List<Freela>> GetAllAsync(int userId, PaginationParameters paginationParameters)
    {
        return await _context.Freelas
            .AsNoTracking().Where(f => f.UserId == userId)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .ToListAsync();
    }

    public async Task<Freela> GetByIdAsync(int userId, int freelaId)
    {
        return await _context.Freelas
            .AsNoTracking()
                .Where(f => f.UserId == userId)
                .Include(f => f.Installments)
                .FirstOrDefaultAsync(f => f.Id == freelaId);
    }

    public async Task<Freela> GetByIdUpdateAsync(int userId, int freelaId)
    {
        return await _context.Freelas
            .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync(f => f.Id == freelaId);
    }

    public async Task<Freela> RegisterFreelaAsync(Freela freela)
    {
        await _context.Freelas.AddAsync(freela);
        await _context.SaveChangesAsync();
        return freela;
    }

    public async Task<int> TotalFreelasAsync(int userId)
    {
        return await _context.Freelas.AsNoTracking().CountAsync(f => f.UserId == userId);
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();

    }
}
