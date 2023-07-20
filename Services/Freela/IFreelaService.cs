using System.Security.Claims;
using HashidsNet;
using myfreelas.Dtos.Freela;

namespace myfreelas.Services.Freela;

public interface IFreelaService
{
    Task<ResponseFreelaJson> RegisterFreelaAsync(ClaimsPrincipal logged,
        RequestRegisterFreelaJson request); 

    Task<List<ResponseAllFreelasJson>> GetAllAsync(ClaimsPrincipal logged, 
        RequestGetFreelaJson request); 

    Task<ResponseFreelaJson> GetByIdAsync(ClaimsPrincipal logged, string fHashId);  

    Task DeleteAsync(ClaimsPrincipal logged, string fHashId); 
}
