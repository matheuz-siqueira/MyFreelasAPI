using Microsoft.AspNetCore.Http;
using MyFreelas.Application.Interfaces;
using System.Security.Claims;

namespace MyFreelas.Application.Services;

public class LoggedService : ILoggedService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoggedService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int GetCurrentUserId()
    {
         var id = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
         return id;
    }
}
