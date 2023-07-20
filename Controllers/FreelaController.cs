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
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]

public class FreelaController : ControllerBase
{
    private readonly IFreelaService _service;
    private readonly IValidator<RequestRegisterFreelaJson> _validatorRegisterFreela;
    private readonly IValidator<RequestUpdateFreelaJson> _validatorUpdateFreela;
    public FreelaController(IFreelaService service, 
        IValidator<RequestRegisterFreelaJson> validatorRegisterFreela,
        IValidator<RequestUpdateFreelaJson> validatorUpdateFreela)
    {
        _service = service; 
        _validatorRegisterFreela = validatorRegisterFreela;
        _validatorUpdateFreela = validatorUpdateFreela;  
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

    /// <summary> 
    /// Obter Lista de projetos cadastrados
    /// </summary> 
    /// <remarks> 
    /// { name: "string" }
    /// </remarks>
    /// <params name="request">Filtro de pesquisa</params> 
    /// <returns>Lista de projetos cadastrados</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="500">Erro interno</response> 
    [Authorize]
    [HttpPost("get-all")]
    public async Task<ActionResult<List<ResponseAllFreelasJson>>> GetAllAsync(
        [FromBody] RequestGetFreelaJson request) 
    {
        try 
        {
            var response = await _service.GetAllAsync(User, request); 
            return Ok(response); 
        }
        catch
        {
            return BadRequest("Erro na requisição"); 
        }
    }


    /// <summary> 
    /// Editar projeto cadastrado
    /// </summary> 
    /// <remarks> 
    /// {"name":"string","description":"string","value":0,"startDate":"2023-07-17T03:47:45.391Z","finishDate":"2023-07-17T03:47:45.391Z" }
    /// </remarks> 
    /// <params name="fHashId">ID do projeto</params> 
    /// <params name="request">Dados para atualizar</params> 
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro na requisição</response>
    /// <response code="401">Não autenticado</response>
    /// <response code="404">Não encontrado</response>

    [Authorize]
    [HttpPut("update-freela/{fHashId}")]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] string fHashId, [FromBody] RequestUpdateFreelaJson request)
    {
        var result = _validatorUpdateFreela.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            await _service.UpdateAsync(User, fHashId, request);
            return NoContent(); 
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch(Exception e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    /// <summary> 
    /// Obter projeto pelo ID
    /// </summary> 
    /// <params name="fHashId">ID do projeto</params> 
    /// <returns>Projeto com ID correspondente</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="400">Erro na requisição</response> 
    /// <response code="404">Não encontrado</response> 
    /// <response code="401">Não autenticado</response> 
    [Authorize]
    [HttpGet("get-by-id/{fHashId}")]
    public async Task<ActionResult<ResponseFreelaJson>> GetByIdAsync(
        [FromRoute] string fHashId)
    {
        try 
        {
            var response = await _service.GetByIdAsync(User, fHashId); 
            return Ok(response); 
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch(SystemException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    /// <summary> 
    /// Deletar projeto pelo ID
    /// </summary> 
    /// <params name="fHashId">ID do projeto</params> 
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro na requisição</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="404">Não encontrado</response>  
    [Authorize]
    [HttpDelete("delete-freela/{fHashId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string fHashId)
    {
        try 
        {
            await _service.DeleteAsync(User, fHashId); 
            return NoContent();
        }
        catch(BadHttpRequestException e)
        {
            return BadRequest(new { message = e.Message }); 
        }
        catch(Exception e)
        {
            return NotFound(new { message = e.Message }); 
        } 
    }
}
