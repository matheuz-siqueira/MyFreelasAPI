using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myfreelas.Dtos.User;
using myfreelas.Models;
using myfreelas.Repositories.User;

namespace myfreelas.Authentication;

public class LoginService : ILoginService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration; 
    private readonly IMapper _mapper;
    public LoginService(
        [FromServices] IUserRepository repository,
        [FromServices] IMapper mapper, 
        [FromServices] IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
    } 
    public ResponseAuthenticationJson Login(RequestAuthenticationJson request)
    {
        var user = _repository.GetByEmail(request.Email);
        if((user is null) || (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)))
        {
            throw new BadHttpRequestException("Usuário ou senha incorretos"); 
        }

        var tokenJWT = GenerateToken(user);
        var response = _mapper.Map<ResponseAuthenticationJson>(user);
        response.Token = tokenJWT; 
        return response;
    }   

    private string GenerateToken(User user) 
    {
        var JWTKey = Encoding.ASCII.GetBytes(_configuration["JWTKey"]);
        var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(JWTKey),
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();       
        claims.Add(new Claim(ClaimTypes.Name, user.Name));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); 

        var tokenJWT = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credenciais,
            claims: claims
        );  

        return new JwtSecurityTokenHandler().WriteToken(tokenJWT);
    }
}
