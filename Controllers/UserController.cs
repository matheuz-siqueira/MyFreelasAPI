using FluentValidation;
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
    public async Task<ActionResult<ResponseRegisterUserJson>> PostUser(
        [FromBody] RequestRegisterUserJson request) 
    {
        // var result = _validator.Validate(request);
        // if(!result.IsValid)
        // {
        //     return BadRequest(result.Errors.ToUserValidationFailure());
        // }
        
        try 
        {
            var response = await _service.RegisterUser(request);
            return StatusCode(201, response);
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message} );
        }
    }
}
