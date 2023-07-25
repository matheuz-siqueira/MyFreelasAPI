using Microsoft.AspNetCore.Mvc;
using myfreelas.Dtos.Dashboard;
using myfreelas.Services.Dashboard;

namespace myfreelas.Controllers;


[Route("api/v{version:apiVersion}/dashboard")]
public class DashboardController : MyFreelasController
{
    private readonly IDashboardService _service;
    public DashboardController(IDashboardService service)
    {
        _service = service;
    }

    /// <summary> 
    /// Obter o total de clientes
    /// </summary> 
    /// <returns>Total de clientes</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response> 
    /// <response code="500">Erro interno</response> 
    [HttpGet("total-customers")]
    public async Task<ActionResult<ResponseTotalCustomers>> GetTotalCustomersAsync()
    {
        try
        {
            var response = await _service.TotalCustomersAsync(User);
            if (response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, "Erro interno");
        }
    }

    /// <summary> 
    /// Obter clientes recorrentes
    /// </summary> 
    /// <returns>Clientes recorrentes</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="204">Sucesso</response> 
    /// <response code="500">Erro interno</response> 
    [HttpGet("total-recurrent-customers")]
    public async Task<ActionResult<ResponseRecurrentCustomerJson>> TotalRecurrentAsync()
    {
        try
        {
            var response = await _service.TotalRecurrentAsync(User);
            if (response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, "Erro interno");
        }
    }

    /// <summary> 
    /// Obter o total de clientes que são pessoa física
    /// </summary> 
    /// <returns>Total de clientes PF</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="500">Erro interno</response>
    [HttpGet("total-pf-customers")]
    public async Task<ActionResult<ResponseTotalPFCustomers>> TotalPFCustomersAsync()
    {
        try
        {
            var response = await _service.TotalPFCustomersAsync(User);
            return Ok(response);
        }
        catch
        {
            return StatusCode(201, "Erro na requisição");
        }
    }

    /// <summary> 
    /// Obter o total de projetos de um usuário 
    /// </summary> 
    /// <returns>Total de projetos</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response> 
    /// <response code="500">Erro interno</response>  
    [HttpGet("total-freelas")]
    public async Task<ActionResult<ResponseTotalFreelasJson>> TotalFreelasAsync()
    {
        try
        {
            var response = await _service.TotalFreelasAsync(User);
            if (response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, "Erro interno");
        }
    }
}
