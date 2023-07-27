using Microsoft.EntityFrameworkCore;
using myfreelas.Data;
using myfreelas.Pagination;

namespace myfreelas.Repositories.Freela;

public class FreelaRepository : IFreelaRepository
{
    private readonly Context _context;
    public FreelaRepository(Context context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Models.Freela freela)
    {
        _context.Remove(freela);
        await _context.SaveChangesAsync();

    }

    public async Task<List<Models.Freela>> GetAllAsync(int userId, PaginationParameters paginationParameters)
    {
        return await _context.Freelas
            .AsNoTracking().Where(f => f.UserId == userId)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .ToListAsync();
    }

    public async Task<Models.Freela> GetByIdAsync(int userId, int freelaId)
    {
        return await _context.Freelas
            .AsNoTracking()
                .Where(f => f.UserId == userId)
                .Include(f => f.Contract)
                .FirstOrDefaultAsync(f => f.Id == freelaId);
    }

    public async Task<Models.Freela> GetByIdUpdateAsync(int userId, int freelaId)
    {
        return await _context.Freelas
            .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync(f => f.Id == freelaId);
    }

    public async Task<Models.Freela> RegisterFreelaAsync(Models.Freela freela)
    {
        await _context.AddAsync(freela);
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
