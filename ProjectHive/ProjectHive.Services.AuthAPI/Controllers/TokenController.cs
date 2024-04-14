using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Models;

namespace ProjectHive.Services.AuthAPI.Controllers
{
    public class TokenController(IConfiguration configuration) : Controller
    {
        public Task<ActionResult> Login(LoginModel request) 
        {
            
        }
    }
}
