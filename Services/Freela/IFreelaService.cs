using System.Security.Claims;
using HashidsNet;
using myfreelas.Dtos.Freela;
using myfreelas.Pagination;

namespace myfreelas.Services.Freela;

public interface IFreelaService
{
    Task<ResponseFreelaJson> RegisterFreelaAsync(ClaimsPrincipal logged,
        RequestRegisterFreelaJson request); 

    Task<List<ResponseAllFreelasJson>> GetAllAsync(ClaimsPrincipal logged, 
        RequestGetFreelaJson request, PaginationParameters paginationParameters); 

    Task<ResponseFreelaJson> GetByIdAsync(ClaimsPrincipal logged, string fHashId);  

    Task DeleteAsync(ClaimsPrincipal logged, string fHashId); 

    Task UpdateAsync(ClaimsPrincipal logged, string fHashId, 
        RequestUpdateFreelaJson request);
}
