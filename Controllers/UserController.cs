using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.User;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Services.User;

namespace myfreelas.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IValidator<RequestRegisterUserJson> _validator;

    public UserController(
        [FromServices] IUserService service, 
        [FromServices] IValidator<RequestRegisterUserJson> validator)
    {
        _service = service; 
        _validator = validator; 
    } 

    /// <summary> 
    /// Registrar usuário no sistema
    /// </summary>
    /// <remarks>
    /// {"name":"string","lastName":"string","email":"string","password":"string","confirmPassword":"string"}
    /// </remarks> 
    /// <params name="request">Dados do usuário</params>
    /// <returns>Usuário recém cadastrado</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Erro na requisição</response>
    [HttpPost("create-account")]
    public async Task<ActionResult<ResponseAuthenticationJson>> PostUser(
        [FromBody] RequestRegisterUserJson request) 
    {
         
        var result = _validator.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        } 
        try 
        {
            var response = await _service.RegisterUserAsync(request);
            return Ok(response);
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message} );
        }
    }

    /// <summary> 
    /// Obter perfil do usuário logado
    /// </summary>
    /// <returns>Perfil</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="401">Não autenticado</response>
    [Authorize]  
    [HttpGet("get-profile")]
    public async Task<ActionResult<ResponseProfileJson>> GetProfile()
    {
        var response = await _service.GetProfileAsync(User); 
        return Ok(response);
    }

}
