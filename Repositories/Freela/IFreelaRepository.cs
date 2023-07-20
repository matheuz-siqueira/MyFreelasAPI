namespace myfreelas.Repositories.Freela;

public interface IFreelaRepository
{
    Task<Models.Freela> RegisterFreelaAsync(Models.Freela freela); 

    Task<List<Models.Freela>> GetAllAsync(int userId);

    Task<Models.Freela> GetByIdAsync(int userId, int freelaId);
    Task DeleteAsync(Models.Freela freela); 

    Task <Models.Freela> GetByIdUpdateAsync(int userId, int freelaId);
    Task UpdateAsync(); 
}
