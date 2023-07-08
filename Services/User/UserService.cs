using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Authentication;
using myfreelas.Dtos.User;
using myfreelas.Repositories.User;

namespace myfreelas.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoginService _service;

    public UserService(
        [FromServices] IUserRepository repository,
        [FromServices] IMapper mapper,
        [FromServices] ILoginService service)
    {
        _repository = repository;
        _mapper = mapper;
        _service = service;
    }

    public async Task<ResponseProfileJson> GetProfileAsync(ClaimsPrincipal logged)
    {
        var userId = int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier)); 
        var user = await _repository.GetProfileAsync(userId); 
        return _mapper.Map<ResponseProfileJson>(user); 
    }

    public async Task<ResponseAuthenticationJson> RegisterUserAsync(
        RequestRegisterUserJson request)
    {
    
        var exists = _repository.GetByEmail(request.Email);
        if(exists is not null)
        {
            throw new BadHttpRequestException("Usuário já cadastrado"); 
        }

        if(request.Password != request.ConfirmPassword)
        {
            throw new BadHttpRequestException("Senhas diferentes"); 
        }

        var user = _mapper.Map<Models.User>(request); 
        var login = _mapper.Map<RequestAuthenticationJson>(user);
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
        await _repository.CreateUserAsync(user); 
        return _service.Login(login);

    }
}
