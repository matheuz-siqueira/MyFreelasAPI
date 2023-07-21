using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.User;
using myfreelas.Exceptions.BaseException;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Services.User;

namespace myfreelas.Controllers;

[Route("api/v{version:apiVersion}/users")]

public class UserController : MyFreelasController
{
    private readonly IUserService _service;
    private readonly IValidator<RequestRegisterUserJson> _validatorRegisterUser; 
    private readonly IValidator<RequestUpdatePasswordJson> _validatorUpdatePassword; 

    public UserController(
        [FromServices] IUserService service, 
        [FromServices] IValidator<RequestRegisterUserJson> validatorRegisterUser,
        [FromServices] IValidator<RequestUpdatePasswordJson> validatorUpdatePassword)
    {
        _service = service; 
        _validatorRegisterUser = validatorRegisterUser; 
        _validatorUpdatePassword = validatorUpdatePassword;
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
    [AllowAnonymous]
    [HttpPost("create-account")]
    public async Task<ActionResult<ResponseAuthenticationJson>> PostUser(
        [FromBody] RequestRegisterUserJson request) 
    {
         
        var result = _validatorRegisterUser.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        } 
        try 
        {
            var response = await _service.RegisterUserAsync(request);
            return Ok(response);
        }
        catch(UserAlreadyExistsException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch(DifferentPasswordsException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    /// <summary> 
    /// Obter perfil do usuário logado
    /// </summary>
    /// <returns>Perfil</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="401">Não autenticado</response>
      
    [HttpGet("get-profile")]
    public async Task<ActionResult<ResponseProfileJson>> GetProfile()
    {
        var response = await _service.GetProfileAsync(User); 
        return Ok(response);
    }


    /// <summary> 
    /// Atualizar senha do usuário logado
    /// </summary>  
    /// <remarks> 
    /// { "currentPassword":"string","newPassword":"string" }
    /// </remarks> 
    /// <params name="request">Dados para alterar senha</params>
    /// <returns>Nada</returns>
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response> 
    
    [HttpPut("update-password")]
    public async Task<ActionResult> UpdatePassword(
        [FromBody] RequestUpdatePasswordJson request)
    {
        var result = _validatorUpdatePassword.Validate(request);  
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            await _service.UpdatePasswordAsync(request, User); 
            return NoContent();
        }
        catch(IncorretPasswordException e)
        {
            return BadRequest(new { message = e.Message });
        }

    }

}
