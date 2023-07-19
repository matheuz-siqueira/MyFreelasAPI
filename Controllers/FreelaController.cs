using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.Freela;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Services.Freela;

namespace myfreelas.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/freelas")]
[ApiConventionType(typeof(DefaultApiConventions))]

public class FreelaController : ControllerBase
{
    private readonly IFreelaService _service;
    private readonly IValidator<RequestRegisterFreelaJson> _validatorRegisterFreela;
    public FreelaController(IFreelaService service, 
        IValidator<RequestRegisterFreelaJson> validatorRegisterFreela)
    {
        _service = service; 
        _validatorRegisterFreela = validatorRegisterFreela; 
    }     

    /// <summary> 
    /// Registrar projeto freela no sistema
    /// </summary> 
    /// <remarks> 
    /// {"name":"string","description":"string","value":0,"startDate":"2023-07-17T03:47:45.391Z","finishDate":"2023-07-17T03:47:45.391Z","customerId":"string"}
    /// </remarks> 
    /// <params name="request">Dados do projetos</params> 
    /// <returns>Projeto cadastrado</returns> 
    /// <response code="201">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response> 

    [Authorize]
    [HttpPost("register-freela")]
    public async Task<ActionResult<ResponseFreelaJson>> RegisterFreelaAsync(
        [FromBody] RequestRegisterFreelaJson request)
    {
        var result = _validatorRegisterFreela.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            var response = await _service.RegisterFreelaAsync(User, request); 
            return StatusCode(201, response); 
        }
        catch(BadHttpRequestException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}
