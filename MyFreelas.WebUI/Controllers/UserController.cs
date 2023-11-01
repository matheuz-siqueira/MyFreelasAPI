using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFreelas.Application.Dtos.User;
using MyFreelas.Application.Interfaces;
using MyFreelas.Application.Exceptions.ErrorsValidators;
using MyFreelas.Application.Exceptions.BaseException;

namespace MyFreelas.WebUI.Controllers;

[Route("api/v{version:apiVersion}/users")]
public class UserController : MyFreelasController
{
    private readonly IUserService _service;
    private readonly IValidator<RequestRegisterUserJson> _validatorRegisterUser;
    private readonly IValidator<RequestUpdatePasswordJson> _validatorUpdatePassword;

    public UserController(
        IUserService service,
        IValidator<RequestRegisterUserJson> validatorRegisterUser,
        IValidator<RequestUpdatePasswordJson> validatorUpdatePassword)
    {
        _service = service;
        _validatorRegisterUser = validatorRegisterUser;
        _validatorUpdatePassword = validatorUpdatePassword;
    }

    /// <summary> 
    /// Registrar usuário no sistema
    /// </summary>
    /// <remarks>
    /// {"name":"name","lastName":"lastName","email":"valid@mail","password":"password","confirmPassword":"password"}
    /// </remarks> 
    /// <params name="request">Dados do usuário</params>
    /// <returns>Usuário recém cadastrado</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Erro na requisição</response>
    [AllowAnonymous]
    [HttpPost("create-account")]
    public async Task<ActionResult<ResponseAuthenticationJson>> PostUser(
        RequestRegisterUserJson request)
    {

        var result = _validatorRegisterUser.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            var response = await _service.RegisterUserAsync(request);
            return Ok(response);
        }
        catch (UserAlreadyExistsException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (DifferentPasswordsException e)
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
        var response = await _service.GetProfileAsync();
        return Ok(response);
    }


    /// <summary> 
    /// Atualizar senha do usuário logado
    /// </summary>  
    /// <remarks> 
    /// { "currentPassword":"currentPassword","newPassword":"newPassword" }
    /// </remarks> 
    /// <params name="request">Dados para alterar senha</params>
    /// <returns>Nada</returns>
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response> 

    [HttpPut("update-password")]
    public async Task<ActionResult> UpdatePassword(
        RequestUpdatePasswordJson request)
    {
        var result = _validatorUpdatePassword.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            await _service.UpdatePasswordAsync(request);
            return NoContent();
        }
        catch (IncorretPasswordException e)
        {
            return BadRequest(new { message = e.Message });
        }

    }
}
