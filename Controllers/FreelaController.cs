using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.Freela;
using myfreelas.Exceptions.BaseException;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Pagination;
using myfreelas.Services.Freela;
namespace myfreelas.Controllers;

[Route("api/v{version:apiVersion}/freelas")]

public class FreelaController : MyFreelasController
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
    /// {"name":"name project","description":"description","price":0,"startDate":"yyyy-MM-dd","finishDate":"yyyy-MM-dd","startPayment":"yyyy-MM-dd","paymentInstallment":0,"customerId":"HashId"}
    /// </remarks> 
    /// <params name="request">Dados do projeto</params> 
    /// <returns>Projeto cadastrado</returns> 
    /// <response code="201">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response> 

    [HttpPost("register-freela")]
    public async Task<ActionResult<ResponseFreelaJson>> RegisterFreelaAsync(
        [FromBody] RequestRegisterFreelaJson request)
    {
        var result = _validatorRegisterFreela.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            var response = await _service.RegisterFreelaAsync(User, request);
            return StatusCode(201, response);
        }
        catch (CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (InvalidIDException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    /// <summary> 
    /// Obter Lista de projetos cadastrados
    /// </summary> 
    /// <remarks> 
    /// { name: "name project" }
    /// </remarks>
    /// <params name="request">Filtro de pesquisa</params> 
    /// <returns>Lista de projetos cadastrados</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="500">Erro interno</response> 

    [HttpPost("get-all")]
    public async Task<ActionResult<List<ResponseAllFreelasJson>>> GetAllAsync(
        [FromQuery] PaginationParameters paginationParameters,
        [FromBody] RequestGetFreelaJson request)
    {
        try
        {
            var response = await _service.GetAllAsync(User, request, paginationParameters);
            if (response.Any())
            {
                return Ok(response);
            }
            return NoContent();
        }
        catch (BadHttpRequestException)
        {
            return BadRequest(new { message = "Erro na requisição" });
        }
        catch
        {
            return StatusCode(500, new { message = "Erro interno" });
        }
    }


    /// <summary> 
    /// Editar projeto cadastrado
    /// </summary> 
    /// <remarks> 
    /// {"name":"name project","description":"description","price":0,"startDate":"yyyy-MM-dd","finishDate":"yyyy-MM-dd","startPayment":"yyyy-MM-dd","paymentInstallment":0,"customerId":"HashId"}
    /// </remarks> 
    /// <params name="fHashId">ID do projeto</params> 
    /// <params name="request">Dados para atualizar</params> 
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro na requisição</response>
    /// <response code="401">Não autenticado</response>
    /// <response code="404">Não encontrado</response>


    [HttpPut("update-freela/{fHashId}")]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] string fHashId, [FromBody] RequestUpdateFreelaJson request)
    {
        var result = _validatorUpdateFreela.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            await _service.UpdateAsync(User, fHashId, request);
            return NoContent();
        }
        catch (InvalidIDException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (ProjectNotFoundException e)
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
    /// <response code="401">Não autenticado</response>
    /// <response code="404">Não encontrado</response> 

    [HttpGet("get-by-id/{fHashId}")]
    public async Task<ActionResult<ResponseFreelaJson>> GetByIdAsync(
        [FromRoute] string fHashId)
    {
        try
        {
            var response = await _service.GetByIdAsync(User, fHashId);
            return Ok(response);
        }
        catch (InvalidIDException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (ProjectNotFoundException e)
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

    [HttpDelete("delete-freela/{fHashId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string fHashId)
    {
        try
        {
            await _service.DeleteAsync(User, fHashId);
            return NoContent();
        }
        catch (ProjectNotFoundException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (InvalidIDException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}
