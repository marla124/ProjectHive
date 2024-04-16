using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.AuthAPI.Model;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Models.RequestModel;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;

public class UserController(IUserService userService, IMapper mapper) : Controller
{
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = mapper.Map<UserViewModel>(await userService.GetById(id, cancellationToken));
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await userService.DeleteById(id, cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateUser(RegisterModel request, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<UserDto>(request);

        mapper.Map<UserViewModel>(await userService.RegisterUser(dto, cancellationToken));
        return Ok();
    }

    [HttpPatch]
    [Route("[action]")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequestViewModel request, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<UserDto>(request);

        return Ok(mapper.Map<UserViewModel>(await userService.Update(dto, cancellationToken)));
    }
}
