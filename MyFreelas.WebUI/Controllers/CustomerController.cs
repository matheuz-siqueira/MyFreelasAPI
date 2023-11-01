using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyFreelas.Application.Dtos.Customer;
using MyFreelas.Application.Interfaces;
using MyFreelas.Application.Exceptions.ErrorsValidators;
using MyFreelas.Application.Exceptions.BaseException;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.WebUI.Controllers;

[Route("api/v{version:apiVersion}/customers")]
public class CustomerController : MyFreelasController
{
    private readonly ICustomerService _service;
    private readonly IValidator<RequestCustomerJson> _validatorRegisterCustomer;
    public CustomerController(
        ICustomerService service,
        IValidator<RequestCustomerJson> validatorRegisterCustomer)
    {
        _service = service;
        _validatorRegisterCustomer = validatorRegisterCustomer;
    }

    /// <summary> 
    /// Registrar cliente no sistema
    /// </summary>
    /// <remarks>
    /// {"name":"name-customer","type":0,"email":"customer@mail","phoneNumber":"customer-phone","otherContact":"customer-contact"}
    /// </remarks>
    /// <params name="request">Dados do cliente</params> 
    /// <returns>Cliente cadastrado</returns>
    /// <response code="201">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response>


    [HttpPost("register-customer")]
    public async Task<ActionResult<ResponseRegisterCustomerJson>> RegisterCustomerAsync(
        RequestCustomerJson request)
    {
        var result = _validatorRegisterCustomer.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            var response = await _service.RegisterCustomerAsync(request);
            return StatusCode(201, response);
        }
        catch (CustomerAlreadyExistsException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }


    /// <summary> 
    /// Obter cliente pelo ID e projetos associados
    /// </summary>
    /// <params name="id">ID do cliente</params> 
    /// <returns>Cliente correspondente ao ID</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="400">Erro na requisição</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="404">Não encontrado</response>  

    [HttpGet("getbyid/{cHashId}")]
    public async Task<ActionResult<ResponseCustomerJson>> GetByIdAsync(
        string cHashId)
    {
        try
        {
            var response = await _service.GetByIdAsync(cHashId);
            return Ok(response);
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
    /// Obter clientes de um usuário 
    /// </summary> 
    /// <remarks> 
    /// { name: "name customer" }
    /// </remarks>
    /// <params name="request">Nome do cliente</params> 
    /// <returns>Clientes correspondente com o filtro de pesquisa</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response>

    [HttpPost("get-all")]
    public async Task<ActionResult<List<ResponseAllCustomerJson>>> GetAllAsync(
        [FromQuery] PaginationParameters customerParameters,
        [FromBody] RequestGetCustomersJson request)
    {
        var response = await _service.GetAllAsync(customerParameters, request);
        if (!response.Any())
        {
            return NoContent();
        }
        return Ok(response);
    }

    /// <summary> 
    /// Atualizar cliente cadastrado
    /// </summary>
    /// <params name="id">ID do cliente</params> 
    /// <params name="request">Dados do cliente</params> 
    /// <remarks> 
    /// {"name":"name-customer","type":0,"email":"customer@mail","phoneNumber":"customer-phone","otherContact":"customer-contact"}
    /// </remarks>
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response>
    /// <response code="400">Erro</response>
    /// <response code="401">Não autenticado</response>
    /// <response code="404">Cliente não encontrado</response>

    [HttpPut("update-customer/{cHashId}")]
    public async Task<ActionResult> UpdateCustomerAsync(
         string cHashId, RequestCustomerJson request)
    {
        try
        {
            await _service.UpdateCustomerAsync(request, cHashId);
            return NoContent();
        }
        catch (CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "Erro na requisição" });
        }
    }

    /// <summary> 
    /// Deletar um cliente
    /// </summary> 
    /// <params name="id">ID do cliente</params>
    /// <returns>Nada</returns>
    /// <response code="204">Sucesso</response>
    /// <response code="400">Erro</response> 
    /// <response code="401">Não autenticado</response> 
    /// <response code="404">Não encontrado</response>  


    [HttpDelete("delete-customer/{cHashId}")]
    public async Task<ActionResult> DeleteAsync(string cHashId)
    {
        try
        {
            await _service.DeleteAsync(cHashId);
            return NoContent();
        }
        catch (CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (InvalidIDException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "Erro na requisição" });
        }
    }
}
