using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.Customer;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Services.Customer;

namespace myfreelas.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/customers")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]

public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;
    private readonly IValidator<RequestRegisterCustomerJson> _validatorRegisterCustomer;
    public CustomerController(
        [FromServices] ICustomerService service, 
        [FromServices] IValidator<RequestRegisterCustomerJson> validatorRegisterCustomer)
    {
        _service = service; 
        _validatorRegisterCustomer = validatorRegisterCustomer; 
    }

    /// <summary> 
    /// Registra cliente 
    /// </summary>
    /// <remarks>
    /// {"name":"string","type":0,"email":"string","phoneNumber":"string","otherContact":"string"}
    /// </remarks>
    /// <params name="request">Dados do cliente</params> 
    /// <returns>Cliente cadastrado</returns>
    /// <response code="201">Sucesso</response> 
    /// <response code="400">Erro</response> 
    [Authorize]
    [HttpPost("register-customer")]
    public async Task<ActionResult<ResponseRegisterCustomerJson>> RegisterCustomerAsync(
        [FromBody] RequestRegisterCustomerJson request)
    {
        var result = _validatorRegisterCustomer.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            var response = await _service.RegisterCustomerAsync(request, User); 
            return StatusCode(201, response); 
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new {message = e.Message} );
        }
    }
}
