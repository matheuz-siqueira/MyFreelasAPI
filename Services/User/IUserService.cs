using myfreelas.Dtos.User;

namespace myfreelas.Services.User;

public interface IUserService
{
    Task<ResponseRegisterUserJson> RegisterUser(RequestRegisterUserJson request);   
    
}
