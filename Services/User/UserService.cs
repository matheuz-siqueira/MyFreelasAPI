using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.User;
using myfreelas.Repositories.User;

namespace myfreelas.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    

    public UserService(
        [FromServices] IUserRepository repository,
        [FromServices] IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterUserJson> RegisterUser(
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
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
        await _repository.CreateUserAsync(user); 
        var response = _mapper.Map<ResponseRegisterUserJson>(user); 
        return response; 
    }
}
