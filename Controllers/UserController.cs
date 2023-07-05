using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.User;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Services.User;

namespace myfreelas.Controllers;

[ApiController]
[Route("api/users")]
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

    [HttpPost("create-account")]
    public async Task<ActionResult<ResponseRegisterUserJson>> PostUser(
        [FromBody] RequestRegisterUserJson request) 
    {
        var result = _validator.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToUserValidationFailure());
        }
        
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
