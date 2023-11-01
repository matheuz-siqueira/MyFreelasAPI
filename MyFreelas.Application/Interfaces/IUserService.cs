using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Interfaces;

public interface IUserService
{
    Task<ResponseAuthenticationJson> RegisterUserAsync(RequestRegisterUserJson request);   
    Task<ResponseProfileJson> GetProfileAsync(); 
    Task UpdatePasswordAsync(RequestUpdatePasswordJson request); 
}
