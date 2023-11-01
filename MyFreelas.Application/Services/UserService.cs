using AutoMapper;
using MyFreelas.Application.Exceptions.BaseException;
using MyFreelas.Application.Interfaces;
using MyFreelas.Application.Dtos.User;
using MyFreelas.Domain.Interfaces;

namespace MyFreelas.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedService _logged;
    private readonly ILoginService _service; 

    public UserService(
        IUserRepository repository,
        IMapper mapper,
        ILoggedService logged, 
        ILoginService service)
    {
        _repository = repository;
        _mapper = mapper;
        _logged = logged;
        _service = service;
    }

    public async Task<ResponseProfileJson> GetProfileAsync()
    {
        var userId = _logged.GetCurrentUserId();
        var user = await _repository.GetProfileAsync(userId); 
        return _mapper.Map<ResponseProfileJson>(user); 
    }

    public async Task<ResponseAuthenticationJson> RegisterUserAsync(
        RequestRegisterUserJson request)
    {
    
        var exists = _repository.GetByEmail(request.Email);
        if(exists is not null)
        {
            throw new UserAlreadyExistsException("Usuário já cadastrado"); 
        }

        if(request.Password != request.ConfirmPassword)
        {
            throw new DifferentPasswordsException("Senhas diferentes"); 
        }

        var user = _mapper.Map<Domain.Entities.User>(request); 
        var login = _mapper.Map<RequestAuthenticationJson>(user);
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
        await _repository.CreateUserAsync(user); 
        return _service.Login(login);

    }

    public async Task UpdatePasswordAsync(RequestUpdatePasswordJson request)
    {
        var id = _logged.GetCurrentUserId();
        var user = await _repository.GetByIdAsync(id); 
        if(!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
        {
            throw new IncorretPasswordException("Senha incorreta"); 
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _repository.UpdatePasswordAsync(); 
    }
}
