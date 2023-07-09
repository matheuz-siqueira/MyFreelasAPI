using System.Security.Claims;
using myfreelas.Dtos.User;

namespace myfreelas.Services.User;

public interface IUserService
{
    Task<ResponseAuthenticationJson> RegisterUserAsync(RequestRegisterUserJson request);   
    Task<ResponseProfileJson> GetProfileAsync(ClaimsPrincipal logged); 
    Task UpdatePasswordAsync(RequestUpdatePasswordJson request, ClaimsPrincipal logged); 
    
    
}
