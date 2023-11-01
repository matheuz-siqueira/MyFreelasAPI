using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyFreelas.WebUI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Authorize]
public class MyFreelasController : ControllerBase
{   
}
