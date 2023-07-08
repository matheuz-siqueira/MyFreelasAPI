using myfreelas.Dtos.User;

namespace myfreelas.Services.User;

public interface IUserService
{
    Task<ResponseAuthenticationJson> RegisterUserAsync(RequestRegisterUserJson request);   
    
}
