using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHive.Services.ProjectsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

        }

        
    }
}
