using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Interfaces;

public interface ILoginService
{
    ResponseAuthenticationJson Login(RequestAuthenticationJson request);
}
