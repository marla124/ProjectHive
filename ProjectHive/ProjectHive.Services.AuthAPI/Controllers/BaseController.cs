using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace ProjectHive.Services.AuthAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class BaseController : Controller
{
    protected string? GetUserId()
    {
        return User?.FindFirst(ClaimConstants.ObjectId)?.Value ?? User?.FindFirst("userId")?.Value;
    }
}
