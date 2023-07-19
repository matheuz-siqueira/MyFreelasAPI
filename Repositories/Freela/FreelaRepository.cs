using myfreelas.Data;

namespace myfreelas.Repositories.Freela;

public class FreelaRepository : IFreelaRepository
{
    private readonly Context _context;
    public FreelaRepository(Context context)
    {
        _context = context; 
    }
    public async Task<Models.Freela> RegisterFreelaAsync(Models.Freela freela)
    {
        await _context.AddAsync(freela); 
        await _context.SaveChangesAsync();
        return freela;
    }
}
