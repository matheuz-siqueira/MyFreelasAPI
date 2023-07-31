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
    /// <returns>Todos os clients</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="500">Erro interno</response> 
    [HttpGet("total-customers")]
    public async Task<ActionResult<ResponseTotalCustomersJson>> GetAllCustomersAsync()
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
            return StatusCode(500, new { message = "Erro interno" });
        }
    }

    /// <summary> 
    /// Obter clientes recorrentes
    /// </summary> 
    /// <returns>Clientes recorrentes</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="204">Sucesso</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="500">Erro interno</response> 
    [HttpGet("total-recurrent-customers")]
    public async Task<ActionResult<ResponseRecurrentCustomerJson>> GetRecurrentAsync()
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
            return StatusCode(500, new { message = "Erro interno" });
        }
    }

    /// <summary> 
    /// Obter total de clientes pessoa física (PF)
    /// </summary> 
    /// <returns>Total de clientes PF</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="401">Não autenticado</response>
    /// <response code="500">Erro interno</response>
    [HttpGet("total-pf-customers")]
    public async Task<ActionResult<ResponseTotalPFCustomersJson>> GetPFCustomersAsync()
    {
        try
        {
            var response = await _service.TotalPFCustomersAsync(User);
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, new { message = "Erro interno" });
        }
    }

    /// <summary> 
    /// Obter total de clientes pessoa jurídica (PJ)
    /// </summary> 
    /// <returns>Total de clientes PJ</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="401">Não autenticado</response> 
    /// <response code="500">Erro interno</response>
    [HttpGet("total-pj-customers")]
    public async Task<ActionResult<ResponseTotalPJCustomersJson>> GetPJCustomersAsync()
    {
        try
        {
            var response = await _service.TotalPJCustomersAsync(User);
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, new { message = "Erro interno" });
        }
    }

    /// <summary> 
    /// Obter o total de projetos cadastrados 
    /// </summary> 
    /// <returns>Total de projetos</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response>
    /// <response code="401">Não autenticado</response> 
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
            return StatusCode(500, new { message = "Erro interno" });
        }
    }

    /// <summary> 
    /// Obter previsão mensal de faturamento
    /// </summary> 
    /// <remarks> 
    /// { "date" : "yyyy-MM-dd" }
    /// </remarks>
    /// <params name="request">Data para consulta</params>  
    /// <returns>Previsão de faturamento</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="401">Não autenticado</response> 
    /// <response code="500">Erro interno</response>  
    [HttpPost("monthly-billing")]
    public async Task<ActionResult<ResponseMonthlyBillingJson>> GetMonthlyBillingAsync(
        RequestGetMonthlyBillingJson request)
    {
        try
        {
            var response = await _service.MonthlyBillingAsync(request);
            return Ok(response);
        }
        catch
        {
            return StatusCode(500, new { message = "Erro interno" });
        }
    }
}
