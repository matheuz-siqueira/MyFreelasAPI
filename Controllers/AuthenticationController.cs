using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Authentication;
using myfreelas.Dtos.User;

namespace myfreelas.Controllers;

[ApiController]
[Route("api/authentication")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly ILoginService _service;
    private readonly IValidator<RequestAuthenticationJson> _validator;
    public AuthenticationController([FromServices] ILoginService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public ActionResult<ResponseAuthenticationJson> Login
        ([FromBody] RequestAuthenticationJson request)
    {
        try
        {
            var respone =  _service.Login(request); 
            return Ok(respone);
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
