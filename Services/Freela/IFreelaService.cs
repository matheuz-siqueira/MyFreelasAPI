using System.Security.Claims;
using myfreelas.Dtos.Freela;

namespace myfreelas.Services.Freela;

public interface IFreelaService
{
    Task<ResponseFreelaJson> RegisterFreelaAsync(ClaimsPrincipal logged,
        RequestRegisterFreelaJson request); 
}
