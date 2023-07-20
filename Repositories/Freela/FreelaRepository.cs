using Microsoft.EntityFrameworkCore;
using myfreelas.Data;

namespace myfreelas.Repositories.Freela;

public class FreelaRepository : IFreelaRepository
{
    private readonly Context _context;
    public FreelaRepository(Context context)
    {
        _context = context; 
    }

    public async Task<List<Models.Freela>> GetAllAsync(int userId)
    {
        return await _context.Freelas
            .AsNoTracking()
                .Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<Models.Freela> GetByIdAsync(int userId, int freelaId)
    {
        return await _context.Freelas
            .AsNoTracking()
                .Where(f => f.UserId == userId).FirstOrDefaultAsync(f => f.Id == freelaId);
    }

    public async Task<Models.Freela> RegisterFreelaAsync(Models.Freela freela)
    {
        await _context.AddAsync(freela); 
        await _context.SaveChangesAsync();
        return freela;
    }
}
