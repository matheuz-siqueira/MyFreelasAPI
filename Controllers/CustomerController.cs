using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;
using myfreelas.Exceptions.BaseException;
using myfreelas.Exceptions.ErrorsValidators;
using myfreelas.Pagination;
using myfreelas.Services.Customer;

namespace myfreelas.Controllers;


[Route("api/v{version:apiVersion}/customers")]
public class CustomerController : MyFreelasController
{
    private readonly ICustomerService _service;
    private readonly IValidator<RequestCustomerJson> _validatorRegisterCustomer;
    public CustomerController(
        [FromServices] ICustomerService service, 
        [FromServices] IValidator<RequestCustomerJson> validatorRegisterCustomer)
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
    /// <response code="401">Não autenticado</response>

    
    [HttpPost("register-customer")]
    public async Task<ActionResult<ResponseRegisterCustomerJson>> RegisterCustomerAsync(
        [FromBody] RequestCustomerJson request)
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
        catch(CustomerAlreadyExistsException e)
        {
            return BadRequest(new {message = e.Message} );
        }
    }


    /// <summary> 
    /// Obter cliente pelo ID e projetos associados
    /// </summary>
    /// <params name="id">ID do cliente</params> 
    /// <returns>Cliente correspondente ao ID</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="401">Não autenticado</response>
    /// <response code="400">Erro na requisição</response> 
    /// <response code="404">Não encontrado</response>  
      
    
    [HttpGet("getbyid/{cHashId}")]
    public async Task<ActionResult<ResponseCustomerJson>> GetByIdAsync(
        [FromRoute] string cHashId) 
    {
        try 
        {
            var response = await _service.GetByIdAsync(User, cHashId); 
            return Ok(response);
        }
        catch(CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch(InvalidIDException e)
        {
            return BadRequest(new { message = e.Message }); 
        }
    }

    /// <summary>
    /// Obter clientes de um usuário 
    /// </summary> 
    /// <remarks> 
    /// { name: "string" }
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
        var response = await _service.GetAllAsync(customerParameters, request, User);
        if(!response.Any())
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
    /// {"name":"string","type":0,"email":"string","phoneNumber":"string","otherContact":"string"}
    /// </remarks>
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response>
    /// <response code="400">Erro</response>
    /// <response code="404">Cliente não encontrado</response>

    
    [HttpPut("update-customer/{cHashId}")]
    public async Task<ActionResult> UpdateCustomerAsync(
        [FromRoute] string cHashId, [FromBody] RequestCustomerJson request)
    {
        try
        {
            await _service.UpdateCustomerAsync(User, request, cHashId); 
            return NoContent();
        }
        catch(CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest("Erro na requisição");
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
    public async Task<ActionResult> DeleteAsync([FromRoute] string cHashId)
    {
        try 
        {
            await _service.DeleteAsync(User, cHashId); 
            return NoContent(); 
        }
        catch(CustomerNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch(InvalidIDException e)
        {
            return BadRequest(new { message = e.Message }); 
        }
        catch
        {
            return BadRequest("Erro na requisição");
        }
    }
}
