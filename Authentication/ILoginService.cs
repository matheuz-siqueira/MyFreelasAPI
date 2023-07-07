using myfreelas.Dtos.User;

namespace myfreelas.Authentication;

public interface ILoginService
{
    ResponseAuthenticationJson Login(RequestAuthenticationJson request);
}
