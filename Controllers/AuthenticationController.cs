using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Authentication;
using myfreelas.Dtos.User;
using myfreelas.Exceptions.ErrorsValidators;

namespace myfreelas.Controllers;

[Route("api/v{version:apiVersion}/authentication")]

public class AuthenticationController : MyFreelasController
{
    private readonly ILoginService _service;
    private readonly IValidator<RequestAuthenticationJson> _validator;
    public AuthenticationController(
        [FromServices] ILoginService service,
        [FromServices] IValidator<RequestAuthenticationJson> validator)
    {
        _service = service;
        _validator = validator;
    }

    /// <summary> 
    /// Logar no sistema
    /// </summary>
    /// <remarks> 
    /// { "email": "your@mail", "password": "your-password" }
    /// </remarks>
    /// <params name="request">Credencias de login</params>
    /// <returns>Token</returns>
    /// <response code="200">Sucesso</response> 
    /// <response code="400">Erro</response>

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<ResponseAuthenticationJson> Login
        ([FromBody] RequestAuthenticationJson request)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            var respone = _service.Login(request);
            return Ok(respone);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
