using myfreelas.Dtos.User;

namespace myfreelas.Authentication;

public interface IAuthenticationService
{
    ResponseAuthenticationJson Login(RequestAuthenticationJson request);
}
