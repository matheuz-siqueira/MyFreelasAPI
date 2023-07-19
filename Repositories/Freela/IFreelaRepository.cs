namespace myfreelas.Repositories.Freela;

public interface IFreelaRepository
{
    Task<Models.Freela> RegisterFreelaAsync(Models.Freela freela); 
}
