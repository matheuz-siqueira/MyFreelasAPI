using MyFreelas.Domain.Entities;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Domain.Interfaces;

public interface IFreelaRepository
{
    Task<Freela> RegisterFreelaAsync(Freela freela);

    Task<List<Freela>> GetAllAsync(int userId, PaginationParameters paginationParameters);

    Task<Freela> GetByIdAsync(int userId, int freelaId);
    Task DeleteAsync(Freela freela);

    Task<Freela> GetByIdUpdateAsync(int userId, int freelaId);
    Task UpdateAsync();
    Task<int> TotalFreelasAsync(int userId);
}
